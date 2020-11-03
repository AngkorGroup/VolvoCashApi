using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.ClientAgg
{
    public class Client : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(11)]
        public string Ruc { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(20)]
        public string TPCode { get; set; }

        [Required]
        public Status Status { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();
        #endregion

        #region NotMapped Properties
        [NotMapped]
        public Contact MainContact { get => Contacts.FirstOrDefault(c => c.Type == ContactType.Primary); }

        [NotMapped]
        public Money Balance 
        {
            get
            {
                var calculatedBalance = new Money(Currency.USD, 0);                
                foreach (var contact in Contacts)
                {
                    foreach(var card in contact.Cards)
                    {
                        calculatedBalance = calculatedBalance.Add(card.Balance);
                    }
                }
                return calculatedBalance;
            }
        }
        #endregion

        #region Constructor
        public Client()
        {
        }

        public Client(string ruc, string address, string email, string name, string phone)
        {
            Ruc = ruc;
            Address = address;
            Email = email;
            Name = name;
            Phone = phone;
            Status = Status.Active;
            Contacts = new List<Contact>();
        }
        #endregion

        #region Public Methods
        public void UpdateFields(string ruc, string address, string email, string name, string phone)
        {
            Ruc = ruc;
            Address = address;
            Email = email;
            Name = name;
            Phone = phone;
        }

        public List<Card> GetAllCards()
        {
            var cards = new List<Card>();
            if (Contacts != null && Contacts.Any())
            {
                Contacts.ToList().ForEach(contact => cards.AddRange(contact.Cards));
            }
            return cards;
        }

        public IEnumerable<CardTypeSummary> GetCardTypesSummary()
        {
            var cards = GetAllCards();
            var cardTypesSummary = cards.GroupBy(c => c.CardTypeId)
                                        .Select(ct => new CardTypeSummary
                                        {
                                            Id = ct.First().CardType.Id,
                                            Name = ct.First().CardType.Name,
                                            DisplayName = ct.First().CardType.DisplayName,
                                            Sum = ct.Aggregate(new Money(ct.First().CardType.Currency,0), (acc,c)=> acc.Add(c.Balance))
                                        }).ToList();
            return cardTypesSummary;
        }
        #endregion
    }
}

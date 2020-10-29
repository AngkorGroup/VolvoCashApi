using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.ContactAgg
{
    public class Contact : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public ContactType Type { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public DocumentType DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        [Required]
        public Status Status { get; set; }

        public DateTime? ArchiveAt { get; set; }

        [ForeignKey("ContactParent")]
        public int? ContactParentId { get; set; }

        public virtual Contact ContactParent { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

        public virtual ICollection<Contact> ContactChildren { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        #endregion

        #region NotMapped Properties
        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }
        #endregion

        #region Constructor
        public Contact()
        {
        }

        public Contact(Client client,ContactType type, DocumentType documentType,
                        string documentNumber, string phone, string firstName,
                        string lastName, string email, int? contactParentId = null)
        {
            Client = client;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ContactParentId = contactParentId;
            Type = type;
            Status = Status.Active;
            Cards = new List<Card>();
            User = new User(UserType.Contact);
        }

        public Contact(Client client, ContactType type, DocumentType documentType, 
                        string documentNumber, string phone, string firstName, 
                        string lastName, string email, ICollection<Contact> contactChildren)
        {
            Client = client;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ContactChildren = contactChildren;
            Type = type;
            Status = Status.Active;
            Cards = new List<Card>();
            User = new User(UserType.Contact);
        }
        #endregion
    }
}

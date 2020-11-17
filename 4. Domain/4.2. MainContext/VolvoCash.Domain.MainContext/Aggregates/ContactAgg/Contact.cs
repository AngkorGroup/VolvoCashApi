using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.EnumAgg;
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

        public virtual ICollection<Batch> Batches { get; } = new List<Batch>();

        public virtual ICollection<Contact> ContactChildren { get; set; } = new List<Contact>();

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        #endregion

        #region NotMapped Properties
        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }

        [NotMapped]
        public bool IsActive { get => Status.Active == 1; }
        #endregion

        #region Constructor
        public Contact()
        {
        }

        public Contact(Client client, ContactType type, DocumentType documentType,
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
            Status = new Status(1);
            Cards = new List<Card>();
            var userType=new UserType("Contact","User of type Contact");
            User = new User(userType);
        }
        #endregion

        #region Public Methods
        public void Delete()
        {
            Status = new Status(0);
            ArchiveAt = DateTime.Now;
        }
        #endregion
    }
}

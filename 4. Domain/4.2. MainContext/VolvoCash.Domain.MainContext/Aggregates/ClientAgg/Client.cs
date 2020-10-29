using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
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
    }
}

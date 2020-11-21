using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.StatusAgg;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.UserAgg
{
    public class Cashier : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(400)]
        public string PasswordHash { get; set; }

        [MaxLength(100)]
        public string TPCode { get; set; }

        public string Imei { get; set; }

        [Required]
        public int DealerId { get; set; }

        [ForeignKey("DealerId")]
        public virtual Dealer Dealer { get; set; }

        public virtual ICollection<Charge> Charges { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public Status Status { get; set; }

        public DateTime? ArchiveAt { get; set; }

        #endregion

        #region NotMapped Properties
        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }       
        #endregion

        #region Constructor
        public Cashier()
        {
        }

        public Cashier(Dealer dealer, string firstName, string lastName, string password,
                    string phone, string tpCode, string email,string imei)
        {
            Dealer = dealer;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = CryptoMethods.HashText(password);
            Phone = phone;
            TPCode = tpCode;
            Email = email;
            Imei = imei;
            Status = new Status(1);
            var userType = new UserType("Cashier","###");
            User = new User(userType);
        }

        public void SetPasswordHash(string password)
        {
            PasswordHash = CryptoMethods.HashText(password);
        }

        public void Delete()
        {
            Status = new Status(0);
            ArchiveAt = DateTime.Now;
        }
        #endregion
    }
}

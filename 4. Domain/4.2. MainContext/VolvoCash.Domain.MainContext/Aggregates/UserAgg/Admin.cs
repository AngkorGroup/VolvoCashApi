using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.UserAgg
{
    public class Admin :AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(400)]
        public string PasswordHash { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Dealer")]
        public int? DealerId { get; set; }

        public virtual Dealer Dealer { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }
        #endregion

        #region NotMapped Properties
        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }       
        #endregion

        #region Constructor
        public Admin()
        {
        }

        public Admin(string firstName, string lastName, string password, string phone, string email,Dealer dealer = null)
        {
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = CryptoMethods.HashText(password);
            Phone = phone;
            Email = email;
            Status = Status.Active;
            User = new User(UserType.WebAdmin);
            Dealer = dealer;          
        }
        #endregion

        #region Public Methods
        public void SetPasswordHash(string password)
        {
            PasswordHash = CryptoMethods.HashText(password);
        }

        public void Delete()
        {
            Status = Status.Inactive;
            ArchiveAt = DateTime.Now;
        }
        #endregion
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.CrossCutting.Utils;
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

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

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
        #endregion

        #region NotMapped Properties
        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }
        #endregion

        #region Constructor
        public Admin()
        {
        }

        public Admin( string firstName, string lastName, string userName,
                    string password, string phone, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            PasswordHash = CryptoMethods.HashText(password);
            Phone = phone;
            Email = email;
            User = new User(UserType.Admin);
        }
        #endregion
    }
}

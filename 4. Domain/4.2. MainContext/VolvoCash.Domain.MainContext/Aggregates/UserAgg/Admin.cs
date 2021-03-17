using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.UserAgg
{
    public class Admin : AuditableEntityWithKey<int>
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

        [ForeignKey("Cashier")]
        public int? CashierId { get; set; }

        public virtual Cashier Cashier { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<RoleAdmin> RoleAdmins { get; set; } = new List<RoleAdmin>();
        #endregion

        #region NotMapped Properties
        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }

        [NotMapped]
        public List<string> MenuOptions { get; set; } = new List<string>();
        #endregion

        #region Constructor
        public Admin()
        {
        }

        public Admin(string firstName, string lastName, string password, string phone, string email, 
            List<int> roleIds,
            Dealer dealer = null,
            Cashier cashier = null)
        {
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = CryptoMethods.HashText(password);
            Phone = phone;
            Email = email;
            Status = Status.Active;
            User = new User(UserType.WebAdmin);
            roleIds.ForEach(roleId => RoleAdmins.Add(new RoleAdmin(roleId, Id)));
            Dealer = dealer;
            Cashier = Cashier;
        }
        #endregion

        #region Public Methods
        public void SetPasswordHash(string password)
        {
            PasswordHash = CryptoMethods.HashText(password);
        }

        public void SetNewRoleAdmins(List<int> roleIds)
        {
            roleIds.ForEach(roleId => RoleAdmins.Add(new RoleAdmin(roleId, Id)));
        }

        public void SetMenuOptions()
        {
            var menuOptions = new List<string>();
            foreach (var roleAdmin in RoleAdmins)
            {
                var roleMenus = roleAdmin.Role.RoleMenus.OrderBy(rm => rm.Menu.Order).ToList();
                foreach (var roleMenu in roleMenus)
                {
                    var key = roleMenu.Menu.Key;
                    if (!menuOptions.Contains(key))
                    {
                        menuOptions.Add(key);
                    }
                }
            }
            
            MenuOptions = menuOptions;
        }

        public void Delete()
        {
            Status = Status.Inactive;
            ArchiveAt = DateTime.Now;
        }
        #endregion
    }
}

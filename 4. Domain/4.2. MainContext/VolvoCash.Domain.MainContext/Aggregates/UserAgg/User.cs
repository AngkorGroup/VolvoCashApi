using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.UserAgg
{
    public class User : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public UserType Type { get; set; }

        public virtual ICollection<Cashier> Cashiers { get; set; } = new List<Cashier>();

        public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

        public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
        #endregion

        #region Not Mapped Properties
        [NotMapped]
        public virtual Cashier Cashier { get => Cashiers.FirstOrDefault(); }

        [NotMapped]
        public virtual Admin Admin { get => Admins.FirstOrDefault(); }

        [NotMapped]
        public virtual Contact Contact { get => Contacts.FirstOrDefault(); }

        [NotMapped]
        public virtual ICollection<Session> OpenSessions { get => Sessions.Where(s => s.Status == Status.Active).ToList(); }
        #endregion

        #region Constructor
        public User()
        {
        }

        public User(UserType type)
        {
            Type = type;
        }
        #endregion
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.StatusAgg;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.UserAgg
{
    public class Session : EntityGuid
    {
        #region Properties
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public Status Status { get; set; }

        public string PushDeviceToken { get; set; }
        #endregion

        #region Constructor
        public Session()
        {
        }

        public Session(int userId, string pushDeviceToken)
        {
            UserId = userId;
            Status = new Status(1);
            PushDeviceToken = pushDeviceToken;
        }
        #endregion
    }
}

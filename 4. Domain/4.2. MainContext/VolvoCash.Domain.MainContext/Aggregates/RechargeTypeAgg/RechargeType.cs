using System;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.RechargeTypeAgg
{
    public class RechargeType : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string TPCode { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }
        #endregion

        #region Constructor
        public RechargeType()
        {
        }

        public RechargeType(string name, string tpCode)
        {
            Name = name;
            TPCode = tpCode;
            Status = Status.Active;
        }
        #endregion
    }
}

/*Contract,
  Addendum
*/

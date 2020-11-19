using System;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BusinessAreaAgg
{
    public class BusinessArea : AuditableEntityWithKey<int>
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
        public BusinessArea()
        {
        }

        public BusinessArea(string name, string tpCode)
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

using System;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.SMSCodeAgg
{
    public class SMSCode : EntityInt
    {
        #region Properties
        public int Code { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public bool ItWasAlreadyUsed { get; set; }

        public DateTime? DateTimeUsed { get; set; }
        #endregion
    }
}

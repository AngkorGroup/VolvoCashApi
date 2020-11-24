using System;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BankAgg
{
    public class Bank : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string TPCode { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }
        #endregion

        #region Constructor
        public Bank()
        {
        }

        public Bank(string name, string abbreviation, string tpCode)
        {
            Name = name;
            Abbreviation = abbreviation;
            TPCode = tpCode;
            Status = Status.Active;
        }
        #endregion
    }
}

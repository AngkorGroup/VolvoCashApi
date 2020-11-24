using System;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg
{
    public class Currency : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string TPCode { get; set; }

        public string Abbreviation { get; set; }

        public string Symbol { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }
        #endregion

        #region Constructor
        public Currency()
        {
        }

        public Currency(string name, string abbreviation, string symbol, string tpCode)
        {
            Name = name;
            Abbreviation = abbreviation;
            Symbol = symbol;
            TPCode = tpCode;
            Status = Status.Active;
        }
        #endregion
    }
}

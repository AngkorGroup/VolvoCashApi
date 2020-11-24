using System;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg
{
    public class DocumentType : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string TPCode { get; set; }

        public string Abbreviation { get; set; }

        public string SUNATCode { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }
        #endregion

        #region Constructor
        public DocumentType()
        {
        }

        public DocumentType(string name, string abbreviation, string sunatCode, string tpCode)
        {
            Name = name;
            Abbreviation = abbreviation;
            SUNATCode = sunatCode;
            TPCode = tpCode;
            Status = Status.Active;
        }
        #endregion
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BankAgg
{
    public class BankDocumentType : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public string Equivalence { get; set; }

        [Required]
        [ForeignKey("Bank")]
        public int BankId { get; set; }

        public Bank Bank { get; set; }

        [Required]
        [ForeignKey("DocumentType")]
        public int DocumentTypeId { get; set; }

        public DocumentType DocumentType { get; set; }
        #endregion
    }
}

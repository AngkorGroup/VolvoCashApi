using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BatchAgg
{
    public class BatchError : AuditableEntityWithKey<int>
    {
        #region Properties
        public int RowIndex { get; set; }

        [MaxLength(300)]
        public string ErrorMessage { get; set; }

        [MaxLength(300)]
        public string FileName { get; set; }

        [MaxLength(4000)]
        public string LineContent { get; set; }
        #endregion

        #region Constructor
        public BatchError()
        {
        }

        public BatchError(int rowIndex,string errorMessage,string fileName,string lineContent)
        {
            RowIndex = rowIndex;
            ErrorMessage = errorMessage;
            FileName = fileName;
            LineContent = lineContent;
        }
        #endregion
    }
}

using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.MainContext.DTO.Movements;

namespace VolvoCash.Application.MainContext.DTO.Batches
{
    public class BatchMovementDTO
    {
        #region Properties
        public MoneyDTO Amount { get; set; }

        public int BatchId { get; set; }

        public BatchDTO Batch { get; set; }

        public int MovementId { get; set; }

        public MovementDTO Movement { get; set; }
        #endregion
    }
}

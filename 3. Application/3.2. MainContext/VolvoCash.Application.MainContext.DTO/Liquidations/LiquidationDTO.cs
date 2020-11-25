using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Liquidations
{
    public class LiquidationDTO
    {
        #region Properties
        public int Id { get; set; }

        public MoneyDTO Amount { get; set; }

        public int DealerId { get; set; }

        public virtual DealerDTO Dealer { get; set; }

        public LiquidationStatus LiquidationStatus { get; set; }
        #endregion
    }
}

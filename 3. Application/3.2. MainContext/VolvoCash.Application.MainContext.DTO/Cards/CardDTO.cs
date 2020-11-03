using System.Collections.Generic;
using VolvoCash.Application.MainContext.DTO.CardBatches;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.MainContext.DTO.Movements;

namespace VolvoCash.Application.MainContext.DTO.Cards
{
    public class CardDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Code { get; set; }

        public MoneyDTO Balance { get; set; }

        public MoneyDTO CalculatedBalance { get; set; }

        public string QrUrl { get; set; }

        public ContactListDTO Contact { get; set; }

        public int ContactId { get; set; }

        public List<MovementDTO> Movements { get; set; }

        public List<CardBatchDTO> CardBatchesWithBalance { get; set; }

        public int CardTypeId { get; set; }

        public CardTypeDTO CardType { get; set; }

        public string TPCode { get; set; }
        #endregion

        #region Public Methods
        public dynamic GetCardForQr()
        {
            return new
            {
                Code,
                Id
            };
        }
        #endregion
    }
}

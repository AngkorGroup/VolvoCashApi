using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.MainContext.DTO.Contacts;

namespace VolvoCash.Application.MainContext.DTO.Cards
{
    public class CardListDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Code { get; set; }

        public MoneyDTO Balance { get; set; }

        public MoneyDTO CalculatedBalance { get; set; }

        public CardTypeDTO CardType { get; set; }

        public ContactListDTO Contact { get; set; }

        public int ContactId { get; set; }

        public string CardToken { get; set; }
        #endregion

        #region Public Methods
        public dynamic GetCardForQr()
        {
            return new 
            {
                Code ,
                Id 
            };
        }
        #endregion
    }
}

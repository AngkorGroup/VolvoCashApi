using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.MainContext.DTO.Contacts;

namespace VolvoCash.Application.MainContext.DTO.POJOS
{
    public class Load
    {
        #region Properties
        public ClientDTO Client { get; set; }

        public ContactDTO Contact { get; set; }

        public CardDTO Card { get; set; }

        public BatchDTO Batch { get; set; }

        public int RowIndex { get; set; }

        public string ErrorMessage { get; set; }

        public string LineContent { get; set; }
        #endregion
    }
}

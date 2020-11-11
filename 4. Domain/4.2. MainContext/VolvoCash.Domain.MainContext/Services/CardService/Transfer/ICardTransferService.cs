using System.Threading.Tasks;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;

namespace VolvoCash.Domain.MainContext.Services.CardService
{
    public interface ICardTransferService
    {
        Task<Transfer> PerformTransfer(Card originCard, Contact destinyContact, Money Amount);
    }
}

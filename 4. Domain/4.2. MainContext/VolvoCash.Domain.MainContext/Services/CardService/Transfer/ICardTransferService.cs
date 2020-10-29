using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Domain.MainContext.Services.CardService
{
    public interface ICardTransferService
    {
         void PerformTransfer(Transfer transfer);
    }
}

using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Domain.MainContext.Services.CardService
{  
    public interface ICardRechargeService
    {
        void PerformRecharge(Card card, Batch batch);
    }
}

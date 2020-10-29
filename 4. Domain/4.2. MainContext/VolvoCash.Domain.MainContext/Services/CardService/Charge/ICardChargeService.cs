using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Domain.MainContext.Services.CardService
{
    public interface ICardChargeService
    {
        void PerformCharge(Charge charge);
    }
}

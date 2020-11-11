using System.Threading.Tasks;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Domain.MainContext.Services.CardService
{
    public interface INotificationChargeService
    {
        Task SendNotificationToCashier(Charge charge);
        Task SendNotificationToContact(Charge charge);
    }
}

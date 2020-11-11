using System.Threading.Tasks;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public interface ITransferRepository : IRepository<Transfer>
    {
        Task<Transfer> GetTransferById(int id);
    }
}

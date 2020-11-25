using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public interface IChargeRepository : IRepository<Charge>
    {
        Task<IEnumerable<Charge>> GetChargesToLiquidate();
    }
}

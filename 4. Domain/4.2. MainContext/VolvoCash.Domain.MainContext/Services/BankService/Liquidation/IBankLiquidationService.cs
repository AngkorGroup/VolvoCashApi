using System.Collections.Generic;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;

namespace VolvoCash.Domain.MainContext.Services.BankService
{  
    public interface IBankLiquidationService
    {
        void GenerateBCPFile(List<Liquidation> liquidations);
    }
}

using System.Collections.Generic;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;

namespace VolvoCash.Domain.MainContext.Services.BankService
{
    public interface IBankLiquidationService
    {
        byte[] GenerateBankFile(BankAccount originBankAccount, List<Liquidation> liquidations);
    }
}

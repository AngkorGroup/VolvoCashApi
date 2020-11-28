using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Liquidations
{
    public class ScheduleLiquidationsRequest
    {
        public int BankId { get; set; }

        public int BankAccountId { get; set; }

        public List<int> LiquidationsId { get; set; }
    }
}

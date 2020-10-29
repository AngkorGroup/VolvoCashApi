using System.Linq;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Data.MainContext;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.DistributedServices.MainContext.Seeders
{
    public static class DbCashierSeeder
    {
        public static void Initialize(MainDbContext context)
        {
            if (context.Cashiers.Any())
            {
                return;
            }
            var dealer = new Dealer("DEALER TPCODE",
                            "987654321",
                            "Av prueba",
                            "Admin DEALER",
                            "MI NOMBRE",
                            "12345678901",
                            4,
                            DealerType.Internal);
            var cashier = new Cashier( dealer,
                                       "Cajero",
                                       "Prueba",
                                       "Holi1234$",
                                       "949283373",
                                       "CAJERO TPCODE",
                                       "cajero@volvo.com");
            context.Cashiers.Add(cashier);
            context.SaveChanges();
        }
    }
}

using System;
using System.Linq;
using VolvoCash.Data.MainContext;
using VolvoCash.Domain.MainContext.Aggregates.RechargeTypeAgg;

namespace VolvoCash.DistributedServices.MainContext.Seeders
{
    public static class DbRechargeTypeSeeder
    {
        public static void Initialize(MainDbContext context)
        {
            if (context.RechargeTypes.Any())
                return;

            var rechargeType1 = new RechargeType(
                "Adenda",
                "A"
            );
            rechargeType1.CreatedBy = "ADMIN";
            rechargeType1.CreatedAt = DateTime.Now;
            context.RechargeTypes.Add(rechargeType1);

            var rechargeType2 = new RechargeType(
                "Contrato",
                "C"
            );
            rechargeType2.CreatedBy = "ADMIN";
            rechargeType2.CreatedAt = DateTime.Now;
            context.RechargeTypes.Add(rechargeType2);

            context.SaveChanges();
        }
    }
}

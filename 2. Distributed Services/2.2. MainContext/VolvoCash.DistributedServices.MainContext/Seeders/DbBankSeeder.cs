using System;
using System.Linq;
using VolvoCash.Data.MainContext;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;

namespace VolvoCash.DistributedServices.MainContext.Seeders
{
    public static class DbBankSeeder
    {
        public static void Initialize(MainDbContext context)
        {
            if (context.Banks.Any())
                return;

            var bankBCP = new Bank("Banco de Crédito", "BCP", "001");
            bankBCP.CreatedBy = "ADMIN";
            bankBCP.CreatedAt = DateTime.Now;
            context.Banks.Add(bankBCP);

            var bankBBVA = new Bank("Banco Continental", "BBVA", "002");
            bankBBVA.CreatedBy = "ADMIN";
            bankBBVA.CreatedAt = DateTime.Now;
            context.Banks.Add(bankBBVA);

            context.SaveChanges();
        }
    }
}

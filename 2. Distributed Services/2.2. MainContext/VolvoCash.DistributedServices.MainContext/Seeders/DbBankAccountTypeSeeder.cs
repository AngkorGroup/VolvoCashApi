using System;
using System.Linq;
using VolvoCash.Data.MainContext;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;

namespace VolvoCash.DistributedServices.MainContext.Seeders
{
    public static class DbBankAccountTypeSeeder
    {
        public static void Initialize(MainDbContext context)
        {
            if (context.BankAccountTypes.Any())
                return;

            var bankAccountType1 = new BankAccountType("Cuenta Corriente");
            bankAccountType1.CreatedBy = "ADMIN";
            bankAccountType1.CreatedAt = DateTime.Now;
            context.BankAccountTypes.Add(bankAccountType1);

            var bankAccountType2 = new BankAccountType("Ahorros");
            bankAccountType2.CreatedBy = "ADMIN";
            bankAccountType2.CreatedAt = DateTime.Now;
            context.BankAccountTypes.Add(bankAccountType2);
            
            var bankAccountType3 = new BankAccountType("Maestra");
            bankAccountType3.CreatedBy = "ADMIN";
            bankAccountType3.CreatedAt = DateTime.Now;
            context.BankAccountTypes.Add(bankAccountType3);

            context.SaveChanges();
        }
    }
}

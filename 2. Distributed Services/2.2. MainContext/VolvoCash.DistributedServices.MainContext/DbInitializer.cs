using VolvoCash.Data.MainContext;
using VolvoCash.DistributedServices.MainContext.Seeders;

namespace VolvoCash.DistributedServices.MainContext
{
    public static class DbInitializer
    {
        public static void Initialize(MainDbContext context)
        {
            //Check if database is created
            context.Database.EnsureCreated();

            //Seeders
            DbAdminSeeder.Initialize(context);
            DbRechargeTypeSeeder.Initialize(context);
        }
    }
}

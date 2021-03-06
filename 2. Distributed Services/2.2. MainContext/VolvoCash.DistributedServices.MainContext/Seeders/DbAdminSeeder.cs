using System;
using System.Collections.Generic;
using System.Linq;
using VolvoCash.Data.MainContext;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.DistributedServices.MainContext.Seeders
{
    public static class DbAdminSeeder
    {
        public static void Initialize(MainDbContext context)
        {
            if (context.Admins.Any())
                return;

            var roleIds = new List<int> { 1 };
            var admin = new Admin(
                "Admin",
                "Prueba",
                "Holi1234$",
                "912456783",
                "admin@volvo.com",
                roleIds
            );
            admin.CreatedBy = "ADMIN";
            admin.CreatedAt = DateTime.Now;
            context.Admins.Add(admin);

            context.SaveChanges();
        }
    }
}

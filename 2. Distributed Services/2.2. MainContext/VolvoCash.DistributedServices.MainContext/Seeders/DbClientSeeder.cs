using System;
using System.Linq;
using VolvoCash.Data.MainContext;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;

namespace VolvoCash.DistributedServices.MainContext.Seeders
{
    public static class DbClientSeeder
    {
        public static void Initialize(MainDbContext context)
        {
            if (context.Clients.Any())
                return;

            //Angkor client
            var angkorClient = new Client
            {
                Name = "ANGKOR GROUP SAC",
                Ruc = "20506002975",
                Address = "Calle Monte Rosa 270",
                Phone = "987654321",
                Status = Status.Active,
                CreatedBy = "ADMIN",
                CreatedAt = DateTime.Now
            };
            context.Clients.Add(angkorClient);

            //SaveChanges
            context.SaveChanges();
        }
    }
}

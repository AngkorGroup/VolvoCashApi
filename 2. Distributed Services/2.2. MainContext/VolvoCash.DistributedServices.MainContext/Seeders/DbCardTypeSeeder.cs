using System;
using System.Linq;
using VolvoCash.Data.MainContext;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.DistributedServices.MainContext.Seeders
{
    public static class DbCardTypeSeeder
    {
        public static void Initialize(MainDbContext context)
        {
            if (context.CardTypes.Any())
                return;

            //Urea cardType
            var vUreCard = new CardType
            {
                Name = "VURE",
                DisplayName = "VOLVO UREA",
                Term = 540,
                Currency = Currency.USD,
                Color = "#919296",
                TPCode = "0001",
                Status = Status.Active,
                CreatedBy = "ADMIN",
                CreatedAt = DateTime.Now
            };
            context.CardTypes.Add(vUreCard);

            //Volvo repuestos cardType
            var vRepCard = new CardType
            {
                Name = "VREP",
                DisplayName = "VOLVO REPUESTOS",
                Term = 540,
                Currency = Currency.USD,
                Color = "#16A6C9",
                TPCode = "0002",
                Status = Status.Active,
                CreatedBy = "ADMIN",
                CreatedAt = DateTime.Now
            };
            context.CardTypes.Add(vRepCard);
            
            //SaveChanges
            context.SaveChanges();
        }
    }
}

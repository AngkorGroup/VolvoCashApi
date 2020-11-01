using System.Linq;
using System.Collections.Generic;
using VolvoCash.Data.MainContext;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;

namespace VolvoCash.DistributedServices.MainContext.Seeders
{
    public static class DbContactSeeder
    {
        public static void Initialize(MainDbContext context)
        {
            if (context.Contacts.Any())
                return;

            var angkorClient = context.Clients.FirstOrDefault(c => c.Ruc == "20506002975");
            if (angkorClient != null)
            {
                //Brajean contact
                var brajeanContact = new Contact(
                    angkorClient,
                    ContactType.Secondary,
                    DocumentType.DNI,
                    "71835732",
                    "956734314",
                    "Brajean",
                    "Junchaya Navarrete",
                    "bjunchaya@angkorperu.com",
                     new List<Contact> {
                        new Contact(
                            angkorClient,
                            ContactType.Secondary,
                            DocumentType.DNI,
                            "71992952",
                            "949283373",
                            "Gianfranco",
                            "Galvez Montero",
                            "ggalvez@angkorperu.com"
                        ),
                        new Contact(
                            angkorClient,
                            ContactType.Primary,
                            DocumentType.DNI,
                            "73076369",
                            "990300645",
                            "Juan José",
                            "Ramirez Calderón",
                            "jjramirez@angkorperu.com"
                        )
                    }
                );
                context.Contacts.Add(brajeanContact);
                context.SaveChanges();
            }
        }
    }
}

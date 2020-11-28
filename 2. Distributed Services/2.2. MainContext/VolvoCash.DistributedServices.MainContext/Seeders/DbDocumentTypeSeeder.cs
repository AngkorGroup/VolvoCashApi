using System;
using System.Linq;
using VolvoCash.Data.MainContext;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;

namespace VolvoCash.DistributedServices.MainContext.Seeders
{
    public static class DbDocumentTypeSeeder
    {
        public static void Initialize(MainDbContext context)
        {
            if (context.DocumentTypes.Any())
                return;

            var docTypeDNI = new DocumentType(
                "Libreta Electoral o DNI",
                "DNI",
                "01",
                "01"
            );
            docTypeDNI.CreatedBy = "ADMIN";
            docTypeDNI.CreatedAt = DateTime.Now;
            context.DocumentTypes.Add(docTypeDNI);

            var docTypeCE = new DocumentType(
                "Carnet de Extranjería",
                "C.E.",
                "04",
                "04"
            );
            docTypeCE.CreatedBy = "ADMIN";
            docTypeCE.CreatedAt = DateTime.Now;
            context.DocumentTypes.Add(docTypeCE);

            var docTypeRUC = new DocumentType(
                "Registro Único de Contribuyente",
                "RUC",
                "06",
                "06"
            );
            docTypeRUC.CreatedBy = "ADMIN";
            docTypeRUC.CreatedAt = DateTime.Now;
            context.DocumentTypes.Add(docTypeRUC);

            var docTypePAS = new DocumentType(
                "Pasaporte",
                "PAS",
                "07",
                "07"
            );
            docTypePAS.CreatedBy = "ADMIN";
            docTypePAS.CreatedAt = DateTime.Now;
            context.DocumentTypes.Add(docTypePAS);

            context.SaveChanges();
        }
    }
}

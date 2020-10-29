using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VolvoCash.CrossCutting.Tests.Classes
{
    class EntityWithValidatableObject : IValidatableObject
    {
        public string RequiredProperty { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(RequiredProperty))
            {
                validationResults.Add(new ValidationResult("Invalid Required property", new string[] { "RequiredProperty" }));
            }

            return validationResults;
        }
    }
}

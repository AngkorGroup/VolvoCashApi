using System.ComponentModel.DataAnnotations;

namespace VolvoCash.CrossCutting.Tests.Classes
{
    class EntityWithValidationAttribute
    {
        [Required(ErrorMessage = "This is a required property")]
        public string RequiredProperty { get; set; }
    }
}

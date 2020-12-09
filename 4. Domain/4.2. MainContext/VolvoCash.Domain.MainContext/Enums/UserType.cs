using System.Runtime.Serialization;

namespace VolvoCash.Domain.MainContext.Enums
{
    public enum UserType
    {
        [EnumMember(Value = "WebAdmin")]
        WebAdmin = 0,

        [EnumMember(Value = "Contacto")]
        Contact = 1,

        [EnumMember(Value = "Cajero")]
        Cashier = 2
    }
}

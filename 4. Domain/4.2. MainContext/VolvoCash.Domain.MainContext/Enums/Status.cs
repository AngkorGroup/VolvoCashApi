using System.Runtime.Serialization;

namespace VolvoCash.Domain.MainContext.Enums
{
    public enum Status
    {
        [EnumMember(Value = "Inactivo")]
        Inactive = 0,

        [EnumMember(Value = "Activo")]
        Active = 1
    }
}

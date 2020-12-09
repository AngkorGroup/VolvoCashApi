using System.Runtime.Serialization;

namespace VolvoCash.Domain.MainContext.Enums
{
    public enum ChargeStatus
    {
        //[EnumMember(Value = "Pendiente")]
        Pending = 1,

        //[EnumMember(Value = "Aceptado")]
        Accepted = 2,

        //[EnumMember(Value = "Rechazado")]
        Rejected = 3,

        //[EnumMember(Value = "Cancelado")]
        Canceled = 4
    }
}

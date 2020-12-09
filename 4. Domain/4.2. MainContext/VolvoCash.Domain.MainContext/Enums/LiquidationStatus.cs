using System.Runtime.Serialization;

namespace VolvoCash.Domain.MainContext.Enums
{
    public enum LiquidationStatus
    {
        [EnumMember(Value = "Anulado")]
        Canceled = 0,

        [EnumMember(Value = "Generado")]
        Generated = 1,

        [EnumMember(Value = "Programado")]
        Scheduled = 2,

        [EnumMember(Value = "Pagado")]
        Paid = 3
    }
}

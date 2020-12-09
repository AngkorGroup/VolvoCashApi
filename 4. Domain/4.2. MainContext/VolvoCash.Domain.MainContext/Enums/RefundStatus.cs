using System.Runtime.Serialization;

namespace VolvoCash.Domain.MainContext.Enums
{
    public enum RefundStatus
    {
        [EnumMember(Value = "Anulado")]
        Canceled = 0,

        [EnumMember(Value = "Programado")]
        Scheduled = 1,

        [EnumMember(Value = "Pagado")]
        Paid = 2
    }
}

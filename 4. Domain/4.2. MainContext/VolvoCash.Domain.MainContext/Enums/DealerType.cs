using System.Runtime.Serialization;

namespace VolvoCash.Domain.MainContext.Enums
{
    public enum DealerType
    {
        [EnumMember(Value = "Own Dealer")]
        Internal = 0,

        [EnumMember(Value = "Private")]
        External = 1
    }
}

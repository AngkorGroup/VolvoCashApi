using System.Runtime.Serialization;

namespace VolvoCash.Domain.MainContext.Enums
{
    public enum ChargeType
    {
        [EnumMember(Value = "Remoto")]
        Remote = 1,
        
        [EnumMember(Value = "Presencial")]
        FaceToFace = 2
    }
}

using System.Threading.Tasks;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.SMSCodeAgg
{
    public interface ISMSCodeRepository : IRepository<SMSCode>
    {
        Task<int> GenerateSMSCodeAsync(string phone);
        Task<SMSCode> VerifyCodeAsync(string phone, int smsCode);
    }
}

using System.Threading.Tasks;

namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public interface IReportManager
    {
        Task<byte[]> GetReportAsync(CustomReport report);
    }
}

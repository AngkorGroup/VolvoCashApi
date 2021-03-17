using System;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Admins;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.MainContext.DTO.Sessions;

namespace VolvoCash.Application.MainContext.Authentication.Services
{
    public interface IAuthenticationAppService : IDisposable
    {
        #region ApiClient
        Task<int> RequestSmsCodeAsync(string phone);
        Task<ContactListDTO> VerifySmsCodeAsync(string phone, int code);
        #endregion

        #region ApiPOS
        Task<CashierDTO> LoginCashierAsync(string email, string password);
        Task SendRecoverPasswordEmailToCashier(string email);
        Task RecoverPasswordCashier(string email, string code, string newPassword);
        #endregion

        #region ApiWeb
        Task<AdminDTO> LoginAdminAsync(string email, string password);
        Task SendRecoverPasswordEmailToAdmin(string email);
        Task RecoverPasswordAdmin(string token, string newPassword, string confirmPassword);
        #endregion

        #region Common
        Task<SessionDTO> CreateSessionAsync(int userId, string deviceToken = "");
        Task DestroySessionAsync(Guid sessionId);
        #endregion
    }
}

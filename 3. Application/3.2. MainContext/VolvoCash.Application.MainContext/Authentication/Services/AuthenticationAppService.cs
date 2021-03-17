using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Admins;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.MainContext.DTO.Sessions;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.NetFramework.Utils;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Aggregates.SMSCodeAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Authentication.Services
{
    public class AuthenticationAppService : IAuthenticationAppService
    {
        #region Members
        private readonly IContactRepository _contactRepository;
        private readonly ISMSCodeRepository _smsCodeRepository;
        private readonly ICashierRepository _cashierRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailManager _emailManager;
        private readonly IResetPasswordTokenRepository _resetPasswordTokenRepository;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public AuthenticationAppService(IContactRepository contactRepository,
                                        ISMSCodeRepository smsCodeRepository,
                                        ICashierRepository cashierRepository,
                                        IAdminRepository adminRepository,
                                        ISessionRepository sessionRepository,
                                        IConfiguration configuration,
                                        IEmailManager emailManager,
                                        IResetPasswordTokenRepository resetPasswordTokenRepository)
        {
            _contactRepository = contactRepository;
            _smsCodeRepository = smsCodeRepository;
            _cashierRepository = cashierRepository;
            _adminRepository = adminRepository;
            _sessionRepository = sessionRepository;
            _configuration = configuration;
            _emailManager = emailManager;
            _resetPasswordTokenRepository = resetPasswordTokenRepository;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiClient Public Methods
        public async Task<int> RequestSmsCodeAsync(string phone)
        {
            var contact = await _contactRepository.GetByPhoneAsync(phone);
            if (contact != null && contact.Status == Status.Active)
            {
                var code = await _smsCodeRepository.GenerateSMSCodeAsync(phone);
                return code;
            }
            else
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_PhoneNotFound));
            }
        }

        public async Task<ContactListDTO> VerifySmsCodeAsync(string phone, int code)
        {
            var smsCode = await _smsCodeRepository.VerifyCodeAsync(phone, code);
            if (smsCode != null)
            {
                var contact = await _contactRepository.GetByPhoneAsync(phone);
                contact.HasSignedIn = true;
                await _contactRepository.UnitOfWork.CommitAsync();
                return contact.ProjectedAs<ContactListDTO>();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
        #endregion

        #region ApiPOS Public Methods
        public async Task<CashierDTO> LoginCashierAsync(string email, string password)
        {
            var cashier = await _cashierRepository.LoginAsync(email, CryptoMethods.HashText(password));
            if (cashier != null)
            {
                return cashier.ProjectedAs<CashierDTO>();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task SendRecoverPasswordEmailToCashier(string email)
        {
            var cashier = await _cashierRepository.GetCashierByEmailAsync(email);
            var resetPasswordToken = new ResetPasswordToken(cashier);
            _resetPasswordTokenRepository.Add(resetPasswordToken);
            await _resetPasswordTokenRepository.UnitOfWork.CommitAsync();
            var subject = _resources.GetStringResource(LocalizationKeys.Application.messages_RecoverCashierPasswordEmailSubject);
            var body = _resources.GetStringResource(LocalizationKeys.Application.messages_RecoverCashierPasswordEmailBody);
            body = body.Replace("{{Code}}", resetPasswordToken.Token);
            _emailManager.SendEmail(email, subject, body);
        }

        public async Task RecoverPasswordCashier(string email, string code, string newPassword)
        {
            var resetPasswordToken = _resetPasswordTokenRepository.Filter(filter: pt => pt.Token == code && pt.Cashier.Email== email, includeProperties: "Cashier").FirstOrDefault();
            var cashier = resetPasswordToken.Cashier;
            cashier.SetPasswordHash(newPassword);
            await _resetPasswordTokenRepository.UnitOfWork.CommitAsync();
            await _cashierRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<AdminDTO> LoginAdminAsync(string email, string password)
        {
            var admin = await _adminRepository.LoginAsync(email, CryptoMethods.HashText(password));
            if (admin != null)
            {
                return admin.ProjectedAs<AdminDTO>();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
        public async Task SendRecoverPasswordEmailToAdmin(string email)
        {
            var admin = await _adminRepository.GetAdminByEmailAsync(email);
            var resetPasswordToken = new ResetPasswordToken(admin);
            _resetPasswordTokenRepository.Add(resetPasswordToken);
            await _resetPasswordTokenRepository.UnitOfWork.CommitAsync();

            var subject = _resources.GetStringResource(LocalizationKeys.Application.messages_RecoverAdminPasswordEmailSubject);
            var body = _resources.GetStringResource(LocalizationKeys.Application.messages_RecoverAdminPasswordEmailBody);
            var recoverUrl = _configuration["Application:BaseWebAdminUrl"] + _configuration["Application:AdminRecoverPasswordUrl"];
            recoverUrl = string.Format(recoverUrl, resetPasswordToken.Token);
            body = string.Format(body, recoverUrl);

            _emailManager.SendEmail(email, subject, body);
        }

        public async Task RecoverPasswordAdmin(string token, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_PasswordAndConfirmNotMatch));
            }
            var resetPasswordToken = _resetPasswordTokenRepository.Filter(filter: pt => pt.Token == token, includeProperties: "Admin").FirstOrDefault();
            var admin = resetPasswordToken.Admin;
            admin.SetPasswordHash(password);
            await _adminRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region Common Public Methods
        public async Task<SessionDTO> CreateSessionAsync(int userId, string deviceToken = "")
        {
            var session = new Session(userId, deviceToken);
            _sessionRepository.Add(session);
            await _sessionRepository.UnitOfWork.CommitAsync();
            return session.ProjectedAs<SessionDTO>();
        }

        public async Task DestroySessionAsync(Guid sessionId)
        {
            var session = _sessionRepository.Get(sessionId);
            if (session != null)
            {
                session.Status = Status.Inactive;
                await _sessionRepository.UnitOfWork.CommitAsync();
            }            
        }
        
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            //dispose all resources
            _contactRepository.Dispose();
            _smsCodeRepository.Dispose();
            _cashierRepository.Dispose();
            _adminRepository.Dispose();
            _sessionRepository.Dispose();
        }
        #endregion
    }
}

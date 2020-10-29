﻿using System;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Admins;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Application.MainContext.DTO.Contacts;

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
        #endregion

        #region ApiWeb
        Task<AdminDTO> LoginAdminAsync(string email, string password);
        #endregion
    }
}

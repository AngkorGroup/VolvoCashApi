using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Menus;

namespace VolvoCash.Application.MainContext.Menus.Services
{
    public interface IMenuAppService : IDisposable
    {
        #region ApiWeb
        Task<List<MenuDTO>> GetMenus();
        #endregion
    }
}

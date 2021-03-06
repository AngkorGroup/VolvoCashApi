using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Menus;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.MenuAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Menus.Services
{
    public class MenuAppService : IMenuAppService
    {
        #region Members
        private readonly IMenuRepository _menuRepository;
        #endregion

        #region Constructor
        public MenuAppService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<MenuDTO>> GetMenus()
        {
            var menus = await _menuRepository.FilterAsync(
                filter: m => (m.Status == Status.Active && m.Type == MenuType.Children),
                includeProperties: "MenuParent",
                orderBy: m => m.OrderBy(m => m.Order));
            return menus.ProjectedAsCollection<MenuDTO>();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _menuRepository.Dispose();
        }
        #endregion
    }
}

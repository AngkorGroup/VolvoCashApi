using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.BusinessAreas;

namespace VolvoCash.Application.MainContext.BusinessAreas.Services
{
    public interface IBusinessAreaAppService : IDisposable
    {
        Task<List<BusinessAreaDTO>> GetBusinessAreas(bool onlyActive);

        Task<BusinessAreaDTO> GetBusinessArea(int id);

        Task<BusinessAreaDTO> AddAsync(BusinessAreaDTO rechargeTypeDTO);

        Task<BusinessAreaDTO> ModifyAsync(BusinessAreaDTO rechargeTypeDTO);

        Task Delete(int id);
    }
}

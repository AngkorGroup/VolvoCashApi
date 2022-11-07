using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Mappings;

namespace VolvoCash.Application.MainContext.MappingDetails.Services
{
    public interface IMappingDetailAppService : IDisposable
    {
        #region ApiWeb
        Task<List<MappingDetailDTO>> GetMappingDetails(bool onlyActive);
        Task<MappingDetailDTO> GetMappingDetail(int id);
        Task<MappingDetailDTO> AddAsync(MappingDetailDTO mappingDetailDTO);
        Task<MappingDetailDTO> ModifyAsync(MappingDetailDTO mappingDetailDTO);
        Task Delete(int id);
        #endregion
    }
}

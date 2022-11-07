using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Mappings;

namespace VolvoCash.Application.MainContext.MappingHeaders.Services
{
    public interface IMappingHeaderAppService : IDisposable
    {
        #region ApiWeb
        Task<List<MappingHeaderDTO>> GetMappingHeaders(bool onlyActive);
        Task<MappingHeaderDTO> GetMappingHeader(int id);
        Task<MappingHeaderDTO> AddAsync(MappingHeaderDTO mappingHeaderDTO);
        Task<MappingHeaderDTO> ModifyAsync(MappingHeaderDTO mappingHeaderDTO);
        Task Delete(int id);
        Task<List<MappingDetailDTO>> GetMappingDetails(int id);
        #endregion
    }
}

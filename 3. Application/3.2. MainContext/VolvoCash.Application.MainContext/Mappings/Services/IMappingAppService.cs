using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Mappings;

namespace VolvoCash.Application.MainContext.Mappings.Services
{
    public interface IMappingAppService : IDisposable
    {
        #region ApiWeb
        Task<List<MappingDTO>> GetMappings(bool onlyActive);
        Task<MappingDTO> GetMapping(int id);
        Task<MappingDTO> AddAsync(MappingDTO mappingDTO);
        Task<MappingDTO> ModifyAsync(MappingDTO mappingDTO);
        Task Delete(int id);
        Task<List<MappingHeaderDTO>> GetMappingHeaders(int id);
        #endregion
    }
}

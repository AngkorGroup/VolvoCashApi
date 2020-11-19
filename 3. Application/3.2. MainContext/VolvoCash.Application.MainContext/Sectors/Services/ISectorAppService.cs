using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Sectors;

namespace VolvoCash.Application.MainContext.Sectors.Services
{
    public interface ISectorAppService : IDisposable
    {
        Task<List<SectorDTO>> GetSectors(bool onlyActive);

        Task<SectorDTO> GetSector(int id);

        Task<SectorDTO> AddAsync(SectorDTO sectorDTO);

        Task<SectorDTO> ModifyAsync(SectorDTO sectorDTO);

        Task Delete(int id);
    }
}

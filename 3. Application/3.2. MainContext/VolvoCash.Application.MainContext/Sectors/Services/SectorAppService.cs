using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Sectors;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.SectorAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Sectors.Services
{
    public class SectorAppService :  ISectorAppService
    {
        #region Members
        private readonly ISectorRepository _sectorRepository;
        #endregion

        #region Constructor
        public SectorAppService(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<SectorDTO>> GetSectors(bool onlyActive)
        {
            var sectors =  await _sectorRepository.FilterAsync(filter: b=> !onlyActive || b.Status == Status.Active);
            return sectors.ProjectedAsCollection<SectorDTO>();
        }

        public async Task<SectorDTO> GetSector(int id)
        {
            var sector = await _sectorRepository.GetAsync(id);
            return sector.ProjectedAs<SectorDTO>();
        }

        public async Task<SectorDTO> AddAsync(SectorDTO sectorDTO)
        {           
            var sector = new Sector(sectorDTO.Name,sectorDTO.TPCode);
            _sectorRepository.Add(sector);
            await _sectorRepository.UnitOfWork.CommitAsync();
            return sector.ProjectedAs<SectorDTO>();
        }

        public async Task<SectorDTO> ModifyAsync(SectorDTO sectorDTO)
        {
            var sector = await _sectorRepository.GetAsync(sectorDTO.Id);
            sector.Name = sectorDTO.Name;
            sector.TPCode = sectorDTO.TPCode;
            await _sectorRepository.UnitOfWork.CommitAsync();
            return sectorDTO;
        }

        public async Task Delete(int id)
        {
            var sector = await _sectorRepository.GetAsync(id);
            sector.ArchiveAt = DateTime.Now;
            sector.Status = Status.Inactive;
            _sectorRepository.Modify(sector);
            await _sectorRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _sectorRepository.Dispose();
        }
        #endregion
    }
}

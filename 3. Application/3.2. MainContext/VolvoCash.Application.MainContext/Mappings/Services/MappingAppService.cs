using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Mappings;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.MappingAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Mappings.Services
{
    public class MappingAppService : IMappingAppService
    {
        #region Members
        private readonly IMappingRepository _mappingRepository;
        private readonly IMappingHeaderRepository _mappingHeaderRepository;

        #endregion

        #region Constructor
        public MappingAppService(IMappingRepository mappingRepository,
                              IMappingHeaderRepository mappingHeaderRepository)
        {
            _mappingRepository = mappingRepository;
            _mappingHeaderRepository = mappingHeaderRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<MappingDTO>> GetMappings(bool onlyActive)
        {
            var mappings = await _mappingRepository.FilterAsync(filter: b => !onlyActive || b.Status == Status.Active);
            return mappings.ProjectedAsCollection<MappingDTO>();
        }

        public async Task<MappingDTO> GetMapping(int id)
        {
            var mapping = await _mappingRepository.GetAsync(id);
            return mapping.ProjectedAs<MappingDTO>();
        }

        public async Task<MappingDTO> AddAsync(MappingDTO mappingDTO)
        {
            var mapping = new Mapping(mappingDTO.MappingNumber, mappingDTO.Name, mappingDTO.Type, mappingDTO.Description, mappingDTO.Company,
                                      mappingDTO.Feeder, mappingDTO.File, mappingDTO.Username, mappingDTO.Password, mappingDTO.Date, 
                                      mappingDTO.Filter, mappingDTO.Version, mappingDTO.ReceiverLogicalId, mappingDTO.ReceiverComponentId, 
                                      mappingDTO.SenderLogicalId, mappingDTO.SenderComponentId);
            _mappingRepository.Add(mapping);
            await _mappingRepository.UnitOfWork.CommitAsync();
            return mapping.ProjectedAs<MappingDTO>();
        }

        public async Task<MappingDTO> ModifyAsync(MappingDTO mappingDTO)
        {
            var mapping = await _mappingRepository.GetAsync(mappingDTO.Id);
            mapping.MappingNumber = mappingDTO.MappingNumber;
            mapping.Name = mappingDTO.Name;
            mapping.Type = mappingDTO.Type;
            mapping.Description = mappingDTO.Description;
            mapping.Company = mappingDTO.Company;
            mapping.Feeder = mappingDTO.Feeder;
            mapping.File = mappingDTO.File;
            mapping.Username = mappingDTO.Username;
            mapping.Password = mappingDTO.Password;
            mapping.Date = mappingDTO.Date;
            mapping.Filter = mappingDTO.Filter;
            mapping.Version = mappingDTO.Version;
            mapping.ReceiverLogicalId = mappingDTO.ReceiverLogicalId;
            mapping.ReceiverComponentId = mappingDTO.ReceiverComponentId;
            mapping.SenderLogicalId = mappingDTO.SenderLogicalId;
            mapping.SenderComponentId = mappingDTO.SenderComponentId;
            await _mappingRepository.UnitOfWork.CommitAsync();
            return mappingDTO;
        }

        public async Task Delete(int id)
        {
            var mapping = await _mappingRepository.GetAsync(id);
            mapping.ArchiveAt = DateTime.Now;
            mapping.Status = Status.Inactive;
            _mappingRepository.Modify(mapping);
            await _mappingRepository.UnitOfWork.CommitAsync();
        }

        public async Task<List<MappingHeaderDTO>> GetMappingHeaders(int id)
        {
            var mappingHeaders = (await _mappingHeaderRepository.FilterAsync(filter: mh => mh.MappingId == id && mh.Status == Status.Active));
            return mappingHeaders.ProjectedAsCollection<MappingHeaderDTO>();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _mappingRepository.Dispose();
            _mappingHeaderRepository.Dispose();
        }
        #endregion
    }
}

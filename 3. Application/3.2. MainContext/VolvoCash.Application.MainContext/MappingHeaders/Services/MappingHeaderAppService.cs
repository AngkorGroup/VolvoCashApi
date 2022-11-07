using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Mappings;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.MappingAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.MappingHeaders.Services
{
    public class MappingHeaderAppService : IMappingHeaderAppService
    {
        #region Members
        private readonly IMappingHeaderRepository _mappingHeaderRepository;
        private readonly IMappingDetailRepository _mappingDetailRepository;

        #endregion

        #region Constructor
        public MappingHeaderAppService(IMappingHeaderRepository mappingHeaderRepository,
                              IMappingDetailRepository mappingDetailRepository)
        {
            _mappingHeaderRepository = mappingHeaderRepository;
            _mappingDetailRepository = mappingDetailRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<MappingHeaderDTO>> GetMappingHeaders(bool onlyActive)
        {
            var mappingHeaders = await _mappingHeaderRepository.FilterAsync(filter: b => !onlyActive || b.Status == Status.Active);
            return mappingHeaders.ProjectedAsCollection<MappingHeaderDTO>();
        }

        public async Task<MappingHeaderDTO> GetMappingHeader(int id)
        {
            var mappingHeader = await _mappingHeaderRepository.GetAsync(id);
            return mappingHeader.ProjectedAs<MappingHeaderDTO>();
        }

        public async Task<MappingHeaderDTO> AddAsync(MappingHeaderDTO mappingHeaderDTO)
        {
            var mappingHeader = new MappingHeader(mappingHeaderDTO.Type, mappingHeaderDTO.RecordType, mappingHeaderDTO.Company,
                                                  mappingHeaderDTO.DocumentNumber, mappingHeaderDTO.Reference, mappingHeaderDTO.Control,
                                                  mappingHeaderDTO.DocumentType, mappingHeaderDTO.DocumentDate, mappingHeaderDTO.PostDate,
                                                  mappingHeaderDTO.Currency, mappingHeaderDTO.ExchangeRate, mappingHeaderDTO.DocumentHeader,
                                                  mappingHeaderDTO.TranslationDate, mappingHeaderDTO.IntercompanyNumber, mappingHeaderDTO.TradingPartner,
                                                  mappingHeaderDTO.ExchangeRateType, mappingHeaderDTO.PostingPeriod, mappingHeaderDTO.ExchangeRateToFactor,
                                                  mappingHeaderDTO.ExchangeRateFromFactor, mappingHeaderDTO.ReversalReason, mappingHeaderDTO.ReversalDate,
                                                  mappingHeaderDTO.MappingId);
            _mappingHeaderRepository.Add(mappingHeader);
            await _mappingHeaderRepository.UnitOfWork.CommitAsync();
            return mappingHeader.ProjectedAs<MappingHeaderDTO>();
        }

        public async Task<MappingHeaderDTO> ModifyAsync(MappingHeaderDTO mappingHeaderDTO)
        {
            var mappingHeader = await _mappingHeaderRepository.GetAsync(mappingHeaderDTO.Id);
            mappingHeader.Type = mappingHeaderDTO.Type;
            mappingHeader.RecordType = mappingHeaderDTO.RecordType;
            mappingHeader.Company = mappingHeaderDTO.Company;
            mappingHeader.DocumentNumber = mappingHeaderDTO.DocumentNumber;
            mappingHeader.Reference = mappingHeaderDTO.Reference;
            mappingHeader.Control = mappingHeaderDTO.Control;
            mappingHeader.DocumentType = mappingHeaderDTO.DocumentType;
            mappingHeader.DocumentDate = mappingHeaderDTO.DocumentDate;
            mappingHeader.PostDate = mappingHeaderDTO.PostDate;
            mappingHeader.Currency = mappingHeaderDTO.Currency;
            mappingHeader.ExchangeRate = mappingHeaderDTO.ExchangeRate;
            mappingHeader.DocumentHeader = mappingHeaderDTO.DocumentHeader;
            mappingHeader.TranslationDate = mappingHeaderDTO.TranslationDate;
            mappingHeader.IntercompanyNumber = mappingHeaderDTO.IntercompanyNumber;
            mappingHeader.TradingPartner = mappingHeaderDTO.TradingPartner;
            mappingHeader.ExchangeRateType = mappingHeaderDTO.ExchangeRateType;
            mappingHeader.PostingPeriod = mappingHeaderDTO.PostingPeriod;
            mappingHeader.ExchangeRateToFactor = mappingHeaderDTO.ExchangeRateToFactor;
            mappingHeader.ExchangeRateFromFactor = mappingHeaderDTO.ExchangeRateFromFactor;
            mappingHeader.ReversalReason = mappingHeaderDTO.ReversalReason;
            mappingHeader.ReversalDate = mappingHeaderDTO.ReversalDate;
            await _mappingHeaderRepository.UnitOfWork.CommitAsync();
            return mappingHeaderDTO;
        }

        public async Task Delete(int id)
        {
            var mappingHeader = await _mappingHeaderRepository.GetAsync(id);
            mappingHeader.ArchiveAt = DateTime.Now;
            mappingHeader.Status = Status.Inactive;
            _mappingHeaderRepository.Modify(mappingHeader);
            await _mappingHeaderRepository.UnitOfWork.CommitAsync();
        }

        public async Task<List<MappingDetailDTO>> GetMappingDetails(int id)
        {
            var mappingDetails = (await _mappingDetailRepository.FilterAsync(filter: md => md.MappingHeaderId == id && md.Status == Status.Active));
            return mappingDetails.ProjectedAsCollection<MappingDetailDTO>();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _mappingHeaderRepository.Dispose();
            _mappingDetailRepository.Dispose();
        }
        #endregion
    }
}

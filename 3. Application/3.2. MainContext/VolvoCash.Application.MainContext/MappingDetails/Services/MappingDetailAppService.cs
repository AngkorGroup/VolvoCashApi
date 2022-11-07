using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Mappings;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.MappingAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.MappingDetails.Services
{
    public class MappingDetailAppService : IMappingDetailAppService
    {
        #region Members
        private readonly IMappingDetailRepository _mappingDetailRepository;
        #endregion

        #region Constructor
        public MappingDetailAppService(IMappingDetailRepository mappingDetailRepository)
        {
            _mappingDetailRepository = mappingDetailRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<MappingDetailDTO>> GetMappingDetails(bool onlyActive)
        {
            var mappingDetails = await _mappingDetailRepository.FilterAsync(filter: b => !onlyActive || b.Status == Status.Active);
            return mappingDetails.ProjectedAsCollection<MappingDetailDTO>();
        }

        public async Task<MappingDetailDTO> GetMappingDetail(int id)
        {
            var mappingDetail = await _mappingDetailRepository.GetAsync(id);
            return mappingDetail.ProjectedAs<MappingDetailDTO>();
        }

        public async Task<MappingDetailDTO> AddAsync(MappingDetailDTO mappingDetailDTO)
        {
            var mappingDetail = new MappingDetail(mappingDetailDTO.Type, mappingDetailDTO.DocumentType, mappingDetailDTO.Line, mappingDetailDTO.RecordType,
                                                  mappingDetailDTO.Company, mappingDetailDTO.Reference, mappingDetailDTO.PostKey, mappingDetailDTO.Account,
                                                  mappingDetailDTO.Sign, mappingDetailDTO.TaxCode, mappingDetailDTO.TaxAmount, mappingDetailDTO.CostCenter,
                                                  mappingDetailDTO.ProfitCenter, mappingDetailDTO.TradePartner, mappingDetailDTO.DocText, mappingDetailDTO.MoreInfo,
                                                  mappingDetailDTO.BusinessArea, mappingDetailDTO.Market, mappingDetailDTO.Customer, mappingDetailDTO.ProductModel, 
                                                  mappingDetailDTO.LineType, mappingDetailDTO.Classification, mappingDetailDTO.MappingHeaderId);
            _mappingDetailRepository.Add(mappingDetail);
            await _mappingDetailRepository.UnitOfWork.CommitAsync();
            return mappingDetail.ProjectedAs<MappingDetailDTO>();
        }

        public async Task<MappingDetailDTO> ModifyAsync(MappingDetailDTO mappingDetailDTO)
        {
            var mappingDetail = await _mappingDetailRepository.GetAsync(mappingDetailDTO.Id);
            mappingDetail.Type = mappingDetailDTO.Type;
            mappingDetail.DocumentType = mappingDetailDTO.DocumentType;
            mappingDetail.Line = mappingDetailDTO.Line;
            mappingDetail.RecordType = mappingDetailDTO.RecordType;
            mappingDetail.Company = mappingDetailDTO.Company;
            mappingDetail.Reference = mappingDetailDTO.Reference;
            mappingDetail.PostKey = mappingDetailDTO.PostKey;
            mappingDetail.Account = mappingDetailDTO.Account;
            mappingDetail.Sign = mappingDetailDTO.Sign;
            mappingDetail.TaxCode = mappingDetailDTO.TaxCode;
            mappingDetail.TaxAmount = mappingDetailDTO.TaxAmount;
            mappingDetail.CostCenter = mappingDetailDTO.CostCenter;
            mappingDetail.ProfitCenter = mappingDetailDTO.ProfitCenter;
            mappingDetail.TradePartner = mappingDetailDTO.TradePartner;
            mappingDetail.DocText = mappingDetailDTO.DocText;
            mappingDetail.MoreInfo = mappingDetailDTO.MoreInfo;
            mappingDetail.BusinessArea = mappingDetailDTO.BusinessArea;
            mappingDetail.Market = mappingDetailDTO.Market;
            mappingDetail.Customer = mappingDetailDTO.Customer;
            mappingDetail.ProductModel = mappingDetailDTO.ProductModel;
            mappingDetail.LineType = mappingDetailDTO.LineType;
            mappingDetail.Classification = mappingDetailDTO.Classification;
            await _mappingDetailRepository.UnitOfWork.CommitAsync();
            return mappingDetailDTO;
        }

        public async Task Delete(int id)
        {
            var mappingDetail = await _mappingDetailRepository.GetAsync(id);
            mappingDetail.ArchiveAt = DateTime.Now;
            mappingDetail.Status = Status.Inactive;
            _mappingDetailRepository.Modify(mappingDetail);
            await _mappingDetailRepository.UnitOfWork.CommitAsync();
        }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _mappingDetailRepository.Dispose();
        }
        #endregion
    }
}

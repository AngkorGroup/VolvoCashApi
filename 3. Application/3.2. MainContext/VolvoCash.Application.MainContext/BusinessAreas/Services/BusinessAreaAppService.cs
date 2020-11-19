using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.BusinessAreas;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.BusinessAreaAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.BusinessAreas.Services
{
    public class BusinessAreaAppService :  IBusinessAreaAppService
    {
        #region Members
        private readonly IBusinessAreaRepository _rechargeTypeRepository;
        #endregion

        #region Constructor
        public BusinessAreaAppService(IBusinessAreaRepository rechargeTypeRepository)
        {
            _rechargeTypeRepository = rechargeTypeRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<BusinessAreaDTO>> GetBusinessAreas(bool onlyActive)
        {
            var rechargeTypes =  await _rechargeTypeRepository.FilterAsync(filter: b=> !onlyActive || b.Status == Status.Active);
            return rechargeTypes.ProjectedAsCollection<BusinessAreaDTO>();
        }

        public async Task<BusinessAreaDTO> GetBusinessArea(int id)
        {
            var rechargeType = await _rechargeTypeRepository.GetAsync(id);
            return rechargeType.ProjectedAs<BusinessAreaDTO>();
        }

        public async Task<BusinessAreaDTO> AddAsync(BusinessAreaDTO rechargeTypeDTO)
        {           
            var rechargeType = new BusinessArea(rechargeTypeDTO.Name, rechargeTypeDTO.TPCode);
            _rechargeTypeRepository.Add(rechargeType);
            await _rechargeTypeRepository.UnitOfWork.CommitAsync();
            return rechargeType.ProjectedAs<BusinessAreaDTO>();
        }

        public async Task<BusinessAreaDTO> ModifyAsync(BusinessAreaDTO rechargeTypeDTO)
        {
            var rechargeType = await _rechargeTypeRepository.GetAsync(rechargeTypeDTO.Id);
            rechargeType.Name = rechargeTypeDTO.Name;
            rechargeType.TPCode = rechargeTypeDTO.TPCode;
            await _rechargeTypeRepository.UnitOfWork.CommitAsync();
            return rechargeTypeDTO;
        }

        public async Task Delete(int id)
        {
            var rechargeType = await _rechargeTypeRepository.GetAsync(id);
            rechargeType.ArchiveAt = DateTime.Now;
            rechargeType.Status = Status.Inactive;
            _rechargeTypeRepository.Modify(rechargeType);
            await _rechargeTypeRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _rechargeTypeRepository.Dispose();
        }
        #endregion
    }
}

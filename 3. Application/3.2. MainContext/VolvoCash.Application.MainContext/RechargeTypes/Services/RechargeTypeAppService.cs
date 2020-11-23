using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.RechargeTypes;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.RechargeTypeAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.RechargeTypes.Services
{
    public class RechargeTypeAppService :  IRechargeTypeAppService
    {
        #region Members
        private readonly IRechargeTypeRepository _rechargeTypeRepository;
        #endregion

        #region Constructor
        public RechargeTypeAppService(IRechargeTypeRepository rechargeTypeRepository)
        {
            _rechargeTypeRepository = rechargeTypeRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<RechargeTypeDTO>> GetRechargeTypes(bool onlyActive)
        {
            var rechargeTypes =  await _rechargeTypeRepository.FilterAsync(filter: b=> !onlyActive || b.Status == Status.Active);
            return rechargeTypes.ProjectedAsCollection<RechargeTypeDTO>();
        }

        public async Task<RechargeTypeDTO> GetRechargeType(int id)
        {
            var rechargeType = await _rechargeTypeRepository.GetAsync(id);
            return rechargeType.ProjectedAs<RechargeTypeDTO>();
        }

        public async Task<RechargeTypeDTO> AddAsync(RechargeTypeDTO rechargeTypeDTO)
        {           
            var rechargeType = new RechargeType(rechargeTypeDTO.Name, rechargeTypeDTO.TPCode);
            _rechargeTypeRepository.Add(rechargeType);
            await _rechargeTypeRepository.UnitOfWork.CommitAsync();
            return rechargeType.ProjectedAs<RechargeTypeDTO>();
        }

        public async Task<RechargeTypeDTO> ModifyAsync(RechargeTypeDTO rechargeTypeDTO)
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

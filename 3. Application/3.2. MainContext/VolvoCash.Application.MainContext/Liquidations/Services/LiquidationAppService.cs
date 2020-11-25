using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Liquidations;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Liquidations.Services
{
    public class LiquidationAppService : ILiquidationAppService
    {
        #region Members
        private readonly ILiquidationRepository _liquidationRepository;
        #endregion

        #region Constructor
        public LiquidationAppService(ILiquidationRepository liquidationRepository)
        {
            _liquidationRepository = liquidationRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<LiquidationDTO>> GetLiquidations(DateTime date, LiquidationStatus liquidationStatus)
        {
            var truncDate = date.Date;
            var liquidations = await _liquidationRepository.FilterAsync(filter: l => l.CreatedAt.Date == truncDate && l.LiquidationStatus == liquidationStatus);
            return liquidations.ProjectedAsCollection<LiquidationDTO>();
        }

        public async Task<LiquidationDTO> GetLiquidation(int id)
        {
            var liquidation = await _liquidationRepository.GetAsync(id);
            return liquidation.ProjectedAs<LiquidationDTO>();
        }

        // public async Task<LiquidationDTO> AddAsync(LiquidationDTO liquidationDTO)
        // {
        //     var liquidation = new Liquidation(liquidationDTO.Amount, liquidation.DealerId);
        //     _liquidationRepository.Add(liquidation);
        //     await _liquidationRepository.UnitOfWork.CommitAsync();
        //     return liquidation.ProjectedAs<LiquidationDTO>();
        // }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _liquidationRepository.Dispose();
        }
        #endregion
    }
}

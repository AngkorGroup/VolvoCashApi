using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Liquidations;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class LiquidationProfile : Profile
    {
        public LiquidationProfile()
        {
            CreateMap<Liquidation, LiquidationDTO>().PreserveReferences();
            CreateMap<Liquidation, LiquidationListDTO>().PreserveReferences();
        }
    }
}

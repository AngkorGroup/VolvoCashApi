using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class DealerProfile : Profile
    {
        public DealerProfile()
        {
            CreateMap<Dealer, DealerDTO>().PreserveReferences();
            CreateMap<DealerDTO, Dealer>().PreserveReferences();
        }
    }
}

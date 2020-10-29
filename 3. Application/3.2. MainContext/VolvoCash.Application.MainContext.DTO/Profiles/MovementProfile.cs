using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Movements;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class MovementProfile : Profile
    {
        public MovementProfile()
        {
            CreateMap<Movement, MovementDTO>().PreserveReferences();
        }
    }
}

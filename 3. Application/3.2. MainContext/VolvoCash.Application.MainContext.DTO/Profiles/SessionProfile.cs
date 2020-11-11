using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Sessions;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<Session, SessionDTO>().PreserveReferences();
        }
    }
}

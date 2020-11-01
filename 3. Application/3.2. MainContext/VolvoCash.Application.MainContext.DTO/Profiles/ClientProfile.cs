using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDTO>().PreserveReferences();
            CreateMap<Client, ClientListDTO>().PreserveReferences();
            CreateMap<Client, ClientFilterDTO>().PreserveReferences();
        }
    }
}

using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDTO>().PreserveReferences();
            CreateMap<Contact, ContactListDTO>().PreserveReferences();
        }
    }
}

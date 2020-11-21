using System;
using VolvoCash.Application.MainContext.DTO.ContactTypes;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;

namespace VolvoCash.Application.MainContext.ContactTypes.Services
{
    public interface IContactTypeAppService:IService<ContactType,ContactTypeDTO>,IDisposable
    {
    }
}

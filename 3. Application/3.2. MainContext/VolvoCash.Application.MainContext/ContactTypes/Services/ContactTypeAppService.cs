using System;
using System.Collections.Generic;
using System.Text;
using VolvoCash.Application.MainContext.DTO.ContactTypes;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;

namespace VolvoCash.Application.MainContext.ContactTypes.Services
{
    public class ContactTypeAppService:Service<ContactType,ContactTypeDTO>,IContactTypeAppService
    {
        #region Constructor
        public ContactTypeAppService(IContactTypeRepository contactTypeRepository):base(contactTypeRepository)
        {
        }
        #endregion
    }
}

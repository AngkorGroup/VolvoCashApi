using System;
using System.Collections.Generic;
using System.Text;
using VolvoCash.Application.MainContext.DTO.UserTypes;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Application.MainContext.UserTypes.Services
{
    public class UserTypeAppService:Service<UserType,UserTypeDTO>,IUserTypeAppService
    {
        #region Constructor
        public UserTypeAppService(IUserTypeRepository userTypeRepository):base(userTypeRepository)
        {
        }
        #endregion
    }
}

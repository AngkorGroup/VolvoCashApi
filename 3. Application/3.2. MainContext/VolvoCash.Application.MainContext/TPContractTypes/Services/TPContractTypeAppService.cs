using System;
using System.Collections.Generic;
using System.Text;
using VolvoCash.Application.MainContext.DTO.TPContractTypes;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.TPContractTypeAgg;

namespace VolvoCash.Application.MainContext.TPContractTypes.Services
{
    public class TPContractTypeAppService:Service<TPContractType,TPContractTypeDTO>,ITPContractTypeAppService
    {
        #region Constructor
        public TPContractTypeAppService(ITPContractTypeRepository tPContractTypeRepository) : base(tPContractTypeRepository)
        {
        }
        #endregion
    }
}

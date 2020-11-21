using System;
using System.Collections.Generic;
using System.Text;
using VolvoCash.Application.MainContext.DTO.MovementTypes;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.MovementTypes.Services
{
    public class MovementTypeAppService : Service<MovementType, MovementTypeDTO>, IMovementTypeAppService
    {
        #region Constructor
        public MovementTypeAppService(IMovementTypeRepository movementTypeRepository):base(movementTypeRepository)
        {
        }
        #endregion
    }
}

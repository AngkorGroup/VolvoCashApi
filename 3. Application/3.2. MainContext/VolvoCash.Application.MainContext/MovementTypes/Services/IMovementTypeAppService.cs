using System;
using VolvoCash.Application.MainContext.DTO.MovementTypes;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.MovementTypes.Services
{
    public interface IMovementTypeAppService:IService<MovementType,MovementTypeDTO>,IDisposable
    {
    }
}

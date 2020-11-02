using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Movements;

namespace VolvoCash.Application.MainContext.Movements.Services
{
    public interface IMovementAppService : IDisposable
    {
        #region ApiWeb 
        Task<List<MovementDTO>> GetMovementsByCardId(int cardId);
        #endregion
    }
}

using System;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VolvoCash.Application.MainContext.CardTypes.Services
{
    public interface ICardTypeAppService : IService<CardType, CardTypeDTO>, IDisposable
    {
        #region ApiWeb
        Task<List<CardTypeDTO>> GetCardTypes(bool onlyActive);
        Task Delete(int id);
        #endregion
    }
}

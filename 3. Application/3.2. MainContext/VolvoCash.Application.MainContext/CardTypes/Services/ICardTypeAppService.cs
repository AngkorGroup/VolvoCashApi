using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.CardTypes;

namespace VolvoCash.Application.MainContext.CardTypes.Services
{
    public interface ICardTypeAppService : IDisposable
    {
        #region ApiWeb
        Task<List<CardTypeDTO>> GetCardTypes(bool onlyActive);

        Task<CardTypeDTO> AddAsync(CardTypeDTO cardTypeDTO);

        Task<CardTypeDTO> ModifyAsync(CardTypeDTO cardTypeDTO);

        Task Delete(int id);
        #endregion
    }
}

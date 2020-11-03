using System;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Application.MainContext.DTO.CardTypes;

namespace VolvoCash.Application.MainContext.CardTypes.Services
{
    public interface ICardTypeAppService : IService<CardType, CardTypeDTO>, IDisposable
    {
    }
}

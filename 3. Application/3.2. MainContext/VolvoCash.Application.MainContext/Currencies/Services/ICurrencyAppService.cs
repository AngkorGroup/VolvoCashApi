using System;
using VolvoCash.Application.MainContext.DTO.Currencies;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;

namespace VolvoCash.Application.MainContext.Currencies.Services
{
    public interface ICurrencyAppService:IService<Currency,CurrencyDTO>,IDisposable
    {
    }
}

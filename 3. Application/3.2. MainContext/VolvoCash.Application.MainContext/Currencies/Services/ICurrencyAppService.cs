using System;
using System.Collections.Generic;
using System.Text;
using VolvoCash.Application.MainContext.DTO.Currencies;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.EnumAgg;

namespace VolvoCash.Application.MainContext.Currencies.Services
{
    public interface ICurrencyAppService:IService<Currency,CurrencyDTO>,IDisposable
    {
        
    }
}

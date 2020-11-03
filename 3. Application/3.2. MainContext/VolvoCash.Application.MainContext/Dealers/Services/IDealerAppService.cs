using System;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;

namespace VolvoCash.Application.MainContext.Dealers.Services
{
    public interface IDealerAppService : IService<Dealer, DealerDTO>, IDisposable
    {
    }
}

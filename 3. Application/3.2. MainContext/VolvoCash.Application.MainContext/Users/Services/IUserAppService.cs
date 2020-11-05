using System;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Application.MainContext.DTO.Users;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Application.MainContext.Users.Services
{
    public interface IUserAppService : IService<User, UserDTO>, IDisposable
    {
    }
}

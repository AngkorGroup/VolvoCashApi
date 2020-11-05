using Microsoft.Extensions.Logging;
using VolvoCash.Application.MainContext.DTO.Users;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Application.MainContext.Users.Services
{
    public class UserAppService : Service<User, UserDTO>, IUserAppService
    {
        #region Members
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public UserAppService(IUserRepository userRepository,
                              ILogger<UserAppService> logger) : base(userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion
    }
}

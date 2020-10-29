using VolvoCash.Application.MainContext.DTO.Admins;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Responses.Authentication
{
    public class LoginResponse
    {
        #region Properties
        public AdminDTO Admin { get; set; }

        public string AuthToken { get; set; }
        #endregion
    }
}

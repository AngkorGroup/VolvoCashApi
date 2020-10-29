using System.ComponentModel.DataAnnotations;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Authentication
{
    public class LoginRequest
    {
        #region Properties
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        #endregion
    }
}

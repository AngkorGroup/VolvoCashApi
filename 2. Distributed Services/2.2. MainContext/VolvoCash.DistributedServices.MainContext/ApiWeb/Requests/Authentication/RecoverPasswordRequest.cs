using System.ComponentModel.DataAnnotations;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Authentication
{
    public class RecoverPasswordRequest
    {
        #region Properties
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Token { get; set; }
        #endregion
    }
}

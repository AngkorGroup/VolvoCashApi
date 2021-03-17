using System.ComponentModel.DataAnnotations;

namespace VolvoCash.DistributedServices.MainContext.ApiPOS.Requests.Authentication
{
    public class ForgotPasswordRequest
    {
        #region Properties
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Code { get; set; }
        #endregion
    }
}

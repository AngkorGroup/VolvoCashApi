using System.ComponentModel.DataAnnotations;

namespace VolvoCash.DistributedServices.MainContext.ApiPOS.Requests.Authentication
{
    public class ChangePasswordRequest
    {
        #region Properties
        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
        #endregion
    }
}

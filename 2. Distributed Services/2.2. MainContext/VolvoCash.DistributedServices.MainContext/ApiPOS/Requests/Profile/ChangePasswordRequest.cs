using System.ComponentModel.DataAnnotations;

namespace VolvoCash.DistributedServices.MainContext.ApiPOS.Requests.Authentication
{
    public class ChangePasswordRequest
    {
        #region Properties
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmNewPassword { get; set; }
        #endregion
    }
}

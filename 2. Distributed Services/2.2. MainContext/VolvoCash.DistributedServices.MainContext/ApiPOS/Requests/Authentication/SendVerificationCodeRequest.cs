using System.ComponentModel.DataAnnotations;

namespace VolvoCash.DistributedServices.MainContext.ApiPOS.Requests.Authentication
{
    public class SendVerificationCodeRequest
    {
        #region Properties
        [Required]
        public string Email { get; set; }

        #endregion
    }
}

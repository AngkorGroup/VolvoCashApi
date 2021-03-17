using System.ComponentModel.DataAnnotations;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Authentication
{
    public class RequestRecoverPasswordRequest
    {
        #region Properties
        [Required]
        public string Email { get; set; }

        #endregion
    }
}

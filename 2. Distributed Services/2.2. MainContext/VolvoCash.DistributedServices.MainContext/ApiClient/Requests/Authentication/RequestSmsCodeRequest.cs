using System.ComponentModel.DataAnnotations;

namespace VolvoCash.DistributedServices.MainContext.ApiClient.Requests.Authentication
{
    public class RequestSmsCodeRequest
    {
        #region Properties
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }
        #endregion
    }
}

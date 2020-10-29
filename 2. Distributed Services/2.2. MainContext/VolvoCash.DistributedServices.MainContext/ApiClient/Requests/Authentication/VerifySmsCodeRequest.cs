using System.ComponentModel.DataAnnotations;

namespace VolvoCash.DistributedServices.MainContext.ApiClient.Requests.Authentication
{
    public class VerifySmsCodeRequest
    {
        #region Properties
        [Required]
        [MaxLength(4)]
        public string Code { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(300)]
        public string DeviceToken { get; set; }
        #endregion
    }
}

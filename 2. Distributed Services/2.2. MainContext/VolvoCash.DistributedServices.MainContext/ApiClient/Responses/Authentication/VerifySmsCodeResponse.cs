using VolvoCash.Application.MainContext.DTO.Contacts;

namespace VolvoCash.DistributedServices.MainContext.ApiClient.Responses.Authentication
{
    public class VerifySmsCodeResponse
    {
        #region Properties
        public ContactListDTO Contact { get; set; }

        public string AuthToken { get; set; }
        #endregion
    }
}

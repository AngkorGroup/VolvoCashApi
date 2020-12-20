namespace VolvoCash.DistributedServices.MainContext.ApiClient.Responses.Authentication
{
    public class RequestSmsCodeResponse
    {
        #region Properties
        public bool IsValidPhoneNumber { get; set; }
        public int SmsCode { get; set; }
        #endregion
    }
}

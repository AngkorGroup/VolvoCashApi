using VolvoCash.Application.MainContext.DTO.Cashiers;

namespace VolvoCash.DistributedServices.MainContext.ApiPOS.Responses.Authentication
{
    public class LoginResponse
    {
        #region Properties
        public CashierDTO Cashier { get; set; }

        public string AuthToken { get; set; }
        #endregion
    }
}

using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class ClientsCardExpirationReportRequest : ReportRequest
    {
        #region Properties
        public List<string> CardTypes { get; set; }

        public string ClientId { get; set; }
        #endregion
    }
}

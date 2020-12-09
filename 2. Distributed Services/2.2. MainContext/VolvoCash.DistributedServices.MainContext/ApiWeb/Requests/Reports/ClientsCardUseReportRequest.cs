using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class ClientsCardUseReportRequest : ReportRequest
    {
        #region Properties
        public List<string> CardTypes { get; set; }

        public string ClientId { get; set; }
        
        public string StartDate { get; set; }

        public string EndDate { get; set; }
        #endregion
    }
}

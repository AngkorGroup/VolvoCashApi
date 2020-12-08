using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class ConsumesByClientReportRequest : ReportRequest
    {
        #region Properties
        public string ClientId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<string> CardTypes { get; set; }
        #endregion
    }
}

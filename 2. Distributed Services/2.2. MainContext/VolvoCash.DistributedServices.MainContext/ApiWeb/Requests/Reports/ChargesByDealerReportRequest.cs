using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class ChargesByDealerReportRequest : ReportRequest
    {
        #region Properties
        public string DealerId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<string> CardTypes { get; set; }
        #endregion
    }
}

using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class ConsumesRankingReportRequest : ReportRequest
    {
        #region Properties
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<string> CardTypes { get; set; }
        #endregion
    }
}

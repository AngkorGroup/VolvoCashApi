using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class ConsumesByEconomicReportRequest : ReportRequest
    {
        #region Properties
        public List<string> CardTypes { get; set; }

        public List<string> Sectors { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
        #endregion
    }
}

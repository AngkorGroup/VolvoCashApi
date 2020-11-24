using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class ConsumesByEconomicReportRequest
    {
        #region Properties
        public string Type { get; set; }

        public List<string> CardTypes { get; set; }

        public List<string> Sectors { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
        #endregion
    }
}

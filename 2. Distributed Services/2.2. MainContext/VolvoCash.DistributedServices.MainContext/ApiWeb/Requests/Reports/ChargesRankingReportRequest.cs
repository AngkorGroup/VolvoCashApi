using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class ChargesRankingReportRequest
    {
        #region Properties
        public string Type { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<string> CardTypes { get; set; }

        public bool EconomicGroup { get; set; }
        #endregion
    }
}

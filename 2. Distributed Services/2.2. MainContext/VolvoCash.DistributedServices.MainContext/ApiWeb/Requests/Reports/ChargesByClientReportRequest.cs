using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class ChargesByClientReportRequest : ReportRequest
    {
        #region Properties
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<string> CardTypes { get; set; }

        public List<string> BusinessAreas { get; set; }

        public List<string> ChargeTypes { get; set; }

        public string ClientId { get; set; }
        #endregion
    }
}

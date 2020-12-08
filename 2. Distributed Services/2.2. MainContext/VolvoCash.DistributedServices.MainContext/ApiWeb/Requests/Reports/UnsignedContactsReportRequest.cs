using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class UnsignedContactsReportRequest : ReportRequest
    {
        #region Properties
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<string> Clients { get; set; }
        #endregion
    }
}

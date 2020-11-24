using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class PendingChargesRefundReportRequest
    {
        #region Properties
        public string Type { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string DealerId { get; set; }
        #endregion
    }
}

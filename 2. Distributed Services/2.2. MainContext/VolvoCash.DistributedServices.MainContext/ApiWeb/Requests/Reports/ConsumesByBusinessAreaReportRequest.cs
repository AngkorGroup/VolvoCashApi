using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class ConsumesByBusinessAreaReportRequest : ReportRequest
    {
        #region Properties
        public List<string> CardTypes { get; set; }

        public List<string> BusinessAreas { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
        #endregion
    }
}

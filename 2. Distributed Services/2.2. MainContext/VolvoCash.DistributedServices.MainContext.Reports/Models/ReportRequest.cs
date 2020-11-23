using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.Reports.Models
{
    public class ReportRequest
    {
        #region Properties
        public string ReportPath { get; set; }

        public List<ReportParameter> Parameters { get; set; }

        public string Extension { get; set; }
        #endregion

        #region Constructor
        public ReportRequest()
        {
        }
        #endregion
    }
}

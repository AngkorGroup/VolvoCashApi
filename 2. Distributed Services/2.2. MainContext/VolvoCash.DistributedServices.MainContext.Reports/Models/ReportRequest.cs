using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.Reports.Models
{
    public class ReportRequest
    {
        public string ReportPath { get; set; }

        public List<ReportParameter> Parameters { get; set; }

        public string Extension { get; set; }

        public ReportRequest()
        {
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.CrossCutting.NetFramework.Utils;
using VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/recharge_types")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ReportsController : ControllerBase
    {
        #region Members
        private readonly IReportManager _reportManager;
        #endregion

        #region Constructor
        public ReportsController(IReportManager reportManager) 
        {
            _reportManager = reportManager;
        }
        #endregion

        #region Private Methods
        private FileContentResult GetFileResponse(byte[] reportContent, string reportType, string fileName)
        {
            var extension = ContentAndExtensions.excelExtension;
            var mimeType = ContentAndExtensions.excelContentType;
            if (reportType == "pdf")
            {
                mimeType = ContentAndExtensions.pdfContentType;
                extension = ContentAndExtensions.pdfExtension;
            }
            return File(reportContent, mimeType, fileName + extension);
        }
        #endregion

        #region Public Methods
        [HttpPost("consumes_by_client")]
        public async Task<IActionResult> GetConsumeByClientReport([FromBody] ConsumesByClientReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension  = reportRequest.Type,
                Parameters = new List<ReportParameter>() { 
                    new ReportParameter("p_clients", reportRequest.ClientId, 8),
                    new ReportParameter("p_card_types", reportRequest.CardTypes, 4),
                    new ReportParameter("p_begin_date", reportRequest.StartDate),
                    new ReportParameter("p_end_date", reportRequest.EndDate)
                },
                Path = "ConsumptionPerCustomer"
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteDeConsumosPorCliente";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }       
        #endregion
    }
}

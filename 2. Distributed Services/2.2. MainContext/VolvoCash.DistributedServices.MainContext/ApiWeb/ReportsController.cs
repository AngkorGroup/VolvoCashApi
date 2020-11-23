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
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ReportsController : Controller
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
        private IActionResult GetFileResponse(byte[] reportContent, string reportType, string fileName)
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
                Extension = reportRequest.Type,
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

        [HttpPost("charges_by_dealer")]
        public async Task<IActionResult> GetChargesByDealerReport([FromBody] ChargesByDealerReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension = reportRequest.Type,
                Parameters = new List<ReportParameter>() {
                    new ReportParameter("p_dealers", reportRequest.DealerId, 4),
                    new ReportParameter("p_card_types", reportRequest.CardTypes, 4),
                    new ReportParameter("p_begin_date", reportRequest.StartDate),
                    new ReportParameter("p_end_date", reportRequest.EndDate)
                },
                Path = "ChargesDealerAndEconomicGroup"
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteDeConsumosPorDealer";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }

        [HttpPost("consumes_ranking")]
        public async Task<IActionResult> GetConsumesRankingReport([FromBody] ConsumesRankingReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension = reportRequest.Type,
                Parameters = new List<ReportParameter>() {
                    new ReportParameter("p_card_types", reportRequest.CardTypes, 4),
                    new ReportParameter("p_begin_date", reportRequest.StartDate),
                    new ReportParameter("p_end_date", reportRequest.EndDate)
                },
                Path = "RankingConsumptionTypeCardCustomers"
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteDeRankingPorCliente";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }

        [HttpPost("charges_ranking")]
        public async Task<IActionResult> GetChargesRankingReport([FromBody] ChargesRankingReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension = reportRequest.Type,
                Parameters = new List<ReportParameter>() {
                    new ReportParameter("p_card_types", reportRequest.CardTypes, 4),
                    new ReportParameter("p_begin_date", reportRequest.StartDate),
                    new ReportParameter("p_end_date", reportRequest.EndDate),
                     new ReportParameter("p_flag", reportRequest.EconomicGroup ? "1" : "0")
                },
                Path = "RankingOfCollectionsByDealers"
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteDeRankingPorDealer";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }

        [HttpPost("charges_by_client")]
        public async Task<IActionResult> GetChargesByClientReport([FromBody] ChargesByClientReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension = reportRequest.Type,
                Parameters = new List<ReportParameter>() {
                    new ReportParameter("p_clients", reportRequest.ClientId, 8),
                    new ReportParameter("p_begin_date", reportRequest.StartDate),
                    new ReportParameter("p_end_date", reportRequest.EndDate),
                    new ReportParameter("p_card_types", reportRequest.CardTypes, 4),
                    new ReportParameter("p_business_areas", reportRequest.BusinessAreas, 4),
                    new ReportParameter("p_recharge_types", reportRequest.ChargeTypes, 4),
                },
                Path = "RefillsByClientAndBusinessArea"
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteDeRankingPorCliente";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }

        [HttpPost("clients_card_use")]
        public async Task<IActionResult> GetClientsCardUseReport([FromBody] ClientsCardUseReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension = reportRequest.Type,
                Parameters = new List<ReportParameter>() {
                    new ReportParameter("p_clients", reportRequest.ClientId, 8),
                    new ReportParameter("p_card_types", reportRequest.CardTypes, 4)
                },
                Path = "ClientsMatrixvsCardUsage"
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteDeClientesUsoTarjeta";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }

        [HttpPost("clients_card_expiration")]
        public async Task<IActionResult> GetClientsCardExpirationReport([FromBody] ClientsCardExpirationReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension = reportRequest.Type,
                Parameters = new List<ReportParameter>() {
                    new ReportParameter("p_clients", reportRequest.ClientId, 8),
                    new ReportParameter("p_card_types", reportRequest.CardTypes, 4)
                },
                Path = "ClientsMatrixCardsClosestToExpire"
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteDeTarjetasVencimientos";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }

        [HttpPost("consumes_by_business_area")]
        public async Task<IActionResult> GetConsumesByBusinessAreaReport([FromBody] ConsumesByBusinessAreaReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension = reportRequest.Type,
                Parameters = new List<ReportParameter>() {
                    new ReportParameter("p_business_areas", reportRequest.BusinessAreas, 4),
                    new ReportParameter("p_card_types", reportRequest.CardTypes, 4),
                    new ReportParameter("p_begin_date", reportRequest.StartDate),
                    new ReportParameter("p_end_date", reportRequest.EndDate),
                },
                Path = "ConsumptionStatisticsByBusinessArea"
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteDeConsumosAreaNegocio";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }

        [HttpPost("consumes_by_economic")]
        public async Task<IActionResult> GetConsumesByEconomicReport([FromBody] ConsumesByEconomicReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension = reportRequest.Type,
                Parameters = new List<ReportParameter>() {
                    new ReportParameter("p_sectors", reportRequest.Sectors, 4),
                    new ReportParameter("p_card_types", reportRequest.CardTypes, 4),
                    new ReportParameter("p_begin_date", reportRequest.StartDate),
                    new ReportParameter("p_end_date", reportRequest.EndDate),
                },
                Path = "ConsumptionStatisticsbyClientEconomicSector"
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteDeConsumosSector";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }

        [HttpPost("client_consumes_by_dealer")]
        public async Task<IActionResult> GetClientConsumesByDealerReport([FromBody] ClientConsumesByDealerReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension = reportRequest.Type,
                Parameters = new List<ReportParameter>() {
                    new ReportParameter("p_clients", reportRequest.ClientId, 8),
                    new ReportParameter("p_card_types", reportRequest.CardTypes, 4),
                    new ReportParameter("p_begin_date", reportRequest.StartDate),
                    new ReportParameter("p_end_date", reportRequest.EndDate),
                },
                Path = "CustomerConsumptionMatrixbyDealer"
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteConsumoClienteDealer";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }

        [HttpPost("refunds")]
        public async Task<IActionResult> GetRefundsReport([FromBody] RefundsReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension = reportRequest.Type,
                Parameters = new List<ReportParameter>() {
                    new ReportParameter("p_dealers", reportRequest.DealerId, 4),
                    new ReportParameter("p_banks", reportRequest.Banks, 4),
                    new ReportParameter("p_begin_date", reportRequest.StartDate),
                    new ReportParameter("p_end_date", reportRequest.EndDate),
                },
                Path = ""// Falta poner el nombre del reporte
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteReembolsos";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }

        [HttpPost("pending_charges_refund")]
        public async Task<IActionResult> GetPendingChargesRefundReport([FromBody] PendingChargesRefundReportRequest reportRequest)
        {
            var report = new CustomReport
            {
                Extension = reportRequest.Type,
                Parameters = new List<ReportParameter>() {
                    new ReportParameter("p_dealers", reportRequest.DealerId, 4),
                    new ReportParameter("p_begin_date", reportRequest.StartDate),
                    new ReportParameter("p_end_date", reportRequest.EndDate),
                },
                Path = "CollectionsPendingReimbursement"
            };
            var reportContent = await _reportManager.GetReportAsync(report);
            var fileName = "ReporteCobrosPendReembolso";
            return GetFileResponse(reportContent, reportRequest.Type, fileName);
        }
        #endregion
    }
}

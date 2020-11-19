using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Web.Http;
using VolvoCash.DistributedServices.MainContext.Reports.Models;

namespace VolvoCash.DistributedServices.MainContext.Reports.Controllers
{

    public class ReportsController : ApiController
    {
        private Stream GetReportStream(ReportRequest reportRequest)
        {

            var reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Reporting" + reportRequest.ReportPath));
            var rpt = new ReportDocument();
            rpt.Load(reportPath);
            rpt.DataSourceConnections[0].SetConnection(ConfigurationManager.AppSettings["O7_DB_CONNECTION_SERVER"],
                                                        "",
                                                        ConfigurationManager.AppSettings["O7_DB_CONNECTION_USER"],
                                                        ConfigurationManager.AppSettings["O7_DB_CONNECTION_PASSWORD"]);
            foreach (var parameter in reportRequest.Parameters)
            {
                try
                {
                    rpt.SetParameterValue(parameter.Name, parameter.Value);
                }
                catch (Exception)
                {
                    try
                    {
                        rpt.SetParameterValue(parameter.Name, "");
                    }
                    catch (Exception)
                    {
                        rpt.SetParameterValue(parameter.Name, 0);
                    }

                }
            }
            var formatToExport = reportRequest.Extension == "pdf" ? ExportFormatType.PortableDocFormat : ExportFormatType.Excel;
            var stream = rpt.ExportToStream(formatToExport);
            stream.Seek(0, SeekOrigin.Begin);
            rpt.Close();
            rpt.Dispose();
            return stream;
        }
        public byte[] ToByteArray(Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }
        [HttpGet]
        public IHttpActionResult GetReportRPT(string _reportRequest)
        {
            try
            {
                var reportRequest = JsonConvert.DeserializeObject<ReportRequest>(_reportRequest);
                var stream = GetReportStream(reportRequest);
                byte[] bytes = ToByteArray(stream);
                var content = Convert.ToBase64String(bytes);
                return Ok(new ReportFileResponse(true, content));

            }
            catch (Exception e)
            {
                return Ok(new ReportFileResponse(false, e.Message +
                    "======================" +
                    e.StackTrace +
                     "======================" +
                     e.InnerException.Message +
                     "======================" +
                    e.InnerException.StackTrace +
                     "======================" +
                     e.InnerException.InnerException.Message +
                     "======================" +
                     e.InnerException.InnerException.StackTrace));
            }

        }

    }
}

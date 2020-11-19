using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public class ReportManager : IReportManager
    {
        #region Members
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public ReportManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        public async Task<byte[]> GetReportAsync(CustomReport report)
        {
            var reportPath = "/" + report.Path + ".rpt";
            var parameters = report.Parameters;

            parameters.Add(new ReportParameter("p_company", "001"));
            parameters.Add(new ReportParameter("p_branch", "001"));
            parameters.Add(new ReportParameter("p_user", "Usuario"));

            var url = _configuration["ReportServiceUrl"];
            var reportRequest = JsonConvert.SerializeObject(new { ReportPath = reportPath, Parameters = parameters, Extension = report.Extension });
            url = url + "?_reportRequest=" + reportRequest;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Timeout = 600000;

            var response = await request.GetResponseAsync();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var obj = JsonConvert.DeserializeObject<ReportFileResponse>(responseString);

            if (obj.Success)
            {
                return Convert.FromBase64String(obj.Content);
            }

            throw new InvalidOperationException(obj.Message);
        }
        #endregion
    }

    public class ReportFileResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Content { get; set; }
    }

    public class ReportParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ReportParameter(string key, string value, int padLength = 0)
        {
            Key = key;
            Value = value == "all" ? "" : value.PadLeft(padLength);
        }

        public ReportParameter(string key, List<string> values, int padLength = 0)
        {
            Key = key;
            var padValues = new List<string>();
            foreach (var value in values)
            {
                padValues.Add(value == "all" ? "" : value.PadLeft(padLength));
            }
            Value = string.Join(",", padValues);
        }
    }

    public class CustomReport
    {
        public string Path { get; set; }
        public List<ReportParameter> Parameters { get; set; }
        public string Extension { get; set; }
    }

    public static class ContentAndExtensions
    {
        public static string pdfContentType = "application/pdf";
        public static string pdfExtension = ".pdf";

        public static string excelContentType = "application/vnd.ms-excel";
        public static string excelExtension = ".xls";
    }
}

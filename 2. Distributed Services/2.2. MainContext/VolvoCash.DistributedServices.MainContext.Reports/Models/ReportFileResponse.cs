namespace VolvoCash.DistributedServices.MainContext.Reports.Models
{
    public class ReportFileResponse
    {
        public string Content { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public ReportFileResponse(bool success, string content)
        {
            Success = success;
            if (success)
            {
                Content = content;
            }
            else
            {
                Message = content;
            }
        }
    }
}

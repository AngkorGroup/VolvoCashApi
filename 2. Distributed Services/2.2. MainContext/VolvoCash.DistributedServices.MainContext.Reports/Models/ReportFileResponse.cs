namespace VolvoCash.DistributedServices.MainContext.Reports.Models
{
    public class ReportFileResponse
    {
        #region Properties
        public string Content { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        #endregion

        #region Contructor
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
        #endregion
    }
}

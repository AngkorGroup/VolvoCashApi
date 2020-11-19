using System.Collections.Generic;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Reports
{
    public class ClientsCardUseReportRequest
	{
		#region Properties
		public string Type { get; set; }			

		public List<string> CardTypes { get; set; }

		public string ClientId { get; set; }
		#endregion
	}
}
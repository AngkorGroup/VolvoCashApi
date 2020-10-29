using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System;

namespace VolvoCash.DistributedServices.Seedwork.Utils
{
    public class PushNotificationManager 
    {
        #region Members
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public PushNotificationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        public bool Send(string deviceToken, string title, string body)
        {
            try
            {
                if (string.IsNullOrEmpty(deviceToken))
                {
                    return false;
                }

                var applicationId = _configuration["FCM:ApplicationId"];
                var senderId = _configuration["FCM:SenderId"];
                var tRequest = WebRequest.Create(_configuration["FCM:Url"]);
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new
                {
                    to = deviceToken,
                    notification = new
                    {
                        body,
                        title,
                        sound = "Enabled"
                    }
                };
                var json = JsonConvert.SerializeObject(data);
                var byteArray = Encoding.UTF8.GetBytes(json);

                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationId));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (var dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using WebResponse tResponse = tRequest.GetResponse();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

            
        }
        #endregion
    }
}

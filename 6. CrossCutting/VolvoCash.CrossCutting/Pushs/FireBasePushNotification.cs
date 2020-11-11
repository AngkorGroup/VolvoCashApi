using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace VolvoCash.CrossCutting.Pushs
{
    public class FireBasePushNotification : IPushNotificationManager
    {
        #region Members
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public FireBasePushNotification(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        public bool SendNotification(AppType appType, string deviceToken, string heading, string content, object data = null)
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

                var message = new
                {
                    to = deviceToken,
                    notification = new
                    {
                        content,
                        heading,
                        sound = "Enabled"
                    }
                };

                var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

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
            catch
            {
                return false;
            }
        }
        
        public bool SendNotifications(AppType appType, List<string> deviceTokens, string heading, string content, object data = null)
        {
            try
            {
                if (deviceTokens is null || deviceTokens.Count ==0 )
                {
                    return false;
                }

                var applicationId = _configuration["FCM:ApplicationId"];
                var senderId = _configuration["FCM:SenderId"];
                var tRequest = WebRequest.Create(_configuration["FCM:Url"]);
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var message = new
                {
                    to = deviceTokens,
                    notification = new
                    {
                        content,
                        heading,
                        sound = "Enabled"
                    }
                };

                var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

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
            catch
            {
                return false;
            }
        }
        #endregion
    }
}

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace VolvoCash.CrossCutting.Pushs
{
    public class OneSignalPushNotification : IPushNotificationManager
    {
        public class OneSignalContent
        {
            [JsonProperty("en")]
            public string En { get; set; }

            [JsonProperty("es")]
            public string Es { get; set; }
        }

        public class OneSignalMessage
        {
            [JsonProperty("app_id")]
            public string AppId { get; set; }
            [JsonProperty("contents")]
            public OneSignalContent Contents { get; set; }
            [JsonProperty("headings")]
            public OneSignalContent Headings { get; set; }
            [JsonProperty("channel_for_external_user_ids")]
            public string ChannelForExternalUserIds { get; set; }
            [JsonProperty("include_player_ids")]
            public List<string> IncludePlayerIds { get; set; }
            [JsonProperty("data")]
            public object Data { get; set; }
        }

        #region Members
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public OneSignalPushNotification(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        public bool SendNotification(AppType appType, string deviceToken, string heading, string content, object data = null)
        {
            var request = WebRequest.Create(_configuration["OneSignal:Url"]) as HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("authorization", "Basic "+ _configuration["OneSignal:"+ appType.ToString()+ "ApiKey"]);

            var oneSignalMessage = new OneSignalMessage()
            {
                AppId = _configuration["OneSignal:" + appType.ToString() + "ApplicationId"],
                Contents= new OneSignalContent{ Es =content ,En = content},
                Headings = new OneSignalContent { Es = heading, En = content },
                ChannelForExternalUserIds = "push",
                IncludePlayerIds = new List<string> { deviceToken},
                Data = data
            };

            byte[] byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(oneSignalMessage));
            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }
                var response = request.GetResponse();
            }
            catch
            {               
            }
            return false;
        }
        
        public bool SendNotifications(AppType appType, List<string> deviceTokens, string heading, string content, object data = null)
        {
           foreach(var deviceToken in deviceTokens)
           {
                SendNotification(appType, deviceToken, heading, content, data);
           }
            return true;
        }
        #endregion
    }
}

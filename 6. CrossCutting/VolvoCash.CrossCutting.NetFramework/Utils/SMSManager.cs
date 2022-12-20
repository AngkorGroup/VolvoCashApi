using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public class SMSManager : ISMSManager
    {
        #region Members
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public SMSManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        public void SendSMS(string to, string body)
        {
            try
            {
                var mockSMS = bool.Parse(_configuration["SMS:ShouldMockSMS"]);
                if (mockSMS)
                    return;

                var username = _configuration["SMS:Username"];
                var password = _configuration["SMS:Password"];
                var baseUrl = _configuration["SMS:BaseUrl"];
                var sendSmsUri = _configuration["SMS:SendSmsUri"];
                var defaultCountryCode = _configuration["SMS:DefaultCountryCode"];
                var base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));

                if (!to.Contains('+'))
                {
                    to = defaultCountryCode + to;
                }
                
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("authorization", "Basic " + base64Credentials);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                to.Replace("+", "");
                var content = new StringContent("{\"to\":[\"" + to + "\"],\"text\":\"" + body + "\"}", Encoding.UTF8, "application/json");
                var result = client.PostAsync(sendSmsUri, content).Result;
            }
            catch
            {
            }         
        }
        #endregion
    }
}

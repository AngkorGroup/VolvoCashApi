using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Configuration;

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
        public string SendSMS(string to, string body)
        {
            try
            {
                var accountSid = _configuration["TwilioSMS:AccountSid"];
                var authToken = _configuration["TwilioSMS:AuthToken"];

                TwilioClient.Init(accountSid, authToken);

                if (!to.Contains('+'))
                {
                    to = _configuration["TwilioSMS:DefaultCountryCode"] + to;
                }

                var from = _configuration["TwilioSMS:From"];

                var message = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(from),
                    to: new Twilio.Types.PhoneNumber(to)
                );

                return message.Sid;
            }
            catch
            {
                //Ignore any connectivity problem with Twilio API
            }
            return null;            
        }
        #endregion
    }
}

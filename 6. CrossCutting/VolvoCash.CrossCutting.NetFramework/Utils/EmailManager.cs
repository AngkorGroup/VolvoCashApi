using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public class EmailManager : IEmailManager
    {
        #region Members
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public EmailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        public bool SendEmail(string toStr, string subjectStr, string bodyStr)
        {
            try
            {
                var message = new MimeMessage();
                
                var from = new MailboxAddress("Volvo Cash", _configuration["Mail:From"]);
                message.From.Add(from);

                var to = new MailboxAddress("Usuario", toStr);
                message.To.Add(to);

                message.Subject = subjectStr;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = bodyStr
                };
                message.Body = bodyBuilder.ToMessageBody();

                var client = new SmtpClient();
                client.Connect(_configuration["Mail:Host"], int.Parse(_configuration["Mail:Port"]), SecureSocketOptions.StartTls);
                client.Authenticate(_configuration["Mail:User"], _configuration["Mail:Password"]);

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();

                return true;
            }
            catch
            {
                //Any exception is ignore to avoid program crash
            }
            return false;
        }
        #endregion
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.SMSCodeAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class SMSCodeRepository : Repository<SMSCode, MainDbContext>, ISMSCodeRepository
    {
        #region Members
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public SMSCodeRepository(MainDbContext dbContext,
                                  ILogger<Repository<SMSCode, MainDbContext>> logger,
                                  IConfiguration configuration) : base(dbContext, logger)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        public async Task<int> GenerateSMSCodeAsync(string phone)
        {
            var code = int.Parse(RandomGenerator.RandomDigits(4));
            var expiresAt = int.Parse(_configuration["SMS:ExpiresMinutes"]);
            var smsCode = new SMSCode()
            {
                Code = code,
                Phone = phone,
                ExpiresAt = DateTime.Now.AddMinutes(expiresAt)
            };
            Add(smsCode);
            await _context.CommitAsync();
            return code;
        }

        public async Task<SMSCode> VerifyCodeAsync(string phone, int code)
        {
            var smsCode = _context.SMSCodes.Where((smsCode) => smsCode.Phone == phone
                                                && smsCode.Code == code
                                                && !smsCode.ItWasAlreadyUsed).FirstOrDefault();
            //&& smsCode.ExpiresAt < DateTime.Now).FirstOrDefault();
            if (smsCode != null)
            {
                smsCode.ItWasAlreadyUsed = true;
                smsCode.DateTimeUsed = DateTime.Now;
                await _context.CommitAsync();
            }
            return smsCode;
        }
        #endregion
    }
}

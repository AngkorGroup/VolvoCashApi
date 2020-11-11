using Microsoft.Extensions.Configuration;
using System;

namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public class UrlManager : IUrlManager
    {
        #region Members
        private readonly IConfiguration _configuration;
        private readonly string _baseApiUrl;
        #endregion

        #region Constructor
        public UrlManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseApiUrl = _configuration["Application:BaseApiUrl"];
        }
        #endregion

        #region Public Methods
        public string GetQrUrl(object obj)
        {
            try
            {
                return _baseApiUrl + _configuration["Application:QrUrl"] + CardTokenizer.GetCardToken(obj);
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public string GetChargeVoucherHtmlUrl(int id)
        {
            try
            {
                return _baseApiUrl + string.Format( _configuration["Application:ChargeHtmlUrl"] , id);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string GetTransferVoucherHtmlUrl(int id)
        {
            try
            {
                return _baseApiUrl + string.Format(_configuration["Application:TransferHtmlUrl"], id);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string GetChargeVoucherImageUrl(int id)
        {
            try
            {
                return _baseApiUrl + string.Format(_configuration["Application:ChargeImageUrl"], id);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string GetTransferVoucherImageUrl(int id)
        {
            try
            {
                return _baseApiUrl + string.Format(_configuration["Application:TransferImageUrl"], id);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }        
        #endregion
    }
}

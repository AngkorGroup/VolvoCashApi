using Microsoft.Extensions.Configuration;
using System;

namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public class UrlManager : IUrlManager
    {
        #region Members
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;
        #endregion

        #region Constructor
        public UrlManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["Application:BaseUrl"];
        }
        #endregion

        #region Public Methods
        public string GetQrUrl(object obj)
        {
            try
            {
                return _baseUrl + _configuration["Application:QrUrl"] + CardTokenizer.GetCardToken(obj);
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public string GetChargeVoucherHtmlUrl(int id, string operationCode)
        {
            try
            {
                return _baseUrl + string.Format( _configuration["Application:ChargeHtmlUrl"] , id, operationCode);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string GetTransferVoucherHtmlUrl(int id, string operationCode)
        {
            try
            {
                return _baseUrl + string.Format(_configuration["Application:TransferHtmlUrl"], id, operationCode);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string GetChargeVoucherImageUrl(int id,string operationCode)
        {
            try
            {
                return _baseUrl + string.Format(_configuration["Application:ChargeImageUrl"], id, operationCode);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string GetTransferVoucherImageUrl(int id,string operationCode)
        {
            try
            {
                return _baseUrl + string.Format(_configuration["Application:TransferImageUrl"], id, operationCode);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }        
        #endregion
    }
}

using Newtonsoft.Json;
using System;
using VolvoCash.CrossCutting.Utils;

namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public static class CardTokenizer
    {

        #region Public Methods
        public static string GetCardToken(object obj)
        {
            try
            {
                var objEncoded = CryptoMethods.EncryptString(JsonConvert.SerializeObject(obj));
                return objEncoded;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion
    }
}

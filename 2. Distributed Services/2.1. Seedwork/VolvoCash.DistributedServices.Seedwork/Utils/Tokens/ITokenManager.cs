using System;

namespace VolvoCash.DistributedServices.Seedwork.Utils
{
    public interface ITokenManager 
    {
        #region Public Methods
        string GenerateTokenJWT(Guid sessionId, int userId, string username, string names, string email, string role);       
        #endregion
    }
}

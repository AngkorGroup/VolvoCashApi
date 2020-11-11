using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace VolvoCash.CrossCutting.NetFramework.Identity
{
    public class ApplicationUser : IApplicationUser
    {
        #region Members
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public ApplicationUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Public Methods
        public int GetUserId()
        {
            try
            {
                var subject = _httpContextAccessor.HttpContext
                             .User.Claims
                             .FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                return int.TryParse(subject.Value, out var id) ? id : 0;
            }
            catch
            {
                return 0;
            }
        }

        public string GetUserName()
        {
            try
            {
                return _httpContextAccessor.HttpContext
                             .User.Claims
                             .FirstOrDefault(claim => claim.Type == JwtClaimTypes.PreferredUserName).Value;
            }
            catch
            {
                return "";
            }
        }

        public string GetName()
        {
            try
            {
                return _httpContextAccessor.HttpContext
                             .User.Claims
                             .FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value;
            }
            catch
            {
                return "";
            }
        }

        public Guid GetSessionId()
        {
            var sessionId = _httpContextAccessor.HttpContext
                         .User.Claims
                         .FirstOrDefault(claim => claim.Type == JwtClaimTypes.JwtId).Value;
            return Guid.Parse(sessionId);
        }
        #endregion
    }
}
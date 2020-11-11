using System;

namespace VolvoCash.CrossCutting.NetFramework.Identity
{
    public interface IApplicationUser
    {
        int GetUserId();

        string GetUserName();

        string GetName();

        Guid GetSessionId();
    }
}

using System.Collections.Generic;

namespace VolvoCash.CrossCutting.Pushs
{
    public interface IPushNotificationManager 
    {
        #region Public Methods
        bool SendNotification(AppType appType, string deviceToken, string heading, string content, object data = null);
        bool SendNotifications(AppType appType, List<string> deviceTokens, string heading, string content, object data = null);
        #endregion
    }
}

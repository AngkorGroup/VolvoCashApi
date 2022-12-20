namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public interface ISMSManager
    {
        void SendSMS(string to, string body);
    }
}

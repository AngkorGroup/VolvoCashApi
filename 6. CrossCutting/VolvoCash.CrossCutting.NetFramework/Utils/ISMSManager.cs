namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public interface ISMSManager
    {
        string SendSMS(string to, string body);
    }
}

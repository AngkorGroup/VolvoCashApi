namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public interface IEmailManager
    {
        bool SendEmail(string toStr, string subjectStr, string bodyStr);
    }
}

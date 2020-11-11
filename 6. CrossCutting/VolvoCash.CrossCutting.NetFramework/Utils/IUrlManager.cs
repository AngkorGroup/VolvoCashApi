namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public interface IUrlManager
    {
        string GetQrUrl(object obj);
        string GetChargeVoucherHtmlUrl(int id);
        string GetTransferVoucherHtmlUrl(int id);
        string GetChargeVoucherImageUrl(int id);
        string GetTransferVoucherImageUrl(int id);
    }
}

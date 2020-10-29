using Microsoft.Extensions.Configuration;
using System;

namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public interface IUrlManager
    {
        string GetQrUrl(object obj);
        string GetChargeVoucherHtmlUrl(int id, string operationCode);
        string GetTransferVoucherHtmlUrl(int id, string operationCode);
        string GetChargeVoucherImageUrl(int id, string operationCode);
        string GetTransferVoucherImageUrl(int id, string operationCode);
    }
}

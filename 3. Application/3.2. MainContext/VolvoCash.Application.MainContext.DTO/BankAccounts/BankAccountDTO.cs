using System;
using VolvoCash.Application.MainContext.DTO.BankAccountTypes;
using VolvoCash.Application.MainContext.DTO.Banks;
using VolvoCash.Application.MainContext.DTO.Currencies;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.BankAccounts
{
    public class BankAccountDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Account { get; set; }

        public string CCI { get; set; }

        public int CurrencyId { get; set; }

        public CurrencyDTO Currency { get; set; }

        public bool IsDefault { get; set; }

        public int BankAccountTypeId { get; set; }

        public BankAccountTypeDTO BankAccountType { get; set; }

        public int BankId { get; set; }

        public BankDTO Bank { get; set; }

        public int? DealerId { get; set; }

        public DealerDTO Dealer { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }
        #endregion
    }
}

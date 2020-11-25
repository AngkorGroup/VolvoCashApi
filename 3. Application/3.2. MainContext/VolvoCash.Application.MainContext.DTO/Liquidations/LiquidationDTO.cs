using System;
using System.Collections.Generic;
using VolvoCash.Application.MainContext.DTO.BankAccounts;
using VolvoCash.Application.MainContext.DTO.Banks;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Liquidations
{
    public class LiquidationDTO
    {
        #region Properties
        public int Id { get; set; }

        public MoneyDTO Amount { get; set; }

        public int DealerId { get; set; }

        public DealerDTO Dealer { get; set; }

        public LiquidationStatus LiquidationStatus { get; set; }

        public DateTime Date { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string Voucher { get; set; }

        public List<ChargeListDTO> Charges { get; set; }

        public int ChargesCount { get; set; }

        public int? BankAccountId { get; set; }

        public BankAccountDTO BankAccount { get; set; }
        #endregion
    }
}

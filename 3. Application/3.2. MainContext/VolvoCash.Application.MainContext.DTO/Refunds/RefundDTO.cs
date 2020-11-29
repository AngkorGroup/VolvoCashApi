using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using VolvoCash.Application.MainContext.DTO.BankAccounts;
using VolvoCash.Application.MainContext.DTO.Liquidations;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Refunds
{
    public class RefundDTO
    {
        #region Properties
        public int Id { get; set; }

        public MoneyDTO Amount { get; set; }

        [JsonConverter(typeof(DefaultDateConverter))]
        public DateTime Date { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RefundStatus RefundStatus { get; set; }

        public int LiquidationsCount { get; set; }

        public int? BankAccountId { get; set; }

        public BankAccountDTO BankAccount { get; set; }

        public string CompanyBankAccount { get; set; }

        [JsonConverter(typeof(DefaultDateConverter))]
        public DateTime? PaymentDate { get; set; }

        public string Voucher { get; set; }

        public List<LiquidationListDTO> Liquidations { get; set; }
        #endregion
    }
}

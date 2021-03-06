﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;
using VolvoCash.CrossCutting.Utils.Constants;

namespace VolvoCash.Domain.MainContext.Services.BankService
{
    public class BankLiquidationService : IBankLiquidationService
    {
        #region Members
        private const string _headerIndicator = "1";
        private const string _detailIndicator = "2";
        private readonly IBankRepository _bankRepository;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public BankLiquidationService(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Private Methods
        private double GetSumForLiquidations(List<Liquidation> liquidations)
        {
            var totalAmount = 0.0;
            liquidations.ForEach(l => totalAmount += l.Amount.Value);
            return totalAmount;
        }

        private long GetCheckSum(BankAccount bankAccount, List<Liquidation> liquidations)
        {
            var sumAccount = long.Parse(bankAccount.Account.Substring(3, 10));
            foreach (var liquidation in liquidations)
            {
                var dealerBankAccount = liquidation.Dealer.GetBankAccount(bankAccount.Bank.Id, liquidation.Amount.CurrencyId);
                // TODO: Validar que si la cuenta no existe como mostrar el error.
                if (dealerBankAccount == null)
                {
                    var dealerBankAccountIsNullMessage = _resources.GetStringResource(LocalizationKeys.Application.exception_DealerBankAccountIsNull);
                    dealerBankAccountIsNullMessage = string.Format(dealerBankAccountIsNullMessage, liquidation.Dealer.Name);
                    throw new InvalidOperationException(dealerBankAccountIsNullMessage);
                }

                var isCCI = dealerBankAccount.Bank.Id != bankAccount.Bank.Id;
                var accountType = dealerBankAccount.BankAccountType.BankBankAccountTypes.FirstOrDefault(bat => bat.BankId == bankAccount.Bank.Id).Equivalence;

                if (isCCI)
                {
                    sumAccount += long.Parse(dealerBankAccount.CCI.Substring(10, 10));
                }
                else if (accountType == "C" || accountType == "M")
                {
                    sumAccount += long.Parse(dealerBankAccount.Account.Substring(3, 10));
                }
                else if (accountType == "A")
                {
                    sumAccount += long.Parse(dealerBankAccount.Account.Substring(3, 11));
                }
            }
            return sumAccount;
        }

        private string GetBCPHeaderLine(BankAccount bankAccount, List<Liquidation> liquidations)
        {
            var headerIndicator = _headerIndicator;
            var paymentsQuantity = liquidations.Count().ToString().PadLeft(6, '0');
            var processDate = DateTime.Now.ToString(DateTimeFormats.BankDateFormat).PadRight(8);
            var bankAccountType = bankAccount.BankAccountType.BankBankAccountTypes.FirstOrDefault(bbat => bbat.Bank.Id == bankAccount.Bank.Id).Equivalence.PadRight(1);
            var currencyAccount = bankAccount.Currency.BankCurrencies.FirstOrDefault(bc => bc.Bank.Id == bankAccount.Bank.Id).Equivalence.PadRight(4);
            var bankAccountNumber = bankAccount.Account.PadRight(20);
            var totalAmount = GetSumForLiquidations(liquidations).ToString("0.00", CultureInfo.InvariantCulture).PadLeft(17, '0');
            var payrollReference = $"Referencia Pago Proveedores {processDate}".PadRight(40);
            var itfExonerationFlag = "N";
            var checkSum = GetCheckSum(bankAccount, liquidations).ToString().PadLeft(15, '0');

            var headerline = $"{headerIndicator}{paymentsQuantity}{processDate}{bankAccountType}{currencyAccount}{bankAccountNumber}{totalAmount}{payrollReference}{itfExonerationFlag}{checkSum}";
            return headerline;
        }

        private string GetBCPDetailLine(BankAccount bankAccount, Liquidation liquidation)
        {
            var dealerBankAccount = liquidation.Dealer.GetBankAccount(bankAccount.Bank.Id, liquidation.Amount.CurrencyId);
            var detailIndicator = _detailIndicator;
            var bankAccountType = dealerBankAccount.BankAccountType.BankBankAccountTypes.FirstOrDefault(bat => bat.BankId == bankAccount.Bank.Id).Equivalence.PadRight(1);
            var bankAccountNumber = dealerBankAccount.Bank.Id == bankAccount.Bank.Id ? dealerBankAccount.Account.PadRight(20) : dealerBankAccount.CCI.PadRight(20);
            var paymentMethod = "1";
            var supplierDocumentTypeId = "6";
            var supplierDocumentTypeNumber = liquidation.Dealer.Ruc.PadRight(12);
            var supplierDocumentCorrelative = "".PadRight(3);
            var supplierName = liquidation.Dealer.Name.PadRight(75).Substring(0, 75);
            var beneficiaryReference = $"Referencia Beneficiario {liquidation.Dealer.Ruc}".PadRight(40);
            var companyReference = $"Ref Emp {liquidation.Dealer.Ruc}".PadRight(20);
            var currencyPayment = liquidation.Amount.Currency.BankCurrencies.FirstOrDefault(bc => bc.BankId == bankAccount.Bank.Id).Equivalence.PadRight(4);
            var amount = liquidation.Amount.Value.ToString("0.00", CultureInfo.InvariantCulture).PadLeft(17, '0');
            var idcValidation = "S";
            liquidation.SetDealerBankAccount(dealerBankAccount);

            var detailLine = $"{detailIndicator}{bankAccountType}{bankAccountNumber}{paymentMethod}{supplierDocumentTypeId}{supplierDocumentTypeNumber}{supplierDocumentCorrelative}{supplierName}{beneficiaryReference}{companyReference}{currencyPayment}{amount}{idcValidation}";
            return detailLine;
        }

        private List<string> GetBCPDetailLines(BankAccount bankAccount, List<Liquidation> liquidations)
        {
            var detailLines = new List<string>();
            foreach (var liquidation in liquidations)
            {
                var detailLine = GetBCPDetailLine(bankAccount, liquidation);
                detailLines.Add(detailLine);
            }
            return detailLines;
        }

        private byte[] GetFileFromStructure(string headerLine, List<string> detailLines)
        {
            byte[] bytes = null;
            using (var memoryStream = new MemoryStream())
            {
                var sw = new StreamWriter(memoryStream);
                sw.WriteLine(headerLine);

                foreach (var detailLine in detailLines)
                {
                    sw.WriteLine(detailLine);
                }

                sw.Flush();
                memoryStream.Position = 0;
                bytes = memoryStream.ToArray();
            }
            return bytes;
        }

        private byte[] GenerateBCPFile(BankAccount bankAccount, List<Liquidation> liquidations)
        {
            if (liquidations.Any())
            {
                var headerLine = GetBCPHeaderLine(bankAccount, liquidations);
                var detailLines = GetBCPDetailLines(bankAccount, liquidations);
                return GetFileFromStructure(headerLine, detailLines);
            }
            throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Domain.exception_NoLiquidationsToGenerateFile));
        }

        private byte[] GenerateBBVAFile(BankAccount bankAccount, List<Liquidation> liquidations)
        {
            //FIXME: REMOVER CUANDO LA LOGICA ESTE LISTA
            return GetFileFromStructure("CABECERABBVA", new List<string>() { "DETALLEBBVA1", "DETALLEBBVA2" });
            if (liquidations.Any())
            {
                var headerLine = GetBCPHeaderLine(bankAccount, liquidations);
                var detailLines = GetBCPDetailLines(bankAccount, liquidations);
                return GetFileFromStructure(headerLine, detailLines);
            }
            throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Domain.exception_NoLiquidationsToGenerateFile));
        }
        #endregion

        #region Public Methods
        public byte[] GenerateBankFile(BankAccount originBankAccount, List<Liquidation> liquidations)
        {
            switch (originBankAccount.Bank.Abbreviation)
            {
                case BankNames.BCP:
                    return GenerateBCPFile(originBankAccount, liquidations);
                case BankNames.BBVA:
                    return GenerateBBVAFile(originBankAccount, liquidations);
                default:
                    return null;
            }
        }
        #endregion
    }
}

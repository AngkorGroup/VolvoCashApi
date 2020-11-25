using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;

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
        private string getBCPHeaderLine(List<Liquidation> liquidations)
        {
            var headerIndicator = _headerIndicator;
            var paymentsQuantity = liquidations.Count().ToString().PadLeft(6, '0');
            var processDate = DateTime.Now.ToString(DateTimeFormats.BankDateFormat);
            var bankAccountType = "";
            var currencyAccount = "";
            var bankAccountNumber = "".PadRight(20);
            var totalAmount = "";
            var payrollReference = "".PadRight(20);
            var itfExonerationFlag = "N";
            var checkSum = "";

            var headerline = $"{headerIndicator}{paymentsQuantity}{processDate}{bankAccountType}{currencyAccount}{bankAccountNumber}{totalAmount}{payrollReference}{itfExonerationFlag}{checkSum}";
            return headerline;
        }

        private string getBCPDetailLine(Liquidation liquidation, int bankId)
        {
            var bankAccount = liquidation.Dealer.GetBankAccount(bankId, liquidation.Amount.CurrencyId);

            // TODO: Validar que si la cuenta no existe como mostrar el error.
            if (bankAccount == null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Domain.exception_BankAccountIsNull));
            }

            var detailIndicator = _detailIndicator;
            var bankAccountType = bankAccount.BankAccountType.BankBankAccountTypes.FirstOrDefault(bat => bat.BankId == bankId).Equivalence;
            var bankAccountNumber = bankAccount.Account.PadRight(20);
            var paymentMethod = "1";
            var supplierDocumentTypeId = "6";
            var supplierDocumentTypeNumber = liquidation.Dealer.Ruc.PadRight(12);
            var supplierDocumentCorrelative = "".PadRight(3);
            var supplierName = liquidation.Dealer.Name.PadRight(75);
            var beneficiaryReference = "".PadRight(40);
            var companyReference = "".PadRight(20);
            var currencyPayment = liquidation.Amount.Currency.BankCurrencies.FirstOrDefault(bc => bc.BankId == bankId).Equivalence;
            var amount = liquidation.Amount.Value.ToString("0.00", CultureInfo.InvariantCulture).PadLeft(17);
            var idcValidation = "S";
            var documentsQuantity = "";

            var detailLine = $"{detailIndicator}{bankAccountType}{bankAccountNumber}{paymentMethod}{supplierDocumentTypeId}{supplierDocumentTypeNumber}{supplierDocumentCorrelative}{supplierName}{beneficiaryReference}{companyReference}{currencyPayment}{amount}{idcValidation}{documentsQuantity}";
            return detailLine;
        }

        private List<string> getBCPDetailLines(List<Liquidation> liquidations)
        {
            var bank = _bankRepository.Filter(b => b.Abbreviation == BankNames.BCP).FirstOrDefault();
            var detailLines = new List<string>();

            foreach (var liquidation in liquidations)
            {
                var detailLine = getBCPDetailLine(liquidation, bank.Id);
                detailLines.Add(detailLine);
            }
            return detailLines;
        }
        #endregion

        #region Public Methods
        public byte[] GenerateBankFile(BankAccount bankAccount, List<Liquidation> liquidations)
        {
            switch (bankAccount.Bank.Abbreviation)
            {
                case BankNames.BCP:
                    return GenerateBCPFile(bankAccount, liquidations);
                case BankNames.BBVA:
                    return GenerateBBVAFile(bankAccount, liquidations);
                default:
                    return null;
            }            
        }
        #endregion

        #region Private Methods
        private byte[] GetFileFromStructure(string headerLine, List<string> detailLines)
        {
            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                var sr = new StreamWriter(ms);
                sr.WriteLine(headerLine);
                foreach (var detailLine in detailLines)
                {
                    sr.WriteLine(detailLine);
                }
                sr.Flush();
                ms.Position = 0;
                bytes = ms.ToArray();
            }               
            return bytes;
        }

        private byte[] GenerateBCPFile(BankAccount bankAccount, List<Liquidation> liquidations)
        {
            //TODO REMOVER CUANDO LA LOGICA ESTE LISTA
            return GetFileFromStructure("CABECERABCP", new List<string>() { "DETALLEBCP1", "DETALLEBCP2" });
            if (liquidations.Any())
            {
                var headerLine = getBCPHeaderLine(liquidations);
                var detailLines = getBCPDetailLines(liquidations);
                return GetFileFromStructure(headerLine, detailLines);
            }
            throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Domain.exception_NoLiquidationsToGenerateFile));
        }

        private byte[] GenerateBBVAFile(BankAccount bankAccount, List<Liquidation> liquidations)
        {
            //TODO REMOVER CUANDO LA LOGICA ESTE LISTA
            return GetFileFromStructure("CABECERABBVA", new List<string>() { "DETALLEBBVA1", "DETALLEBBVA2" });
            if (liquidations.Any())
            {
                var headerLine = getBCPHeaderLine(liquidations);
                var detailLines = getBCPDetailLines(liquidations);
                return GetFileFromStructure(headerLine, detailLines);
            }
            throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Domain.exception_NoLiquidationsToGenerateFile));
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;

namespace VolvoCash.Domain.MainContext.Services.BankService
{
    public class BankLiquidationService : IBankLiquidationService
    {
        #region Properties
        private readonly string _headerIndicator = "1";
        private readonly string _detailIndicator = "2";
        #endregion

        #region Private Methods
        private string getBCPHeaderLine(List<Liquidation> liquidations)
        {
            var headerIndicator = _headerIndicator;
            var paymentsQuantity = liquidations.Count().ToString().PadLeft(6, '0');
            var processDate = DateTime.Now.ToString(DateTimeFormats.BankDateFormat);
            var bankAccountType = "";
            var currencyAccount = "";
            var bankAccount = "".PadRight(20);
            var totalAmount = "";
            var payrollReference = "".PadRight(20);
            var itfExonerationFlag = "N";
            var checkSum = "";

            var headerline = $"{headerIndicator}{paymentsQuantity}{processDate}{bankAccountType}{currencyAccount}{bankAccount}{totalAmount}{payrollReference}{itfExonerationFlag}{checkSum}";
            return headerline;
        }

        private string getBCPDetailLine(Liquidation liquidation)
        {
            var detailIndicator = _detailIndicator;
            var bankAccountType = "";
            var bankAccount = "".PadRight(20);
            var paymentMethod = "1";
            var supplierDocumentTypeId = "6";
            var supplierDocumentTypeNumber = liquidation.Dealer.Ruc.PadRight(12);
            var supplierDocumentCorrelative = "".PadRight(3);
            var supplierName = liquidation.Dealer.Name.PadRight(75);
            var beneficiaryReference = "".PadRight(40);
            var companyReference = "".PadRight(20);
            var currencyPayment = "";
            var amount = liquidation.Amount.Value.ToString().PadLeft(17);
            var idcValidation = "S";
            var documentsQuantity = "";

            var detailLine = $"{detailIndicator}{bankAccountType}{bankAccount}{paymentMethod}{supplierDocumentTypeId}{supplierDocumentTypeNumber}{supplierDocumentCorrelative}{supplierName}{beneficiaryReference}{companyReference}{currencyPayment}{amount}{idcValidation}{documentsQuantity}";
            return detailLine;
        }

        private List<string> getBCPDetailLines(List<Liquidation> liquidations)
        {
            var detailLines = new List<string>();
            foreach (var liquidation in liquidations)
            {
                var detailLine = getBCPDetailLine(liquidation);
                detailLines.Add(detailLine);
            }
            return detailLines;
        }
        #endregion

        #region Public Methods
        #endregion

        public void GenerateBCPFile(List<Liquidation> liquidations)
        {
            if (liquidations.Any())
            {
                var headerLine = getBCPHeaderLine(liquidations);
                var detailLines = getBCPDetailLines(liquidations);
                //TODO:Crear el archivo con el header y el detailLines
            }
        }
    }
}

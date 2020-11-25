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
            var bankAccount = "";
            var totalAmount = "";
            var payrollReference = "";
            var itfExonerationFlag = "";
            var checkSum = "";

            var headerline = $"{headerIndicator}{paymentsQuantity}{processDate}{bankAccountType}{currencyAccount}{bankAccount}{totalAmount}{payrollReference}{itfExonerationFlag}{checkSum}";
            return headerline;
        }

        private string getBCPDetailLine(Liquidation liquidation)
        {
            var detailIndicator = _detailIndicator;
            var bankAccountType = "";
            var bankAccount = "";
            var paymentMethod = "";
            var supplierDocumentTypeId = "";
            var supplierDocumentTypeNumber = "";
            var supplierDocumentCorrelative = "";
            var supplierName = "";
            var beneficiaryReference = "";
            var companyReference = "";
            var currencyPayment = "";
            var amount = "";
            var idcValidation = "";
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

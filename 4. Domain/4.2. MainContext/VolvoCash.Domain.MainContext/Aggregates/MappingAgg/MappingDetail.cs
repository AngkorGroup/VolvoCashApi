using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.MappingAgg
{
    public class MappingDetail : AuditableEntityWithKey<int>
    {
        #region Properties
        [MaxLength(200)]
        public string Type { get; set; }

        [MaxLength(200)]
        public string DocumentType { get; set; }

        [MaxLength(200)]
        public string Line { get; set; }

        [MaxLength(200)]
        public string RecordType { get; set; }

        [MaxLength(200)]
        public string Company { get; set; }

        [MaxLength(200)]
        public string Reference { get; set; }

        [MaxLength(200)]
        public string PostKey { get; set; }

        [MaxLength(200)]
        public string Account { get; set; }

        [MaxLength(200)]
        public string Sign { get; set; }

        [MaxLength(200)]
        public string TaxCode { get; set; }

        [MaxLength(200)]
        public string TaxAmount { get; set; }

        [MaxLength(200)]
        public string CostCenter { get; set; }

        [MaxLength(200)]
        public string ProfitCenter { get; set; }

        [MaxLength(200)]
        public string TradePartner { get; set; }

        [MaxLength(200)]
        public string DocText { get; set; }

        [MaxLength(200)]
        public string MoreInfo { get; set; }

        [MaxLength(200)]
        public string BusinessArea { get; set; }

        [MaxLength(200)]
        public string Market { get; set; }

        [MaxLength(200)]
        public string Customer { get; set; }

        [MaxLength(200)]
        public string ProductModel { get; set; }

        [MaxLength(200)]
        public string LineType { get; set; }

        [MaxLength(200)]
        public string Classification { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }

        [ForeignKey("MappingHeader")]
        public int MappingHeaderId { get; set; }

        public virtual MappingHeader MappingHeader { get; set; }
        #endregion

        #region NotMapped Properties
        #endregion

        #region Constructor
        public MappingDetail()
        {
        }
        public MappingDetail(string type, string documentType, string line, string recordType, string company,
                            string reference, string postKey, string account, string sign, string taxCode, string taxAmount, 
                            string costCenter, string profitCenter, string tradePartner, string docText, string moreInfo,
                            string businessArea, string market, string customer, string productModel, string lineType,
                            string classification, int mappingHeaderId)
        {
            Type = type;
            DocumentType = documentType;
            Line = line;
            RecordType = recordType;
            Company = company;
            Reference = reference;
            PostKey = postKey;
            Account = account;
            Sign = sign;
            TaxCode = taxCode;
            TaxAmount = taxAmount;
            CostCenter = costCenter;
            ProfitCenter = profitCenter;
            TradePartner = tradePartner;
            DocText = docText;
            MoreInfo = moreInfo;
            BusinessArea = businessArea;
            Market = market;
            Customer = customer;
            ProductModel = productModel;
            LineType = lineType;
            Classification = classification;
            MappingHeaderId = mappingHeaderId;
            Status = Status.Active;
        }
        #endregion

        #region Public Methods
        #endregion
    }
}

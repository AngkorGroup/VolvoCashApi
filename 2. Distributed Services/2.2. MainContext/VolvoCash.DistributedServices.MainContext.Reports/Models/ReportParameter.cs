namespace VolvoCash.DistributedServices.MainContext.Reports.Models
{
    public class ReportParameter
    {
        #region Properties
        public string Name { get; set; }
        public string Value { get; set; }
        #endregion

        #region Constructor
        public ReportParameter()
        {
        }

        public ReportParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }
        #endregion
    }
}

namespace VolvoCash.DistributedServices.MainContext.Reports.Models
{
    public class ReportParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ReportParameter()
        {
        }

        public ReportParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

    }
}

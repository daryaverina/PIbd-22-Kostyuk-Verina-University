namespace UniversityContracts.BindingModels
{
    public class ProviderReportBindingModel
    {
        public string FileName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string ProviderEmail { get; set; }

        public List<int> StatusIds { get; set; }
    }
}

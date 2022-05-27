using UniversityContracts.ViewModels;

namespace UniversityContracts.BindingModels
{
    public class ProviderReportBindingModel
    {
        public string FileName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public List<StudentViewModel> Students { get; set; }
    }
}

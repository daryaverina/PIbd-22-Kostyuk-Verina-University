using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.OfficePackage.HelperModels
{
    public class ProviderPdfInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public List<ReportStatusesViewModel> Statuses { get; set; }
    }
}
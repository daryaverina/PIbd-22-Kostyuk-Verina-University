using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.OfficePackage.HelperModels
{
    public class ProviderWordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportSubjectStudentViewModel> SubjectStudents { get; set; }
    }
}
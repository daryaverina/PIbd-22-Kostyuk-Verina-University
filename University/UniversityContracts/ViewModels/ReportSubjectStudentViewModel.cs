namespace UniversityContracts.ViewModels
{
    // получение списка дисциплин, посещаемых выбранными студентами, в формате doc/xls;

    public class ReportSubjectStudentViewModel
    {
        public string FullName { get; set; }

        // = Faculty 
        public string FlowName { get; set; }

        public List<Tuple<string>> Subjects { get; set; }
    }
}

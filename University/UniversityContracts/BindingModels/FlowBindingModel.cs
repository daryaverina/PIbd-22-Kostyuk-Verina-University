namespace UniversityContracts.BindingModels
{
    public class FlowBindingModel
    {
        public int? Id { get; set; }
        public string Flow_name { get; set; }
        public string Faculty { get; set; }
        public int NumberOfCourse { get; set; }
        public int? CustomerID { get; set; }
        public Dictionary<int, string> SubjectFlows { get; set; }

        // поток-студент многие ко многим
        public Dictionary<int, string> StudentFlows { get; set; }
    }
}

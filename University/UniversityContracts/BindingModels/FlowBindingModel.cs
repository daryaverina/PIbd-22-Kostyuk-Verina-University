namespace UniversityContracts.BindingModels
{
    public class FlowBindingModel
    {
        public int? Id { get; set; }
        public string Faculty { get; set; }
        public int NumberOfCourse { get; set; }
        public int? CustomerID { get; set; }
        public Dictionary<int, string> SubjectFlows { get; set; }
    }
}

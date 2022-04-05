namespace UniversityContracts.BindingModels
{
    public class SubjectBindingModel
    {
        public int? Id { get; set; }
        public string SubjectName { get; set; }
        public int HoursAmount { get; set; }
        public int? CustomerID { get; set; }
        public Dictionary<int, string> SubjectFlows { get; set; }
    }
}

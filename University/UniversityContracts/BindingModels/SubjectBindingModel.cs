namespace UniversityContracts.BindingModels
{
    public class SubjectBindingModel
    {
        public int? Id { get; set; }
        public string Subjectname { get; set; }
        public int HoursAmount { get; set; }
        public int? CustomerID { get; set; }
    }
}

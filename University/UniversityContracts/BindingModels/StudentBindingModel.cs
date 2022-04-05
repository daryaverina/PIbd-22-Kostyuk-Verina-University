namespace UniversityContracts.BindingModels
{
    // Студент
    public class StudentBindingModel
    {
        // Номер зачетки
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int? ProviderId { get; set; }
        public Dictionary<int, string> DecreeStudents { get; set; }
    }
}

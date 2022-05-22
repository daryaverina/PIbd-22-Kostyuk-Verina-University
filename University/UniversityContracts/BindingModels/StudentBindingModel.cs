namespace UniversityContracts.BindingModels
{
    // Студент
    public class StudentBindingModel
    {
        // Номер зачетки
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int ProviderId { get; set; }
        // приказ-студент многие ко многим
        public Dictionary<int, string> DecreeStudents { get; set; }
        // поток-студент многие ко многим
        public Dictionary<int, string> StudentFlows { get; set; }
        // статус-студент один ко многим 
        public Dictionary<int, string> EducationalStatuses { get; set; }
    }
}

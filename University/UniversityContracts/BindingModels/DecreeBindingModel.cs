namespace UniversityContracts.BindingModels
{
    // Приказ
    public class DecreeBindingModel
    {
        public int? Id { get; set; }
        public string DecreeNumber { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int ProviderId { get; set; }
        // приказ-группа многие ко многим
        public Dictionary<int, string> DecreeGroups { get; set; }
        // приказ-студент многие ко многим
        public Dictionary<int, string> DecreeStudents { get; set; }
    }
}

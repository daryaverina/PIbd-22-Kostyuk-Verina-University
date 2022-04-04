namespace UniversityContracts.BindingModels
{
    // Приказ
    public class DecreeBindingModel
    {
        // Номер приказа
        public int? Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int? ProviderId { get; set; }
    }
}

using UniversityContracts.Enums;

namespace UniversityContracts.BindingModels
{
    // Статус обучения
    public class EducationalStatusBindingModel
    {
        public int? Id { get; set; }
        public BaseStatus BStatus { get; set; }
        public FormStatus FStatus { get; set; }
        public DateTime DateOfChange { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int StudentId { get; set; }
        public int ProviderId { get; set; }
    }
}

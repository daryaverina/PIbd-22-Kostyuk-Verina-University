using UniversityContracts.Enums;
using UniversityContracts.Attributes;

namespace UniversityContracts.ViewModels
{
    // Статус обучения
    public class EducationalStatusViewModel
    {
        public int Id { get; set; }

        [Column(title: "Номер зачетки студента", width: 70)]
        public int StudentId { get; set; }

        [Column(title: "Основа обучения", gridViewAutoSize: GridViewAutoSize.Fill)]
        public BaseStatus BStatus { get; set; }

        [Column(title: "Форма обучения", gridViewAutoSize: GridViewAutoSize.Fill)]
        public FormStatus FStatus { get; set; }

        [Column(title: "Дата изменения статуса", width: 200)]
        public DateTime DateOfChange { get; set; }

        public int ProviderId { get; set; }
    }
}

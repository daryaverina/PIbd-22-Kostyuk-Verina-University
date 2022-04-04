using UniversityContracts.Enums;
using System.ComponentModel;

namespace UniversityContracts.ViewModels
{
    // Статус обучения
    public class EducationalStatusViewModel
    {
        public int Id { get; set; }

        [DisplayName("Основа обучения")]
        public BaseStatus BStatus { get; set; }

        [DisplayName("Форма обучения")]
        public FormStatus FStatus { get; set; }

        [DisplayName("Дата изменения статуса")]
        public DateTime DateOfChange { get; set; }
    }
}

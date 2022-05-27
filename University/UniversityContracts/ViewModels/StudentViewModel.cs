using UniversityContracts.Attributes;

namespace UniversityContracts.ViewModels
{
    // Студент
    public class StudentViewModel
    {
        [Column(title: "Номер зачетной книжки", width: 70)]
        public int Id { get; set; }

        [Column(title: "ФИО", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string FullName { get; set; }

        [Column(title: "Номер телефона", width: 150)]
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

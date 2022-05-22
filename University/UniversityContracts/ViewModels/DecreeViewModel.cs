using UniversityContracts.Attributes;

namespace UniversityContracts.ViewModels
{
    // Приказ
    public class DecreeViewModel
    {
        public int Id { get; set; }

        [Column(title: "Номер приказа", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string DecreeNumber { get; set; }

        [Column(title: "Дата принятия", width: 150)]
        public DateTime DateOfCreation { get; set; }

        public int ProviderId { get; set; }

        // приказ-группа многие ко многим
        public Dictionary<int, string> DecreeGroups { get; set; }

        // приказ-студент многие ко многим
        public Dictionary<int, string> DecreeStudents { get; set; }
    }
}

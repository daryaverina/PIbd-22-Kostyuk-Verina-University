using System.ComponentModel;

namespace UniversityContracts.ViewModels
{
    // Приказ
    public class DecreeViewModel
    {
        public int Id { get; set; }

        [DisplayName("Номер приказа")]
        public string DecreeNumber { get; set; }

        [DisplayName("Дата принятия")]
        public DateTime DateOfCreation { get; set; }

        public Dictionary<int, string> DecreeGroups { get; set; }
    }
}

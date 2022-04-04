using System.ComponentModel;

namespace UniversityContracts.ViewModels
{
    // Приказ
    public class DecreeViewModel
    {
        [DisplayName("Номер приказа")]
        public int Id { get; set; }

        [DisplayName("Дата принятия")]
        public DateTime DateOfCreation { get; set; }
    }
}

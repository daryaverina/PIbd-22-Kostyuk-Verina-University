using System.ComponentModel;

namespace UniversityContracts.ViewModels
{
    // Студент
    public class StudentViewModel
    {
        [DisplayName("Номер зачетной книжки")]
        public int Id { get; set; }

        [DisplayName("ФИО")]
        public string FullName { get; set; }

        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }
    }
}

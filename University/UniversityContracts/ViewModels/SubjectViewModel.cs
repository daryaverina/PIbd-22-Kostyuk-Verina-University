using System.ComponentModel;

namespace UniversityContracts.ViewModels
{
    internal class SubjectViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название предмета")]
        public string SubjectName { get; set; }

        [DisplayName("Количество часов")]
        public int HoursAmount { get; set; }
    }
}

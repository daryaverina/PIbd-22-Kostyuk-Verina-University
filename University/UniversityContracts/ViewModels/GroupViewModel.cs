using System.ComponentModel;

namespace UniversityContracts.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        [DisplayName("Специальность")]
        public string Speciality { get; set; }

        [DisplayName("Количество семестров")]
        public int SemestersAmount { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreated { get; set; }
    }
}

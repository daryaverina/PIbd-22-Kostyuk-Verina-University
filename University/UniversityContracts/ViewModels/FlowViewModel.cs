using System.ComponentModel;

namespace UniversityContracts.ViewModels
{
    public class FlowViewModel
    {
        public int Id { get; set; }

        [DisplayName("Факультет")]
        public string Faculty { get; set; }

        [DisplayName("Номер курса")]
        public int NumberOfCourse { get; set; }

        [DisplayName("Специальность")]
        public string Speciality { get; set; }
    }
}

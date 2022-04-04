using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

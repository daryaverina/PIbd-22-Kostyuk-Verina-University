using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityContracts.ViewModels
{
    public class SubjectViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название предмета")]
        public string SubjectName { get; set; }

        [DisplayName("Количество часов")]
        public int HoursAmount { get; set; }
    }
}

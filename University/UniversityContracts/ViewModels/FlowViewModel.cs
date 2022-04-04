using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityContracts.ViewModels
{
    public class FlowViewModel
    {
        public int Id { get; set; }

        [DisplayName("Факультет")]
        public string Faculty { get; set; }

        [DisplayName("Номер курса")]
        public int NumberOfCourse { get; set; }
    }
}

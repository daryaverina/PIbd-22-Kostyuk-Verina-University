using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityContracts.BindingModels
{
    public class FlowBindingModel
    {
        public int? Id { get; set; }
        public string Faculty { get; set; }
        public int NumberOfCourse { get; set; }
        public int? Clientd { get; set; }
    }
}

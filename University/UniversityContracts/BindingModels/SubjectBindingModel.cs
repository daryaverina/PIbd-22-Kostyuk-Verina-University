using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityContracts.BindingModels
{
    public class SubjectBindingModel
    {
        public int? Id { get; set; }
        public string Subjectname { get; set; }
        public int HoursAmount { get; set; }
        public int? CustomerId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityContracts.BindingModels
{
    public class GroupBindingModel
    {
        public int? Id { get; set; }
        public string Speciality { get; set; }
        public int SemestersAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public int? CustomerId { get; set; }
    }
}

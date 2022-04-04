using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        public string SubjectName { get; set; }

        [Required]
        public int HoursAmount { get; set; }

        [ForeignKey("SubjectId")]
        public virtual List<SubjectFlow> SubjectFlows { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}

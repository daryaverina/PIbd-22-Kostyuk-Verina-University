using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UniversityDatabaseImplement.Models
{
    public class Flow
    {
        public int Id { get; set; }

        [Required]
        public string Faculty { get; set; }
        [Required]
        public int NumberOfCourse { get; set; }

        public int CustomerID { get; set; }

        [ForeignKey("FlowId")]
        public virtual List<Group> Groups { get; set; }

        [ForeignKey("FlowId")]
        public virtual List<SubjectFlow> SubjectFlows { get; set; }

        [ForeignKey("FlowId")]
        public virtual List<FlowStudent> FlowStudents { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
    public class SubjectFlow
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int FlowId { get; set; }

        [Required]
        public string Lecturer { get; set; }
        public virtual Flow Flow { get; set; }
        public virtual Subject Subject { get; set; }
    }
}

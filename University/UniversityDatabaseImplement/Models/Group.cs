using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        public string Speciality { get; set; }
        [Required]
        public int SemestersAmount { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}

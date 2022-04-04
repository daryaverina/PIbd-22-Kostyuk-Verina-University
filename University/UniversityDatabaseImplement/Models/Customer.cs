using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UniversityDatabaseImplement.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [ForeignKey("CustomerID")]
        public virtual List<Flow> Flows { get; set; }

        [ForeignKey("CustomerID")]
        public virtual List<Subject> Subjects { get; set; }

        [ForeignKey("CustomerID")]
        public virtual List<Group> Groups { get; set; }
    }
}
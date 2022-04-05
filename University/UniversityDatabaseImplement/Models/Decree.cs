using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
    // Приказ
    public class Decree
    {
        public int Id { get; set; }

        [Required]
        public string DecreeNumber { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }

        [ForeignKey("DecreeId")]
        public virtual List<DecreeGroup> DecreeGroups { get; set; }

        [ForeignKey("DecreeId")]
        public virtual List<DecreeStudent> DecreeStudents { get; set; }
    }
}


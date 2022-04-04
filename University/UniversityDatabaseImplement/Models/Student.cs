using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
    // Поставщик
    public class Student
    {
        // Номер зачетки
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }

        [ForeignKey("StudentId")]
        public virtual List<EducationalStatus> EducationalStatuses { get; set; }

        [ForeignKey("StudentId")]
        public virtual List<DecreeStudent> DecreeStudents { get; set; }

        [ForeignKey("StudentId")]
        public virtual List<ПотокStudent> ПотокStudents { get; set; }
    }
}


using UniversityContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace UniversityDatabaseImplement.Models
{
    // Статус обучения
    public class EducationalStatus
    {
        public int Id { get; set; }

        [Required]
        public BaseStatus BStatus { get; set; }

        [Required]
        public FormStatus FStatus { get; set; }

        [Required]
        public DateTime DateOfChange { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }
    }
}


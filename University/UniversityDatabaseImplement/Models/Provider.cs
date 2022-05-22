using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
    // Поставщик
    public class Provider
    {
        public int Id { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [ForeignKey("ProviderId")]
        public virtual List<Student> Students { get; set; }

        [ForeignKey("ProviderId")]
        public virtual List<Decree> Decrees { get; set; }

        [ForeignKey("ProviderId")]
        public virtual List<EducationalStatus> EducationalStatuses { get; set; }
    }
}


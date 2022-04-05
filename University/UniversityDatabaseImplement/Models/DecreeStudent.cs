namespace UniversityDatabaseImplement.Models
{
    public class DecreeStudent
    {
        public int Id { get; set; }

        public int DecreeId { get; set; }

        public int StudentId { get; set; }

        public virtual Decree Decree { get; set; }

        public virtual Student Student { get; set; }
    }
}
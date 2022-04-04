using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDatabaseImplement.Models
{
    public class DecreeGroup
    {
        public int Id { get; set; }

        public int DecreeId { get; set; }

        public int GroupId { get; set; }

        public virtual Decree Decree { get; set; }

        public virtual Group Group { get; set; }
    }
}

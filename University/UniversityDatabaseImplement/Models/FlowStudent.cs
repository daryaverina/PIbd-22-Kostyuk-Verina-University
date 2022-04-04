using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDatabaseImplement.Models
{
    public class FlowStudent
    {
        public int Id { get; set; }

        public int FlowId { get; set; }

        public int StudentId { get; set; }

        public virtual Flow Flow { get; set; }

        public virtual Student Student { get; set; }
    }
}

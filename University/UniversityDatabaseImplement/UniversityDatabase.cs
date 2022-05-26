using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement
{
    public class UniversityDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-CM2EFFR\SQLEXPRESS;Initial Catalog=UniversityDatabase;Integrated Security=True;Encrypt=False;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Customer> Customers { set; get; }
        public virtual DbSet<Decree> Decrees { set; get; }
        public virtual DbSet<DecreeGroup> DecreeGroups { set; get; }
        public virtual DbSet<DecreeStudent> DecreeStudents { set; get; }
        public virtual DbSet<EducationalStatus> EducationalStatuses { set; get; }
        public virtual DbSet<Flow> Flows { set; get; }
        public virtual DbSet<FlowStudent> FlowStudents { set; get; }
        public virtual DbSet<Group> Groups { set; get; }
        public virtual DbSet<Provider> Providers { set; get; }
        public virtual DbSet<Student> Students { set; get; }
        public virtual DbSet<Subject> Subjects { set; get; }
        public virtual DbSet<SubjectFlow> SubjectFlows { set; get; }
    }
}

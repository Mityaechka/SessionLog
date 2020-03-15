using SessionLog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionLog.Data
{
    public class AppContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<SubjectStudent> SubjectStudents { get; set; }
        public AppContext() :base("Connection")
        { }
    }
}

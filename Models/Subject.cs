using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionLog.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual List<SubjectStudent> SubjectStudents { get; set; }
        public virtual List<Day> Days { get; set; }
        public virtual List<Result> Results { get; set; }
        public double Attendance(int id)
        {
            var student = SubjectStudents.First(x => x.StudentId == id);
            if (student == null)
                throw new Exception("Неправильный Id");
            var results = Results.Where(x => x.StudentId == id);
            var total = results.Count();
            var a = results.Where(x => x.IsPresented).Count();
            return ((double)a / (double)total) * 100;
        }
    }
}

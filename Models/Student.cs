using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionLog.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public virtual List<SubjectStudent> SubjectStudents { get; set; }
    }
}

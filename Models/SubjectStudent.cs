using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionLog.Models
{
    public class SubjectStudent
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public Subject Subject { get; set; }
        public Student Student { get; set; }
    }
}

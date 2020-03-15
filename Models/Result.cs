using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionLog.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public Day Day { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public bool IsPresented { get; set; }
        public int? Mark { get; set; } = null;
    }
}

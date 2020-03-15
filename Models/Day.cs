using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionLog.Models
{
    public class Day
    {
        public int Id { get; set; }
        public DayType DayType { get; set; }
        public DateTime Date { get; set; }
    }
}

using SessionLog.Data;
using SessionLog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Day = SessionLog.Models.Day;

namespace SessionLog.Forms
{
    public partial class SubjectDaysForm : Form
    {
        int id;
        Subject s = new Subject();
        public SubjectDaysForm(int id)
        {
            InitializeComponent();
            this.id = id;
            using (var context = new AppContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                s = context.Subjects.Include(x => x.Days).FirstOrDefault(x => x.Id == id);
                Text = s.Title;
            }
            Type.DataSource= Enum.GetValues(typeof(DayType))
                .Cast<DayType>()
                .Select(p => new { Name = Enum.GetName(typeof(DayType), p), Value = (int)p })
                .ToList();
            Type.DisplayMember = "Name";
            Type.ValueMember = "Value";

            grid.Rows.Clear();
            foreach (var st in s.Days)
            {
                grid.Rows.Add(st.Id,st.Date,(int)st.DayType);
            }
            button.Text = "Сохранить";

        }
        private void button_Click(object sender, EventArgs e)
        {
            using (var context = new AppContext())
            {
                s = context.Subjects.Include(x => x.Days).FirstOrDefault(x => x.Id == id);
                var days = new List<Day>();
                foreach(DataGridViewRow r in grid.Rows)
                {
                    try
                    {
                        days.Add(new Day()
                        {
                            Id = Convert.ToInt32(r.Cells[0].Value),
                            Date = Convert.ToDateTime(r.Cells[1].Value),
                            DayType = (DayType)r.Cells[2].Value
                        });
                    }
                    catch (Exception ex) { }
                }
                foreach(var d in days)
                {
                    if (d.Id == -1)
                    {
                        s.Days.Add(d);
                    }
                    else
                    {
                        var day = s.Days[ s.Days.IndexOf(s.Days.Find(x=>x.Id==d.Id))];
                        day.Date = d.Date;
                        day.DayType = d.DayType;
                    }
                }
                s.Days.RemoveAll(x => !days.Any(u => u.Id == x.Id));
                context.SaveChanges();
            }

        }
        void RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (grid.Rows[e.RowIndex].IsNewRow == true)
            {
                grid.Rows[e.RowIndex].Cells[0].Value = -1;
                grid.Rows[e.RowIndex].Cells[1].Value = DateTime.Now;
                //grid.Rows[e.RowIndex].Cells[2].Value = (int)DayType.Занятие;

            }
        }
    }
}


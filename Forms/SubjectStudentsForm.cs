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

namespace SessionLog.Forms
{
    public partial class SubjectStudentsForm : Form
    {
        int id;
        Subject s = new Subject();
        public SubjectStudentsForm(int id)
        {
            InitializeComponent();
            this.id = id;
            var students = new List<Student>();
            using (var context = new AppContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                s = context.Subjects.Include(x => x.SubjectStudents).FirstOrDefault(x => x.Id == id);
                Text = s.Title;
                students = context.Students.ToList();
            }
            foreach (var st in students)
                Student.Items.Add(st);
            
            Student.DisplayMember = "Fullname";
            Student.ValueMember = "Id";
            grid.Rows.Clear();
            foreach (var st in s.SubjectStudents)
            {
                grid.Rows.Add(st.Id, st.Student.Id);
            }
            button.Text = "Сохранить";

        }
        private void button_Click(object sender, EventArgs e)
        {
            using (var context = new AppContext())
            {
                s = context.Subjects.Include(x => x.SubjectStudents).FirstOrDefault(x => x.Id == id);
                var students = new List<SubjectStudent>();
                foreach (DataGridViewRow r in grid.Rows)
                {
                    try
                    {
                        if (r.Cells[1].Value == null)
                            continue;
                        students.Add(new SubjectStudent()
                        {
                            Id = Convert.ToInt32(r.Cells[0].Value),
                            StudentId = Convert.ToInt32(r.Cells[1].Value),
                            SubjectId = s.Id
                        });
                    }
                    catch (Exception ex) { }
                }
                //students = students.AsEnumerable().DistinctBy(x => x.StudentId).ToList();
                foreach (var d in students)
                {
                    if (d.Id == -1)
                    {
                        s.SubjectStudents.Add(d);
                    }
                    else
                    {
                        var day = s.SubjectStudents[s.SubjectStudents.IndexOf(s.SubjectStudents.Find(x => x.Id == d.Id))];
                        day.StudentId = d.StudentId;
                        day.SubjectId = d.SubjectId;
                    }
                }
                var remove = s.SubjectStudents.Where(x => !students.Any(u => u.Id == x.Id));
                context.SubjectStudents.RemoveRange(remove);
                context.SaveChanges();
                //s.SubjectStudents.RemoveAll(x => !students.Any(u => u.Id == x.Id));
                
                context.SaveChanges();
            }

        }
        void RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (grid.Rows[e.RowIndex].IsNewRow == true)
            {
               grid.Rows[e.RowIndex].Cells[0].Value = -1;

            }
        }
        private void CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.ColumnIndex == 1) //VALIDATE FIRST COLUMN

                    for (int row = 0; row < grid.Rows.Count - 1; row++)
                    {

                        if (grid.Rows[row].Cells[1].Value != null &&
                            row != e.RowIndex &&
                            grid.Rows[row].Cells[1].Value.Equals(grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                        {

                            //MessageBox.Show("Duplicate");
                            grid.Rows[e.RowIndex].Cells[1].Value = null;

                        }
                        else
                        {
                            
                            //Add To datagridview

                        }

                    }
            }
            catch (Exception ex)
            {

            }
        }
    }
}


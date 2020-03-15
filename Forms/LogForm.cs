using SessionLog.Data;
using SessionLog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
namespace SessionLog.Forms
{
    public partial class LogForm : Form
    {
        Subject subject = new Subject();

        public LogForm()
        {
            InitializeComponent();
            RefreshSubjects();
        }
        public void RefreshSubjects()
        {
            using (var context = new AppContext())
            {
                var subjects = context.Subjects.ToArray();
                subjectSelect.Items.Clear();
                subjectSelect.Items.AddRange(subjects);
                subjectSelect.DisplayMember = "Title";
                subjectSelect.ValueMember = "Id";
                subjectSelect.SelectedIndex = 0;
            }
        }
        public void RefreshGrid(bool generateGrid)
        {
            grid.AutoGenerateColumns = false;
            using (var context = new AppContext())
            {
                var id = (subjectSelect.SelectedItem as Subject).Id;
                subject = context.Subjects.Include(x => x.SubjectStudents.Select(u => u.Student)).Include("Days").Include("Results").FirstOrDefault(x => x.Id == id);
                if (subject.Results.Count() == 0)
                {
                    foreach (var day in subject.Days)
                    {
                        foreach (var student in subject.SubjectStudents)
                        {
                            subject.Results.Add(new Result() { DayId = day.Id, StudentId = student.StudentId, SubjectId = subject.Id, Mark = 0, IsPresented = true });
                        }
                    }
                }
                else
                {
                    var newDays = subject.Days.Except(subject.Results.Select(x => x.Day));
                    foreach (var day in newDays)
                    {
                        foreach (var student in subject.SubjectStudents)
                        {
                            subject.Results.Add(new Result() { DayId = day.Id, StudentId = student.StudentId, SubjectId = subject.Id, Mark = 0, IsPresented = true });

                        }
                    }
                    var newStudents = subject.SubjectStudents.Select(x=>x.Student).Except(subject.Results.Select(x => x.Student));
                    foreach (var day in subject.Days)
                    {
                        foreach (var student in newStudents)
                        {
                            subject.Results.Add(new Result() { DayId = day.Id, StudentId = student.Id, SubjectId = subject.Id, Mark = 0, IsPresented = true });

                        }
                    }
                    var deletedDays = subject.Results.Select(x => x.DayId).Except(subject.Days.Select(x=>x.Id));
                    context.Results.RemoveRange(subject.Results.Where(x => deletedDays.Any(u => x.DayId == u)));

                    var deletedstudents = subject.Results.Select(x => x.StudentId).Except(subject.SubjectStudents.Select(x => x.StudentId));
                    context.Results.RemoveRange(subject.Results.Where(x => deletedDays.Any(u => x.DayId == u)));
                }
                context.SaveChanges();
            }

            if (generateGrid)
            {
                grid.Columns.Clear();
                grid.Rows.Clear();
                grid.Columns.Add(new DataGridViewColumn()
                {
                    HeaderText = "Id",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    ReadOnly = true,
                    Visible = false,
                    Frozen = true
                });
                grid.Columns.Add(new DataGridViewColumn()
                {
                    HeaderText = "Ф.И.О.",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    ReadOnly = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                    Frozen = true
                });
                var marks = new List<string>() { "5", "4", "3", "2", "1", "0", "Н" };
                foreach (var day in subject.Days)
                {
                    if (day.DayType == DayType.Занятие)
                    {
                        var c = new DataGridViewComboBoxColumn()
                        {
                            HeaderText = day.Date.ToString("dd.MM"),
                            CellTemplate = new DataGridViewComboBoxCell(),
                            ReadOnly = false,
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
                        };
                        foreach (var m in marks)
                        {
                            c.Items.Add(m);
                        }
                        grid.Columns.Add(c);
                    }
                    else
                    {
                        var c = new DataGridViewComboBoxColumn()
                        {
                            HeaderText = day.DayType + " " + day.Date.ToString("dd.MM"),
                            CellTemplate = new DataGridViewComboBoxCell(),
                            ReadOnly = false,
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
                        };
                        foreach (var m in marks)
                        {
                            c.Items.Add(m);
                        }
                        grid.Columns.Add(c);
                    }
                }
                grid.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    HeaderText = "Посешаемость",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    ReadOnly = true
                });
                grid.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    HeaderText = "Успеваемость",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    ReadOnly = true
                });
                grid.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    HeaderText = "Итоговый балл",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    ReadOnly = true
                });
            }
            
            
            int index = 0;
            foreach (var student in subject.SubjectStudents)
            {
                if(generateGrid)
                index = grid.Rows.Add();
                grid.Rows[index].Cells[0].Value = student.Student.Id;
                grid.Rows[index].Cells[1].Value = student.Student.Fullname;
                int indexResult = 2;
                foreach (var result in subject.Results.Where(x => x.StudentId == student.StudentId))
                {
                    if (result.IsPresented)
                        grid.Rows[index].Cells[indexResult].Value = result.Mark.ToString();
                    else
                        grid.Rows[index].Cells[indexResult].Value = "Н";
                    indexResult++;
                }
                double attendance = 0;
                var results = subject.Results.Where(x => x.StudentId == student.StudentId);
                var total = results.Count();
                var a = results.Where(x => x.IsPresented).Count();
                attendance=((double)a / (double)total) * 60;
                grid.Rows[index].Cells[indexResult].Value = string.Format("{0:0.00}", attendance);

                double progress = 0;
                var types= results.GroupBy(x => x.Day.DayType).ToDictionary(g=>g.Key,g=>g.ToList());
                if (types.ContainsKey(DayType.Занятие)){
                    double r = 0;
                    types[DayType.Занятие].ForEach(x =>
                    {
                        r += Convert.ToDouble(x.Mark);
                    });
                    progress += r / (types[DayType.Занятие].Count()*5) * 20;
                }
                else
                {
                    progress += 20;
                }

                if (types.ContainsKey(DayType.СамРабота))
                {
                    double r = 0;
                    types[DayType.СамРабота].ForEach(x =>
                    {
                        r += Convert.ToDouble(x.Mark);
                    });
                    progress += r / (types[DayType.СамРабота].Count() * 5) * 10;
                }
                else
                {
                    progress += 10;
                }

                if (types.ContainsKey(DayType.Тест))
                {
                    double r = 0;
                    types[DayType.Тест].ForEach(x =>
                    {
                        r += Convert.ToDouble(x.Mark);
                    });
                    progress += r / (types[DayType.Тест].Count() * 5) * 10;
                }
                else
                {
                    progress += 10;
                }
                grid.Rows[index].Cells[indexResult+1].Value = string.Format("{0:0.00}", progress);
                grid.Rows[index].Cells[indexResult + 2].Value = string.Format("{0:0.00}", progress+attendance);
                index++;
                //grid.Rows
            }
        }
        private void SubjectsBtn_Click(object sender, EventArgs e)
        {
            new SubjectsGridForm().ShowDialog();

            RefreshGrid(true);
            RefreshSubjects();
        }

        private void StudentsBtn_Click(object sender, EventArgs e)
        {
            new StudentsGridForm().ShowDialog();

            RefreshGrid(true);
            RefreshSubjects();
        }

        private void ShowBtn_Click(object sender, EventArgs e)
        {
            RefreshGrid(true);
            RefreshSubjects();
        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            using (var context = new AppContext())
            {
                var studentId =Convert.ToInt32( grid.Rows[e.RowIndex].Cells[0].Value);
                subject = context.Subjects.Include(x => x.SubjectStudents.Select(u => u.Student)).
                    Include("Days").Include("Results").FirstOrDefault(x => x.Id == subject.Id);
                var results = context.Results.Where(x =>x.SubjectId==subject.Id&& x.StudentId == studentId).ToList();
                
                for (int i = 2; i < grid.Rows[e.RowIndex].Cells.Count-3; i++)
                {
                    if (grid.Rows[e.RowIndex].Cells[i].Value.ToString()=="Н")
                    {
                        results[i - 2].IsPresented = false;
                        results[i - 2].Mark = 0;
                    }
                    else
                    {
                        results[i - 2].IsPresented = true; ;
                        results[i - 2].Mark = Convert.ToInt32(grid.Rows[e.RowIndex].Cells[i].Value);
                    }
                    
                }
                context.SaveChanges();
                
                RefreshGrid(false);
            }
        }
    }
}

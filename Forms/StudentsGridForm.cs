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

namespace SessionLog.Forms
{
    public partial class StudentsGridForm : Form
    {
        public StudentsGridForm()
        {
            InitializeComponent();
            RefreshGrid();
        }
        public void RefreshGrid()
        {
            using (var context = new AppContext())
            {
                grid.Rows.Clear();
                foreach (var s in context.Students.ToArray())
                {
                    grid.Rows.Add(s.Id, s.Fullname);
                }
            }
        }
        private void GridMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                int currentMouseOverRow = grid.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow >= 0)
                {
                    m.MenuItems.Add(new MenuItem("Редактировать", new EventHandler(delegate (Object o, EventArgs a)
                    {
                        int id = (int)grid.Rows[currentMouseOverRow].Cells[0].Value;
                        var form = new StudentForm(id, EditState.Update);
                        form.ShowDialog();
                        RefreshGrid();

                    })));
                    m.MenuItems.Add(new MenuItem("Просмотр", new EventHandler(delegate (Object o, EventArgs a)
                    {
                        int id = (int)grid.Rows[currentMouseOverRow].Cells[0].Value;
                        var form = new StudentForm(id, EditState.View);
                        form.ShowDialog();
                        RefreshGrid();
                    })));
                    m.MenuItems.Add(new MenuItem("Удалить", new EventHandler(delegate (Object o, EventArgs a)
                    {
                        int id = (int)grid.Rows[currentMouseOverRow].Cells[0].Value;
                        using (var context = new AppContext())
                        {
                            context.Students.Remove(context.Students.FirstOrDefault(x => x.Id == id));
                            context.SaveChanges();
                        }
                        RefreshGrid();
                    })));
                }
                m.Show(grid, new Point(e.X, e.Y));

            }
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            new StudentForm().ShowDialog();
            RefreshGrid();
        }
    }
}

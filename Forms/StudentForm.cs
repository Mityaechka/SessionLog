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
    public partial class StudentForm : Form
    {
        int id;
        Student s = new Student();
        EditState state = EditState.Add;
        public StudentForm()
        {
            InitializeComponent();
            button.Text = "Добавить";
        }
        public StudentForm(int id,EditState state)
        {
            InitializeComponent();
            this.id = id;
            this.state = state;
            using(var context = new AppContext())
            {
                s = context.Students.Find(id);
                Fullname.Text = s.Fullname;
            }
            if(state == EditState.View)
            {
                Fullname.Enabled = false;
                button.Text = "Ок";
            }
            else
            {
                button.Text = "Сохранить";
            }
        }
        private void button_Click(object sender, EventArgs e)
        {
            using (var context = new AppContext())
            {
                s.Fullname = Fullname.Text;
                switch (state)
                {
                    case EditState.Add:
                        s.Fullname = Fullname.Text;
                        context.Students.Add(s);
                        break;
                    case EditState.Update:
                        s = context.Students.Find(id);
                        s.Fullname = Fullname.Text;
                        break;
                }
                context.SaveChanges();
            }
        }
    }
}

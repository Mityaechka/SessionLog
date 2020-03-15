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
    public partial class SubjectForm : Form
    {
        int id;
        Subject s = new Subject();
        EditState state = EditState.Add;
        public SubjectForm()
        {
            InitializeComponent();
            button.Text = "Добавить";
        }
        public SubjectForm(int id,EditState state)
        {
            InitializeComponent();
            this.id = id;
            this.state = state;
            using(var context = new AppContext())
            {
                s = context.Subjects.Find(id);
                Fullname.Text = s.Title;
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
                switch (state)
                {
                    case EditState.Add:
                        s.Title = Fullname.Text;
                        context.Subjects.Add(s);
                        break;
                    case EditState.Update:
                        s = context.Subjects.Find(id);
                        s.Title = Fullname.Text;
                        break;
                }
                context.SaveChanges();
            }
        }
    }
}

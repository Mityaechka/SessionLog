namespace SessionLog.Forms
{
    partial class LogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SubjectsBtn = new System.Windows.Forms.ToolStripButton();
            this.StudentsBtn = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.subjectSelect = new System.Windows.Forms.ComboBox();
            this.ShowBtn = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubjectsBtn,
            this.StudentsBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(926, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SubjectsBtn
            // 
            this.SubjectsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SubjectsBtn.Image = ((System.Drawing.Image)(resources.GetObject("SubjectsBtn.Image")));
            this.SubjectsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SubjectsBtn.Name = "SubjectsBtn";
            this.SubjectsBtn.Size = new System.Drawing.Size(68, 22);
            this.SubjectsBtn.Text = "Предметы";
            this.SubjectsBtn.Click += new System.EventHandler(this.SubjectsBtn_Click);
            // 
            // StudentsBtn
            // 
            this.StudentsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StudentsBtn.Image = ((System.Drawing.Image)(resources.GetObject("StudentsBtn.Image")));
            this.StudentsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StudentsBtn.Name = "StudentsBtn";
            this.StudentsBtn.Size = new System.Drawing.Size(63, 22);
            this.StudentsBtn.Text = "Студенты";
            this.StudentsBtn.Click += new System.EventHandler(this.StudentsBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Предмет";
            // 
            // subjectSelect
            // 
            this.subjectSelect.FormattingEnabled = true;
            this.subjectSelect.Location = new System.Drawing.Point(70, 25);
            this.subjectSelect.Name = "subjectSelect";
            this.subjectSelect.Size = new System.Drawing.Size(121, 21);
            this.subjectSelect.TabIndex = 2;
            // 
            // ShowBtn
            // 
            this.ShowBtn.Location = new System.Drawing.Point(199, 23);
            this.ShowBtn.Name = "ShowBtn";
            this.ShowBtn.Size = new System.Drawing.Size(75, 23);
            this.ShowBtn.TabIndex = 3;
            this.ShowBtn.Text = "Показать";
            this.ShowBtn.UseVisualStyleBackColor = true;
            this.ShowBtn.Click += new System.EventHandler(this.ShowBtn_Click);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(16, 53);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(898, 198);
            this.grid.TabIndex = 4;
            this.grid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 263);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.ShowBtn);
            this.Controls.Add(this.subjectSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LogForm";
            this.Text = "Журнал";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SubjectsBtn;
        private System.Windows.Forms.ToolStripButton StudentsBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox subjectSelect;
        private System.Windows.Forms.Button ShowBtn;
        private System.Windows.Forms.DataGridView grid;
    }
}
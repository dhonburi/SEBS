namespace SEBS.App
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStudent = new Button();
            btnStaff = new Button();
            SuspendLayout();
            // 
            // btnStudent
            // 
            btnStudent.Location = new Point(27, 26);
            btnStudent.Name = "btnStudent";
            btnStudent.Size = new Size(94, 29);
            btnStudent.TabIndex = 0;
            btnStudent.Text = "Student";
            btnStudent.UseVisualStyleBackColor = true;
            btnStudent.Click += btnStudent_Click;
            // 
            // btnStaff
            // 
            btnStaff.Location = new Point(127, 26);
            btnStaff.Name = "btnStaff";
            btnStaff.Size = new Size(94, 29);
            btnStaff.TabIndex = 1;
            btnStaff.Text = "Staff";
            btnStaff.UseVisualStyleBackColor = true;
            btnStaff.Click += btnStaff_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(255, 82);
            Controls.Add(btnStaff);
            Controls.Add(btnStudent);
            Name = "MainForm";
            Text = "SEBS Launcher";
            ResumeLayout(false);
        }

        #endregion

        private Button btnStudent;
        private Button btnStaff;
    }
}

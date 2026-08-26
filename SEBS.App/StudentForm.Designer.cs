namespace SEBS.App
{
    partial class StudentForm
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
            dgvEquipment = new DataGridView();
            label1 = new Label();
            txtStudentId = new TextBox();
            label2 = new Label();
            dtpDueDate = new DateTimePicker();
            btnBook = new Button();
            dgvMyBookings = new DataGridView();
            label3 = new Label();
            label4 = new Label();
            lblStatus2 = new Label();
            btnCancelBooking = new Button();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvEquipment).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMyBookings).BeginInit();
            SuspendLayout();
            // 
            // dgvEquipment
            // 
            dgvEquipment.AllowUserToAddRows = false;
            dgvEquipment.AllowUserToDeleteRows = false;
            dgvEquipment.AllowUserToResizeRows = false;
            dgvEquipment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEquipment.Location = new Point(13, 37);
            dgvEquipment.Name = "dgvEquipment";
            dgvEquipment.ReadOnly = true;
            dgvEquipment.RowHeadersWidth = 51;
            dgvEquipment.Size = new Size(858, 201);
            dgvEquipment.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 254);
            label1.Name = "label1";
            label1.Size = new Size(82, 20);
            label1.TabIndex = 1;
            label1.Text = "Student ID:";
            label1.Click += label1_Click;
            // 
            // txtStudentId
            // 
            txtStudentId.Location = new Point(27, 277);
            txtStudentId.Name = "txtStudentId";
            txtStudentId.Size = new Size(125, 27);
            txtStudentId.TabIndex = 2;
            txtStudentId.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(175, 254);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 3;
            label2.Text = "Due Date:";
            label2.Click += label2_Click;
            // 
            // dtpDueDate
            // 
            dtpDueDate.Format = DateTimePickerFormat.Short;
            dtpDueDate.Location = new Point(175, 277);
            dtpDueDate.Name = "dtpDueDate";
            dtpDueDate.Size = new Size(250, 27);
            dtpDueDate.TabIndex = 5;
            // 
            // btnBook
            // 
            btnBook.Location = new Point(462, 254);
            btnBook.Name = "btnBook";
            btnBook.Size = new Size(183, 65);
            btnBook.TabIndex = 6;
            btnBook.Text = "Book Selected Equipment";
            btnBook.UseVisualStyleBackColor = true;
            btnBook.Click += btnBook_Click;
            // 
            // dgvMyBookings
            // 
            dgvMyBookings.AllowUserToAddRows = false;
            dgvMyBookings.AllowUserToDeleteRows = false;
            dgvMyBookings.AllowUserToResizeRows = false;
            dgvMyBookings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyBookings.Location = new Point(13, 354);
            dgvMyBookings.Name = "dgvMyBookings";
            dgvMyBookings.ReadOnly = true;
            dgvMyBookings.RowHeadersWidth = 51;
            dgvMyBookings.Size = new Size(858, 201);
            dgvMyBookings.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 9);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 8;
            label3.Text = "Equipment:";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 324);
            label4.Name = "label4";
            label4.Size = new Size(73, 20);
            label4.TabIndex = 9;
            label4.Text = "Bookings:";
            // 
            // lblStatus2
            // 
            lblStatus2.AutoSize = true;
            lblStatus2.Location = new Point(216, 592);
            lblStatus2.Name = "lblStatus2";
            lblStatus2.Size = new Size(0, 20);
            lblStatus2.TabIndex = 10;
            lblStatus2.Click += label5_Click;
            // 
            // btnCancelBooking
            // 
            btnCancelBooking.Location = new Point(27, 564);
            btnCancelBooking.Name = "btnCancelBooking";
            btnCancelBooking.Size = new Size(173, 77);
            btnCancelBooking.TabIndex = 11;
            btnCancelBooking.Text = "Cancel Selected Booking";
            btnCancelBooking.UseVisualStyleBackColor = true;
            btnCancelBooking.Click += btnCancelBooking_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(663, 277);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 20);
            lblStatus.TabIndex = 12;
            // 
            // StudentForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 653);
            Controls.Add(lblStatus);
            Controls.Add(btnCancelBooking);
            Controls.Add(lblStatus2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(dgvMyBookings);
            Controls.Add(btnBook);
            Controls.Add(dtpDueDate);
            Controls.Add(label2);
            Controls.Add(txtStudentId);
            Controls.Add(label1);
            Controls.Add(dgvEquipment);
            Name = "StudentForm";
            Text = "StudentForm";
            ((System.ComponentModel.ISupportInitialize)dgvEquipment).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMyBookings).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvEquipment;
        private Label label1;
        private TextBox txtStudentId;
        private Label label2;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dtpDueDate;
        private Button btnBook;
        private DataGridView dgvMyBookings;
        private Label label3;
        private Label label4;
        private Label lblStatus2;
        private Button btnCancelBooking;
        private Label lblStatus;
    }
}
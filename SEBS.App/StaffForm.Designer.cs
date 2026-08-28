namespace SEBS.App
{
    partial class StaffForm
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
            label1 = new Label();
            txtStaffId = new TextBox();
            label2 = new Label();
            dgvBookings = new DataGridView();
            btnCheckIn = new Button();
            lblCheckInStatus = new Label();
            btnCheckInDamaged = new Button();
            label3 = new Label();
            dgvEquipment = new DataGridView();
            btnMarkRepaired = new Button();
            lblMarkRepairedStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvBookings).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEquipment).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 0;
            label1.Text = "Staff ID:";
            // 
            // txtStaffId
            // 
            txtStaffId.Location = new Point(80, 6);
            txtStaffId.Name = "txtStaffId";
            txtStaffId.Size = new Size(137, 27);
            txtStaffId.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 35);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 2;
            label2.Text = "Bookings:";
            // 
            // dgvBookings
            // 
            dgvBookings.AllowUserToAddRows = false;
            dgvBookings.AllowUserToDeleteRows = false;
            dgvBookings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBookings.Location = new Point(12, 59);
            dgvBookings.Name = "dgvBookings";
            dgvBookings.ReadOnly = true;
            dgvBookings.RowHeadersWidth = 51;
            dgvBookings.Size = new Size(908, 260);
            dgvBookings.TabIndex = 3;
            // 
            // btnCheckIn
            // 
            btnCheckIn.Location = new Point(12, 325);
            btnCheckIn.Name = "btnCheckIn";
            btnCheckIn.Size = new Size(94, 29);
            btnCheckIn.TabIndex = 0;
            btnCheckIn.Text = "Check In";
            btnCheckIn.UseVisualStyleBackColor = true;
            btnCheckIn.Click += btnCheckIn_Click;
            // 
            // lblCheckInStatus
            // 
            lblCheckInStatus.AutoSize = true;
            lblCheckInStatus.Location = new Point(287, 329);
            lblCheckInStatus.Name = "lblCheckInStatus";
            lblCheckInStatus.Size = new Size(0, 20);
            lblCheckInStatus.TabIndex = 4;
            // 
            // btnCheckInDamaged
            // 
            btnCheckInDamaged.Location = new Point(112, 325);
            btnCheckInDamaged.Name = "btnCheckInDamaged";
            btnCheckInDamaged.Size = new Size(166, 29);
            btnCheckInDamaged.TabIndex = 5;
            btnCheckInDamaged.Text = "Check In (Damaged)";
            btnCheckInDamaged.UseVisualStyleBackColor = true;
            btnCheckInDamaged.Click += btnCheckInDamaged_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 363);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 6;
            label3.Text = "Equipment:";
            // 
            // dgvEquipment
            // 
            dgvEquipment.AllowUserToAddRows = false;
            dgvEquipment.AllowUserToDeleteRows = false;
            dgvEquipment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEquipment.Location = new Point(12, 386);
            dgvEquipment.Name = "dgvEquipment";
            dgvEquipment.ReadOnly = true;
            dgvEquipment.RowHeadersWidth = 51;
            dgvEquipment.Size = new Size(908, 260);
            dgvEquipment.TabIndex = 7;
            // 
            // btnMarkRepaired
            // 
            btnMarkRepaired.Location = new Point(12, 652);
            btnMarkRepaired.Name = "btnMarkRepaired";
            btnMarkRepaired.Size = new Size(205, 29);
            btnMarkRepaired.TabIndex = 8;
            btnMarkRepaired.Text = "Mark Equipment Repaired";
            btnMarkRepaired.UseVisualStyleBackColor = true;
            btnMarkRepaired.Click += btnMarkRepaired_Click;
            // 
            // lblMarkRepairedStatus
            // 
            lblMarkRepairedStatus.AutoSize = true;
            lblMarkRepairedStatus.Location = new Point(226, 656);
            lblMarkRepairedStatus.Name = "lblMarkRepairedStatus";
            lblMarkRepairedStatus.Size = new Size(0, 20);
            lblMarkRepairedStatus.TabIndex = 9;
            // 
            // StaffForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(932, 703);
            Controls.Add(lblMarkRepairedStatus);
            Controls.Add(btnMarkRepaired);
            Controls.Add(dgvEquipment);
            Controls.Add(label3);
            Controls.Add(btnCheckInDamaged);
            Controls.Add(lblCheckInStatus);
            Controls.Add(btnCheckIn);
            Controls.Add(dgvBookings);
            Controls.Add(label2);
            Controls.Add(txtStaffId);
            Controls.Add(label1);
            Name = "StaffForm";
            Text = "StaffForm";
            ((System.ComponentModel.ISupportInitialize)dgvBookings).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEquipment).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtStaffId;
        private Label label2;
        private DataGridView dgvBookings;
        private Button btnCheckIn;
        private Label lblCheckInStatus;
        private Button btnCheckInDamaged;
        private Label label3;
        private DataGridView dgvEquipment;
        private Button btnMarkRepaired;
        private Button button1;
        private Label lblMarkRepairedStatus;
    }
}
namespace SEBS.App
{
    partial class StaffLoginForm
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
            lblLoginStaffId = new Label();
            txtLoginStaffId = new TextBox();
            lblPin = new Label();
            txtPin = new TextBox();
            btnSignIn = new Button();
            btnCancelLogin = new Button();
            lblLoginStatus = new Label();
            SuspendLayout();
            // 
            // lblLoginStaffId
            // 
            lblLoginStaffId.AutoSize = true;
            lblLoginStaffId.Location = new Point(20, 23);
            lblLoginStaffId.Name = "lblLoginStaffId";
            lblLoginStaffId.Size = new Size(66, 20);
            lblLoginStaffId.TabIndex = 0;
            lblLoginStaffId.Text = "Staff ID:";
            // 
            // txtLoginStaffId
            // 
            txtLoginStaffId.Location = new Point(100, 20);
            txtLoginStaffId.Name = "txtLoginStaffId";
            txtLoginStaffId.Size = new Size(180, 27);
            txtLoginStaffId.TabIndex = 1;
            // 
            // lblPin
            // 
            lblPin.AutoSize = true;
            lblPin.Location = new Point(20, 63);
            lblPin.Name = "lblPin";
            lblPin.Size = new Size(35, 20);
            lblPin.TabIndex = 2;
            lblPin.Text = "PIN:";
            // 
            // txtPin
            // 
            txtPin.Location = new Point(100, 60);
            txtPin.MaxLength = 8;
            txtPin.Name = "txtPin";
            txtPin.Size = new Size(180, 27);
            txtPin.TabIndex = 3;
            txtPin.UseSystemPasswordChar = true;
            // 
            // btnSignIn
            // 
            btnSignIn.Location = new Point(100, 100);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new Size(85, 32);
            btnSignIn.TabIndex = 4;
            btnSignIn.Text = "Sign In";
            btnSignIn.UseVisualStyleBackColor = true;
            btnSignIn.Click += btnSignIn_Click;
            // 
            // btnCancelLogin
            // 
            btnCancelLogin.DialogResult = DialogResult.Cancel;
            btnCancelLogin.Location = new Point(195, 100);
            btnCancelLogin.Name = "btnCancelLogin";
            btnCancelLogin.Size = new Size(85, 32);
            btnCancelLogin.TabIndex = 5;
            btnCancelLogin.Text = "Cancel";
            btnCancelLogin.UseVisualStyleBackColor = true;
            // 
            // lblLoginStatus
            // 
            lblLoginStatus.AutoSize = true;
            lblLoginStatus.ForeColor = Color.DarkRed;
            lblLoginStatus.Location = new Point(20, 145);
            lblLoginStatus.Name = "lblLoginStatus";
            lblLoginStatus.Size = new Size(0, 20);
            lblLoginStatus.TabIndex = 6;
            // 
            // StaffLoginForm
            // 
            AcceptButton = btnSignIn;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelLogin;
            ClientSize = new Size(310, 180);
            Controls.Add(lblLoginStatus);
            Controls.Add(btnCancelLogin);
            Controls.Add(btnSignIn);
            Controls.Add(txtPin);
            Controls.Add(lblPin);
            Controls.Add(txtLoginStaffId);
            Controls.Add(lblLoginStaffId);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StaffLoginForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Staff Sign In";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLoginStaffId;
        private TextBox txtLoginStaffId;
        private Label lblPin;
        private TextBox txtPin;
        private Button btnSignIn;
        private Button btnCancelLogin;
        private Label lblLoginStatus;
    }
}

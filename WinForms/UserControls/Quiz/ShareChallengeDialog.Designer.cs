namespace WinForms.UserControls.Quiz
{
    partial class ShareChallengeDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlCard = new Panel();
            lblDeckName = new Label();
            lblPinCode = new Label();
            txtMessage = new TextBox();
            btnConfirm = new Button();
            btnCancel = new Button();
            lblTitlePopup = new Label();
            lblPrivacyTitle = new Label();
            cbbPrivacy = new ComboBox();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.FromArgb(18, 38, 44);
            pnlCard.BorderStyle = BorderStyle.FixedSingle;
            pnlCard.Controls.Add(lblDeckName);
            pnlCard.Controls.Add(lblPinCode);
            pnlCard.Location = new Point(29, 60);
            pnlCard.Margin = new Padding(3, 4, 3, 4);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(457, 333);
            pnlCard.TabIndex = 0;
            // 
            // lblDeckName
            // 
            lblDeckName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDeckName.ForeColor = Color.FromArgb(193, 225, 127);
            lblDeckName.Location = new Point(11, 27);
            lblDeckName.Name = "lblDeckName";
            lblDeckName.Size = new Size(434, 53);
            lblDeckName.TabIndex = 0;
            lblDeckName.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblPinCode
            // 
            lblPinCode.Font = new Font("Segoe UI Black", 36F, FontStyle.Bold);
            lblPinCode.ForeColor = Color.White;
            lblPinCode.Location = new Point(11, 80);
            lblPinCode.Name = "lblPinCode";
            lblPinCode.Size = new Size(434, 133);
            lblPinCode.TabIndex = 1;
            lblPinCode.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtMessage
            // 
            txtMessage.BackColor = Color.FromArgb(30, 50, 55);
            txtMessage.BorderStyle = BorderStyle.None;
            txtMessage.Font = new Font("Segoe UI", 10F);
            txtMessage.ForeColor = Color.White;
            txtMessage.Location = new Point(29, 440);
            txtMessage.Margin = new Padding(3, 4, 3, 4);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.PlaceholderText = "Bạn muốn nhắn gì đến đối thủ? (VD: Solo thắng mình không?)...";
            txtMessage.Size = new Size(457, 107);
            txtMessage.TabIndex = 1;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.FromArgb(24, 119, 242);
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(263, 573);
            btnConfirm.Margin = new Padding(3, 4, 3, 4);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(223, 60);
            btnConfirm.TabIndex = 2;
            btnConfirm.Text = "ĐĂNG NGAY 🚀";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.ForeColor = Color.Gray;
            btnCancel.Location = new Point(29, 573);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(223, 60);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "HỦY BỎ";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblTitlePopup
            // 
            lblTitlePopup.Dock = DockStyle.Top;
            lblTitlePopup.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitlePopup.ForeColor = Color.White;
            lblTitlePopup.Location = new Point(2, 3);
            lblTitlePopup.Name = "lblTitlePopup";
            lblTitlePopup.Size = new Size(529, 67);
            lblTitlePopup.TabIndex = 4;
            lblTitlePopup.Text = "CHIA SẺ THÁCH ĐẤU";
            lblTitlePopup.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPrivacyTitle
            // 
            lblPrivacyTitle.AutoSize = true;
            lblPrivacyTitle.Font = new Font("Segoe UI", 9F);
            lblPrivacyTitle.ForeColor = Color.Gray;
            lblPrivacyTitle.Location = new Point(29, 367);
            lblPrivacyTitle.Name = "lblPrivacyTitle";
            lblPrivacyTitle.Size = new Size(197, 20);
            lblPrivacyTitle.TabIndex = 5;
            lblPrivacyTitle.Text = "Ai có thể thấy bài đăng này?";
            // 
            // cbbPrivacy
            // 
            cbbPrivacy.BackColor = Color.FromArgb(30, 50, 55);
            cbbPrivacy.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbPrivacy.FlatStyle = FlatStyle.Flat;
            cbbPrivacy.Font = new Font("Segoe UI", 10F);
            cbbPrivacy.ForeColor = Color.White;
            cbbPrivacy.Items.AddRange(new object[] { "🌍 Công khai", "👥 Bạn bè (Followers)" });
            cbbPrivacy.Location = new Point(29, 401);
            cbbPrivacy.Margin = new Padding(3, 4, 3, 4);
            cbbPrivacy.Name = "cbbPrivacy";
            cbbPrivacy.Size = new Size(457, 31);
            cbbPrivacy.TabIndex = 6;
            // 
            // ShareChallengeDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(10, 25, 29);
            ClientSize = new Size(533, 667);
            Controls.Add(lblTitlePopup);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(txtMessage);
            Controls.Add(pnlCard);
            Controls.Add(lblPrivacyTitle);
            Controls.Add(cbbPrivacy);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "ShareChallengeDialog";
            Padding = new Padding(2, 3, 2, 3);
            StartPosition = FormStartPosition.CenterParent;
            pnlCard.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblDeckName;
        private System.Windows.Forms.Label lblPinCode;
        private System.Windows.Forms.Label lblTitlePopup;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbbPrivacy;
        private System.Windows.Forms.Label lblPrivacyTitle;
    }
}
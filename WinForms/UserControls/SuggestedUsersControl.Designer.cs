namespace WinForms.UserControls
{
    partial class SuggestedUsersControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private FlowLayoutPanel flpUsers;
        private Label lblTitle;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            flpUsers = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(10);
            lblTitle.Size = new Size(377, 45);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "👥 Gợi ý theo dõi";
            // 
            // flpUsers
            // 
            flpUsers.AutoScroll = true;
            flpUsers.BackColor = Color.White;
            flpUsers.Dock = DockStyle.Fill;
            flpUsers.FlowDirection = FlowDirection.TopDown;
            flpUsers.Location = new Point(0, 45);
            flpUsers.Name = "flpUsers";
            flpUsers.Size = new Size(377, 290);
            flpUsers.TabIndex = 0;
            flpUsers.WrapContents = false;
            // 
            // SuggestedUsersControl
            // 
            Controls.Add(flpUsers);
            Controls.Add(lblTitle);
            Name = "SuggestedUsersControl";
            Size = new Size(377, 335);
            ResumeLayout(false);
        }

        #endregion
    }
}

namespace WinForms.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitContainer1; // Chỉ giữ lại cái này
        private System.Windows.Forms.FlowLayoutPanel menuPanel;
        private System.Windows.Forms.Panel contentPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            menuPanel = new System.Windows.Forms.FlowLayoutPanel();
            contentPanel = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();

            // ================= splitContainer1 =================
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Size = new System.Drawing.Size(1129, 483);
            splitContainer1.SplitterDistance = 192; // Độ rộng menu bên trái
            splitContainer1.TabIndex = 0;

            // ================= menuPanel (Panel bên trái) =================
            menuPanel.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            menuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            menuPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            menuPanel.Name = "menuPanel";
            menuPanel.WrapContents = false;
            menuPanel.AutoScroll = true;
            splitContainer1.Panel1.Controls.Add(menuPanel);

            // ================= contentPanel (Vùng hiển thị chính) =================
            contentPanel.BackColor = System.Drawing.Color.White;
            contentPanel.Dock = System.Windows.Forms.DockStyle.Fill; // Chiếm toàn bộ Panel2
            contentPanel.Location = new System.Drawing.Point(0, 0);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new System.Drawing.Size(933, 483);

            // QUAN TRỌNG: Thêm trực tiếp vào Panel2 của splitContainer1
            splitContainer1.Panel2.Controls.Add(contentPanel);

            // ================= MainForm =================
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1129, 483);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Text = "StudyApp";
            Load += MainForm_Load;

            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);

            // Tạo TabControl
            var tabControl = new TabControl
            {
                Name = "tabControl1",
                Dock = DockStyle.Fill
            };

            // Thêm các tab hiện có (Learn, User, v.v.)
            var tabLearn = new TabPage("📚 Học tập");
            // ...  thêm controls cho tab Learn

            var tabUser = new TabPage("👤 Người dùng");
            // ... thêm controls cho tab User

            tabControl.TabPages.Add(tabLearn);
            tabControl.TabPages.Add(tabUser);

            this.Controls.Add(tabControl);
        }
    }
}
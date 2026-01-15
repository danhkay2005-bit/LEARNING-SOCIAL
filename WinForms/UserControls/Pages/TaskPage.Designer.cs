namespace WinForms.UserControls.Pages
{
    partial class TaskPage
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
        private void InitializeComponent()
        {
            TControlTask = new TabControl();
            tpHangNgay = new TabPage();
            flpDaily = new FlowLayoutPanel();
            tbHangTuan = new TabPage();
            flpWeekly = new FlowLayoutPanel();
            tbAchievements = new TabPage();
            flpAchievement = new FlowLayoutPanel();
            tpEvent = new TabPage();
            flpEvent = new FlowLayoutPanel();
            TControlTask.SuspendLayout();
            tpHangNgay.SuspendLayout();
            tbHangTuan.SuspendLayout();
            tbAchievements.SuspendLayout();
            tpEvent.SuspendLayout();
            SuspendLayout();
            // 
            // TControlTask
            // 
            TControlTask.Controls.Add(tpHangNgay);
            TControlTask.Controls.Add(tbHangTuan);
            TControlTask.Controls.Add(tbAchievements);
            TControlTask.Controls.Add(tpEvent);
            TControlTask.Dock = DockStyle.Fill;
            TControlTask.Location = new Point(0, 0);
            TControlTask.Name = "TControlTask";
            TControlTask.SelectedIndex = 0;
            TControlTask.Size = new Size(983, 547);
            TControlTask.TabIndex = 0;
            // 
            // tpHangNgay
            // 
            tpHangNgay.Controls.Add(flpDaily);
            tpHangNgay.Location = new Point(4, 24);
            tpHangNgay.Name = "tpHangNgay";
            tpHangNgay.Padding = new Padding(3);
            tpHangNgay.Size = new Size(975, 519);
            tpHangNgay.TabIndex = 0;
            tpHangNgay.Text = "Hàng Ngày";
            tpHangNgay.UseVisualStyleBackColor = true;
            // 
            // flpDaily
            // 
            flpDaily.AutoScroll = true;
            flpDaily.Dock = DockStyle.Fill;
            flpDaily.FlowDirection = FlowDirection.TopDown;
            flpDaily.Location = new Point(3, 3);
            flpDaily.Name = "flpDaily";
            flpDaily.Size = new Size(969, 513);
            flpDaily.TabIndex = 0;
            flpDaily.WrapContents = false;
            // 
            // tbHangTuan
            // 
            tbHangTuan.Controls.Add(flpWeekly);
            tbHangTuan.Location = new Point(4, 24);
            tbHangTuan.Name = "tbHangTuan";
            tbHangTuan.Padding = new Padding(3);
            tbHangTuan.Size = new Size(975, 519);
            tbHangTuan.TabIndex = 1;
            tbHangTuan.Text = "Hàng Tuần";
            tbHangTuan.UseVisualStyleBackColor = true;
            // 
            // flpWeekly
            // 
            flpWeekly.AutoScroll = true;
            flpWeekly.Dock = DockStyle.Fill;
            flpWeekly.FlowDirection = FlowDirection.TopDown;
            flpWeekly.Location = new Point(3, 3);
            flpWeekly.Name = "flpWeekly";
            flpWeekly.Size = new Size(969, 513);
            flpWeekly.TabIndex = 0;
            flpWeekly.WrapContents = false;
            // 
            // tbAchievements
            // 
            tbAchievements.Controls.Add(flpAchievement);
            tbAchievements.Location = new Point(4, 24);
            tbAchievements.Name = "tbAchievements";
            tbAchievements.Padding = new Padding(3);
            tbAchievements.Size = new Size(975, 519);
            tbAchievements.TabIndex = 2;
            tbAchievements.Text = "Thành Tựu";
            tbAchievements.UseVisualStyleBackColor = true;
            // 
            // flpAchievement
            // 
            flpAchievement.AutoScroll = true;
            flpAchievement.Dock = DockStyle.Fill;
            flpAchievement.FlowDirection = FlowDirection.TopDown;
            flpAchievement.Location = new Point(3, 3);
            flpAchievement.Name = "flpAchievement";
            flpAchievement.Size = new Size(969, 513);
            flpAchievement.TabIndex = 0;
            flpAchievement.WrapContents = false;
            // 
            // tpEvent
            // 
            tpEvent.Controls.Add(flpEvent);
            tpEvent.Location = new Point(4, 24);
            tpEvent.Name = "tpEvent";
            tpEvent.Padding = new Padding(3);
            tpEvent.Size = new Size(975, 519);
            tpEvent.TabIndex = 3;
            tpEvent.Text = "Sự kiện";
            tpEvent.UseVisualStyleBackColor = true;
            // 
            // flpEvent
            // 
            flpEvent.AutoScroll = true;
            flpEvent.Dock = DockStyle.Fill;
            flpEvent.FlowDirection = FlowDirection.TopDown;
            flpEvent.Location = new Point(3, 3);
            flpEvent.Name = "flpEvent";
            flpEvent.Size = new Size(969, 513);
            flpEvent.TabIndex = 0;
            flpEvent.WrapContents = false;
            // 
            // TaskPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TControlTask);
            Name = "TaskPage";
            Size = new Size(983, 547);
            Load += TaskPage_Load;
            TControlTask.ResumeLayout(false);
            tpHangNgay.ResumeLayout(false);
            tbHangTuan.ResumeLayout(false);
            tbAchievements.ResumeLayout(false);
            tpEvent.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl TControlTask;
        private TabPage tpHangNgay;
        private FlowLayoutPanel flpDaily;
        private TabPage tbHangTuan;
        private FlowLayoutPanel flpWeekly;
        private TabPage tbAchievements;
        private FlowLayoutPanel flpAchievement;
        private TabPage tpEvent;
        private FlowLayoutPanel flpEvent;
    }
}

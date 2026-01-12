using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;

        // THÊM
        private FlowLayoutPanel menuPanel;
        private Panel contentPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();

            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.SuspendLayout();

            SuspendLayout();

            // ================= splitContainer1 =================
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Size = new Size(1129, 483);
            splitContainer1.SplitterDistance = 192; // GIỮ NGUYÊN
            splitContainer1.TabIndex = 0;

            // ================= splitContainer2 =================
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Size = new Size(933, 483);
            splitContainer2.SplitterDistance = 751; // GIỮ NGUYÊN
            splitContainer2.TabIndex = 0;

            splitContainer1.Panel2.Controls.Add(splitContainer2);

            // ================= LEFT MENU PANEL =================
            menuPanel = new FlowLayoutPanel();
            menuPanel.Dock = DockStyle.Fill;
            menuPanel.FlowDirection = FlowDirection.TopDown;
            menuPanel.WrapContents = false;
            menuPanel.AutoScroll = true;
            menuPanel.BackColor = Color.FromArgb(45, 45, 48);

            splitContainer1.Panel1.Controls.Add(menuPanel);

            // ================= CONTENT PANEL =================
            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.White;

            splitContainer2.Panel1.Controls.Add(contentPanel);

            // ================= MainForm =================
            ClientSize = new Size(1129, 483);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Text = "StudyApp";
            Load += MainForm_Load;

            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);

            ResumeLayout(false);
        }
    }
}

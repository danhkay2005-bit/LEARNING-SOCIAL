using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.Forms
{
    public partial class MainForm : Form
    {
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
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1129, 483);
            splitContainer1.SplitterDistance = 192;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Size = new Size(933, 483);
            splitContainer2.SplitterDistance = 751;
            splitContainer2.TabIndex = 0;
            // 
            // MainForm
            // 
            ClientSize = new Size(1129, 483);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);

        }

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
    }
}

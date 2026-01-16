using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms.UserControls.Tasks
{
    public partial class RewardPopupTask : Form
    {
        public RewardPopupTask(string message)
        {
            InitializeComponent();
            lblMessage.Text = message;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

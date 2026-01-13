using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WinForms.Forms;
using WinForms.UserControls.Quiz;

namespace WinForms.UserControls.Pages
{
    public partial class HocTapPage : UserControl
    {
        public HocTapPage()
        {
            InitializeComponent();
        }

        private void btnTaoQuiz_Click(object sender, EventArgs e)
        {
            // Tìm Form chính đang chứa User Control này
            var mainForm = this.ParentForm as MainForm;

            if (mainForm != null)
            {
                // Giả sử bạn đã tạo một UserControl tên là TaoQuizPage
                mainForm.LoadPage(new TaoQuizPage());
            }
        }
    }
}

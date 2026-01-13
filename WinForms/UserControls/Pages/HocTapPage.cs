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
            // 1. Tìm Form chính
            var mainForm = this.ParentForm as MainForm;

            if (mainForm != null)
            {
                // 2. Lấy TaoQuizPage từ DI Container (ServiceProvider)
                // Việc này đảm bảo TaoQuizPage được nạp sẵn BoDeHocService vào constructor
                var serviceProvider = Program.ServiceProvider;
                if (serviceProvider == null)
                {
                    MessageBox.Show("Lỗi: ServiceProvider chưa được khởi tạo!", "Thông báo");
                    return;
                }

                var taoQuizPage = serviceProvider.GetService(typeof(TaoQuizPage)) as TaoQuizPage;

                if (taoQuizPage != null)
                {
                    // 3. Load trang vào vùng hiển thị chính
                    mainForm.LoadPage(taoQuizPage);
                }
                else
                {
                    MessageBox.Show("Lỗi: Chưa đăng ký TaoQuizPage trong hệ thống Dependency Injection!", "Thông báo");
                }
            }
        }
    }
}

using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class TracNghiemEditorControl : UserControl, IQuestionEditor
    {
        public TracNghiemEditorControl()
        {
            InitializeComponent();
            SetupEvents();
        }

        private void SetupEvents()
        {
            // Xử lý chọn ảnh khi nhấn nút
            btnThemAnh.Click += (s, e) =>
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        picCauHoi.Image = Image.FromFile(ofd.FileName);
                        // Lưu đường dẫn ảnh vào Tag để GetQuestionData có thể lấy
                        picCauHoi.Tag = ofd.FileName;
                    }
                }
            };
        }

        /// <summary>
        /// THỰC THI INTERFACE: Thu thập dữ liệu từ giao diện nhập liệu
        /// </summary>
        public ChiTietTheRequest GetQuestionData()
        {
            // Xác định nội dung đáp án đúng để lưu vào MatSau
            string dapAnDung = "";
            if (rbA.Checked) dapAnDung = txtDapAnA.Text.Trim();
            else if (rbB.Checked) dapAnDung = txtDapAnB.Text.Trim();
            else if (rbC.Checked) dapAnDung = txtDapAnC.Text.Trim();
            else if (rbD.Checked) dapAnDung = txtDapAnD.Text.Trim();

            return new ChiTietTheRequest
            {
                TheChinh = new TaoTheFlashcardRequest
                {
                    LoaiThe = LoaiTheEnum.TracNghiem,
                    MatTruoc = txtCauHoi.Text.Trim(), // Lưu câu hỏi vào mặt trước
                    MatSau = dapAnDung,             // Lưu đáp án đúng vào mặt sau
                    HinhAnhTruoc = picCauHoi.Tag?.ToString(), // Ảnh đi kèm câu hỏi
                    ThuTu = 0 // Bạn có thể lấy từ index nếu cần
                },
                DapAnTracNghiem = new List<TaoDapAnTracNghiemRequest>
        {
            new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnA.Text.Trim(), LaDapAnDung = rbA.Checked },
            new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnB.Text.Trim(), LaDapAnDung = rbB.Checked },
            new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnC.Text.Trim(), LaDapAnDung = rbC.Checked },
            new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnD.Text.Trim(), LaDapAnDung = rbD.Checked }
        }
            };
        }

        public void SetQuestionData(ChiTietTheRequest data)
        {
            if (data == null || data.TheChinh == null) return;

            // Đổ dữ liệu vào mặt trước (Câu hỏi)
            txtCauHoi.Text = data.TheChinh.MatTruoc;

            // Hiển thị ảnh nếu có
            if (!string.IsNullOrEmpty(data.TheChinh.HinhAnhTruoc) && System.IO.File.Exists(data.TheChinh.HinhAnhTruoc))
            {
                picCauHoi.Image = Image.FromFile(data.TheChinh.HinhAnhTruoc);
                picCauHoi.Tag = data.TheChinh.HinhAnhTruoc;
            }

            // Đổ dữ liệu 4 đáp án
            if (data.DapAnTracNghiem != null && data.DapAnTracNghiem.Count >= 4)
            {
                txtDapAnA.Text = data.DapAnTracNghiem[0].NoiDung;
                rbA.Checked = data.DapAnTracNghiem[0].LaDapAnDung;

                txtDapAnB.Text = data.DapAnTracNghiem[1].NoiDung;
                rbB.Checked = data.DapAnTracNghiem[1].LaDapAnDung;

                txtDapAnC.Text = data.DapAnTracNghiem[2].NoiDung;
                rbC.Checked = data.DapAnTracNghiem[2].LaDapAnDung;

                txtDapAnD.Text = data.DapAnTracNghiem[3].NoiDung;
                rbD.Checked = data.DapAnTracNghiem[3].LaDapAnDung;
            }
        }
    }
}
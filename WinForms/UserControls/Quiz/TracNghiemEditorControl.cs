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
        private static readonly Random _random = new Random();
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
            // 1. Xác định nội dung đáp án đúng để lưu vào MatSau (Flashcard)
            string dapAnDung = "";
            if (rbA.Checked) dapAnDung = txtDapAnA.Text.Trim();
            else if (rbB.Checked) dapAnDung = txtDapAnB.Text.Trim();
            else if (rbC.Checked) dapAnDung = txtDapAnC.Text.Trim();
            else if (rbD.Checked) dapAnDung = txtDapAnD.Text.Trim();

            // 2. Tạo danh sách tạm thời chứa 4 đáp án từ UI
            var danhSachTam = new List<TaoDapAnTracNghiemRequest>
    {
        new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnA.Text.Trim(), LaDapAnDung = rbA.Checked },
        new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnB.Text.Trim(), LaDapAnDung = rbB.Checked },
        new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnC.Text.Trim(), LaDapAnDung = rbC.Checked },
        new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnD.Text.Trim(), LaDapAnDung = rbD.Checked }
    };

            // 3. THỰC HIỆN TRỘN NGẪU NHIÊN (SHUFFLE)
            // OrderBy theo một số ngẫu nhiên để thay đổi vị trí các phần tử
            var danhSachDaTron = danhSachTam.OrderBy(x => _random.Next()).ToList();

            // 4. GÁN THỨ TỰ (ThuTu) SAU KHI TRỘN
            for (int i = 0; i < danhSachDaTron.Count; i++)
            {
                danhSachDaTron[i].ThuTu = i + 1; // Gán thứ tự 1, 2, 3, 4
            }

            // 5. Trả về object Request hoàn chỉnh
            return new ChiTietTheRequest
            {
                TheChinh = new TaoTheFlashcardRequest
                {
                    LoaiThe = LoaiTheEnum.TracNghiem,
                    MatTruoc = txtCauHoi.Text.Trim(),
                    MatSau = dapAnDung,
                    HinhAnhTruoc = picCauHoi.Tag?.ToString(),
                    ThuTu = 0 // Thứ tự của câu hỏi trong bộ đề (sẽ do TaoQuizPage quản lý)
                },
                DapAnTracNghiem = danhSachDaTron
            };
        }
        public void SetQuestionData(ChiTietTheRequest data)
        {
            if (data == null || data.TheChinh == null) return;

            // 1. Đổ dữ liệu câu hỏi
            txtCauHoi.Text = data.TheChinh.MatTruoc;

            // 2. Hiển thị ảnh nếu có
            if (!string.IsNullOrEmpty(data.TheChinh.HinhAnhTruoc) && System.IO.File.Exists(data.TheChinh.HinhAnhTruoc))
            {
                picCauHoi.Image = Image.FromFile(data.TheChinh.HinhAnhTruoc);
                picCauHoi.Tag = data.TheChinh.HinhAnhTruoc;
            }

            // 3. Đổ dữ liệu đáp án (Dựa trên flag LaDapAnDung)
            if (data.DapAnTracNghiem != null && data.DapAnTracNghiem.Count >= 4)
            {
                // Sắp xếp lại theo thứ tự cũ để đổ vào đúng ô A, B, C, D trên UI
                var sortedList = data.DapAnTracNghiem.OrderBy(x => x.ThuTu).ToList();

                txtDapAnA.Text = sortedList[0].NoiDung;
                rbA.Checked = sortedList[0].LaDapAnDung;

                txtDapAnB.Text = sortedList[1].NoiDung;
                rbB.Checked = sortedList[1].LaDapAnDung;

                txtDapAnC.Text = sortedList[2].NoiDung;
                rbC.Checked = sortedList[2].LaDapAnDung;

                txtDapAnD.Text = sortedList[3].NoiDung;
                rbD.Checked = sortedList[3].LaDapAnDung;
            }
        }
    }
}
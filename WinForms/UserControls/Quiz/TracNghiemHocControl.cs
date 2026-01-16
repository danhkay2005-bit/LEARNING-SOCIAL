using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class TracNghiemHocControl : UserControl, IQuestionControl
    {
        // Thuộc tính bắt buộc từ IQuestionControl
        public bool IsCorrect { get; private set; } = false;

        private List<Button> _optionButtons = new List<Button>();

        public TracNghiemHocControl(TheFlashcardResponse info, List<DapAnTracNghiemResponse> dapAns)
        {
            InitializeComponent();

            // Hiển thị mặt trước (Câu hỏi)
            lblMatTruoc.Text = info.MatTruoc;

            // Nạp danh sách đáp án
            RenderDapAn(dapAns);
        }

        private void RenderDapAn(List<DapAnTracNghiemResponse> dapAns)
        {
            flpDapAn.Controls.Clear();
            _optionButtons.Clear();

            foreach (var da in dapAns)
            {
                Button btn = new Button();
                btn.Text = da.NoiDung;
                btn.Tag = da.LaDapAnDung; // Lưu trữ đáp án đúng/sai vào Tag

                // Thiết kế Button Style
                btn.Size = new Size(600, 65); // Kích thước cố định cho đồng bộ
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.FromArgb(60, 80, 85);
                btn.ForeColor = Color.White;
                btn.BackColor = Color.FromArgb(25, 45, 50);
                btn.Font = new Font("Segoe UI", 12F);
                btn.Cursor = Cursors.Hand;
                btn.Margin = new Padding(0, 10, 0, 10);

                btn.Click += OptionButton_Click;

                _optionButtons.Add(btn);
                flpDapAn.Controls.Add(btn);
            }
        }

        private void OptionButton_Click(object? sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender!;

            // 1. Reset màu tất cả các nút về trạng thái bình thường
            foreach (var btn in _optionButtons)
            {
                btn.BackColor = Color.FromArgb(25, 45, 50);
                btn.FlatAppearance.BorderColor = Color.FromArgb(60, 80, 85);
            }

            // 2. Highlight nút đang được chọn (Màu xanh dương nhẹ)
            clickedBtn.BackColor = Color.FromArgb(40, 70, 80);
            clickedBtn.FlatAppearance.BorderColor = Color.FromArgb(193, 225, 127); // Màu Lime nhấn

            // 3. Cập nhật kết quả hiện tại
            IsCorrect = (clickedBtn.Tag is bool b && b);
        }

        /// <summary>
        /// Hiển thị kết quả đúng/sai (Tô màu xanh/đỏ)
        /// </summary>
        public void ShowResult()
        {
            foreach (var btn in _optionButtons)
            {
                bool isCorrectOption = btn.Tag is bool b && b;

                if (isCorrectOption)
                {
                    // Đáp án đúng luôn tô màu xanh lá
                    btn.BackColor = Color.FromArgb(46, 125, 50);
                    btn.FlatAppearance.BorderColor = Color.Lime;
                }
                else if (btn.BackColor == Color.FromArgb(40, 70, 80))
                {
                    // Nếu nút này được chọn nhưng nó sai -> Tô màu đỏ
                    btn.BackColor = Color.FromArgb(183, 28, 28);
                    btn.FlatAppearance.BorderColor = Color.Red;
                }

                btn.Enabled = false; // Khóa tương tác sau khi đã hiện kết quả
            }
        }
    }
}
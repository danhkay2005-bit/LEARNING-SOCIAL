using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; // Cần thêm System.Linq để dùng Any()
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class TracNghiemHocControl : UserControl, IQuestionControl
    {
        public bool IsCorrect { get; private set; } = false;

        // 1. THÊM BIẾN NÀY để lưu vết lựa chọn (không phụ thuộc màu sắc UI)
        private Button? _selectedButton = null;

        private List<Button> _optionButtons = new List<Button>();

        // HasAnswered dựa vào biến lưu trữ thay vì quét màu
        public bool HasAnswered => _selectedButton != null;

        public TracNghiemHocControl(TheFlashcardResponse info, List<DapAnTracNghiemResponse> dapAns)
        {
            InitializeComponent();
            lblMatTruoc.Text = info.MatTruoc;
            RenderDapAn(dapAns);
        }

        private void RenderDapAn(List<DapAnTracNghiemResponse> dapAns)
        {
            // 1. Dọn dẹp giao diện và danh sách cũ
            flpDapAn.Controls.Clear();
            _optionButtons.Clear();
            _selectedButton = null; // Reset lựa chọn khi nạp câu hỏi mới

            foreach (var da in dapAns)
            {
                // 2. Khởi tạo Button cho từng đáp án
                Button btn = new Button
                {
                    Text = da.NoiDung,
                    Tag = da.LaDapAnDung, // Lưu flag đúng/sai vào Tag
                    Size = new Size(600, 65),
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(25, 45, 50),
                    Font = new Font("Segoe UI", 12F),
                    Cursor = Cursors.Hand,
                    Margin = new Padding(0, 10, 0, 10)
                };

                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.FromArgb(60, 80, 85);

                // 3. Gán sự kiện Click
                btn.Click += OptionButton_Click;

                // 4. Thêm vào danh sách quản lý và Panel hiển thị
                _optionButtons.Add(btn);
                flpDapAn.Controls.Add(btn);
            }
        }

        private void OptionButton_Click(object? sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender!;

            // Lưu vết nút được chọn vào biến
            _selectedButton = clickedBtn;

            foreach (var btn in _optionButtons)
            {
                btn.BackColor = Color.FromArgb(25, 45, 50);
                btn.FlatAppearance.BorderColor = Color.FromArgb(60, 80, 85);
            }

            clickedBtn.BackColor = Color.FromArgb(40, 70, 80);
            clickedBtn.FlatAppearance.BorderColor = Color.FromArgb(193, 225, 127);

            IsCorrect = (clickedBtn.Tag is bool b && b);
        }

        public void ShowResult()
        {
            foreach (var btn in _optionButtons)
            {
                bool isCorrectOption = btn.Tag is bool b && b;

                if (isCorrectOption)
                {
                    btn.BackColor = Color.FromArgb(46, 125, 50);
                    btn.FlatAppearance.BorderColor = Color.Lime;
                }
                // Nếu nút này là nút user đã chọn (_selectedButton) nhưng nó sai
                else if (btn == _selectedButton)
                {
                    btn.BackColor = Color.FromArgb(183, 28, 28);
                    btn.FlatAppearance.BorderColor = Color.Red;
                }

                btn.Enabled = false;
            }
        }

        public string GetUserAnswer()
        {
            // Trả về Text của biến đã lưu, đảm bảo dữ liệu luôn chính xác
            return _selectedButton != null ? _selectedButton.Text : "Không trả lời";
        }
    }
}
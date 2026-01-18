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

        // ROOT FIX: Kiểm tra xem có nút nào đang ở trạng thái "đã chọn" (màu highlight) không
        public bool HasAnswered => _optionButtons.Any(btn => btn.BackColor == Color.FromArgb(40, 70, 80));

        private List<Button> _optionButtons = new List<Button>();

        public TracNghiemHocControl(TheFlashcardResponse info, List<DapAnTracNghiemResponse> dapAns)
        {
            InitializeComponent();
            lblMatTruoc.Text = info.MatTruoc;
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
                btn.Tag = da.LaDapAnDung;

                btn.Size = new Size(600, 65);
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

            foreach (var btn in _optionButtons)
            {
                btn.BackColor = Color.FromArgb(25, 45, 50);
                btn.FlatAppearance.BorderColor = Color.FromArgb(60, 80, 85);
            }

            // Đánh dấu nút được chọn
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
                else if (btn.BackColor == Color.FromArgb(40, 70, 80))
                {
                    btn.BackColor = Color.FromArgb(183, 28, 28);
                    btn.FlatAppearance.BorderColor = Color.Red;
                }

                btn.Enabled = false;
            }
        }
    }
}
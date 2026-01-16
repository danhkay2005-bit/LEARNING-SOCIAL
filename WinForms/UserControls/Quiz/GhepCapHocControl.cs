using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class GhepCapHocControl : UserControl, IQuestionControl
    {
        public bool IsCorrect { get; private set; } = false;

        private Button? _selectedLeft = null;
        private Button? _selectedRight = null;
        private int _totalMatched = 0;
        private int _totalPairs = 0;

        // Các màu sắc đồng bộ với StudyApp
        private readonly Color NormalColor = Color.FromArgb(40, 70, 80);
        private readonly Color SelectedColor = Color.FromArgb(193, 225, 127); // Lime
        private readonly Color MatchedColor = Color.FromArgb(46, 125, 50); // Green
        private readonly Color WrongColor = Color.FromArgb(183, 28, 28); // Red

        public GhepCapHocControl(TheFlashcardResponse info, List<CapGhepResponse> pairs)
        {
            InitializeComponent();
            _totalPairs = pairs.Count;
            RenderPairs(pairs);
        }

        private void RenderPairs(List<CapGhepResponse> pairs)
        {
            flpLeft.Controls.Clear();
            flpRight.Controls.Clear();

            // Trộn vế trái và vế phải độc lập
            var leftItems = pairs.OrderBy(x => Guid.NewGuid()).ToList();
            var rightItems = pairs.OrderBy(x => Guid.NewGuid()).ToList();

            foreach (var item in leftItems) flpLeft.Controls.Add(CreateMatchButton(item.VeTrai, item.MaCap, true));
            foreach (var item in rightItems) flpRight.Controls.Add(CreateMatchButton(item.VePhai, item.MaCap, false));
        }

        private Button CreateMatchButton(string text, int pairId, bool isLeft)
        {
            Button btn = new Button
            {
                Text = text,
                Tag = pairId, // Dùng ID để kiểm tra cặp đúng
                Width = 350,
                Height = 60,
                FlatStyle = FlatStyle.Flat,
                BackColor = NormalColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F),
                Margin = new Padding(5),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(60, 80, 85);

            btn.Click += (s, e) => HandleButtonClick(btn, isLeft);
            return btn;
        }

        private async void HandleButtonClick(Button clickedBtn, bool isLeft)
        {
            if (isLeft)
            {
                if (_selectedLeft != null) _selectedLeft.BackColor = NormalColor;
                _selectedLeft = clickedBtn;
                _selectedLeft.BackColor = SelectedColor;
                _selectedLeft.ForeColor = Color.Black;
            }
            else
            {
                if (_selectedRight != null) _selectedRight.BackColor = NormalColor;
                _selectedRight = clickedBtn;
                _selectedRight.BackColor = SelectedColor;
                _selectedRight.ForeColor = Color.Black;
            }

            // Nếu đã chọn đủ cả 2 vế -> Kiểm tra
            if (_selectedLeft != null && _selectedRight != null)
            {
                var leftTag = _selectedLeft.Tag?.ToString();
                var rightTag = _selectedRight.Tag?.ToString();
                if (leftTag != null && rightTag != null && leftTag == rightTag)
                {
                    // ĐÚNG: Đổi màu và khóa nút
                    _selectedLeft.BackColor = _selectedRight.BackColor = MatchedColor;
                    _selectedLeft.ForeColor = _selectedRight.ForeColor = Color.White;
                    _selectedLeft.Enabled = _selectedRight.Enabled = false;
                    _totalMatched++;
                }
                else
                {
                    // SAI: Báo đỏ rồi reset
                    _selectedLeft.BackColor = _selectedRight.BackColor = WrongColor;
                    _selectedLeft.ForeColor = _selectedRight.ForeColor = Color.White;
                    await Task.Delay(500);
                    _selectedLeft.BackColor = _selectedRight.BackColor = NormalColor;
                }

                _selectedLeft = null;
                _selectedRight = null;
            }
        }

        public void ShowResult()
        {
            // Nếu đã ghép hết các cặp thì coi là đúng
            IsCorrect = (_totalMatched == _totalPairs);
        }
    }
}

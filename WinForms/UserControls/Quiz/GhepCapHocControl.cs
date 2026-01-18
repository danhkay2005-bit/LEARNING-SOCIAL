using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class GhepCapHocControl : UserControl, IQuestionControl
    {
        public bool IsCorrect { get; private set; } = false;

        // Fix lỗi HasAnswered: Trả về true nếu người dùng đã ghép được ít nhất 1 cặp
        public bool HasAnswered => _totalMatched > 0;

        private Button? _selectedLeft = null;
        private Button? _selectedRight = null;
        private int _totalMatched = 0;
        private int _totalPairs = 0;
        private bool _isProcessing = false; // Biến khóa để tránh nhấn nhanh gây lỗi

        private readonly Color NormalColor = Color.FromArgb(40, 70, 80);
        private readonly Color SelectedColor = Color.FromArgb(193, 225, 127);
        private readonly Color MatchedColor = Color.FromArgb(46, 125, 50);
        private readonly Color WrongColor = Color.FromArgb(183, 28, 28);

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
                Tag = pairId,
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
            btn.Click += (s, e) => HandleButtonClick(btn, isLeft);
            return btn;
        }

        private async void HandleButtonClick(Button clickedBtn, bool isLeft)
        {
            if (_isProcessing) return; // Nếu đang chờ hiệu ứng sai thì không cho nhấn

            if (isLeft)
            {
                if (_selectedLeft != null && _selectedLeft.Enabled) _selectedLeft.BackColor = NormalColor;
                _selectedLeft = clickedBtn;
                _selectedLeft.BackColor = SelectedColor;
                _selectedLeft.ForeColor = Color.Black;
            }
            else
            {
                if (_selectedRight != null && _selectedRight.Enabled) _selectedRight.BackColor = NormalColor;
                _selectedRight = clickedBtn;
                _selectedRight.BackColor = SelectedColor;
                _selectedRight.ForeColor = Color.Black;
            }

            if (_selectedLeft != null && _selectedRight != null)
            {
                // Lưu vào biến cục bộ để an toàn luồng (Local capture)
                Button left = _selectedLeft;
                Button right = _selectedRight;

                _selectedLeft = null; // Giải phóng biến toàn cục ngay
                _selectedRight = null;

                if (left.Tag?.ToString() == right.Tag?.ToString())
                {
                    // ĐÚNG
                    left.BackColor = right.BackColor = MatchedColor;
                    left.ForeColor = right.ForeColor = Color.White;
                    left.Enabled = right.Enabled = false;
                    _totalMatched++;
                }
                else
                {
                    // SAI
                    _isProcessing = true; // Bắt đầu khóa
                    left.BackColor = right.BackColor = WrongColor;
                    left.ForeColor = right.ForeColor = Color.White;

                    await Task.Delay(500); // Chờ hiệu ứng đỏ

                    // Kiểm tra xem control có bị hủy (Dispose) trong lúc chờ không
                    if (!left.IsDisposed && !right.IsDisposed)
                    {
                        left.BackColor = right.BackColor = NormalColor;
                        left.ForeColor = right.ForeColor = Color.White;
                    }
                    _isProcessing = false; // Mở khóa
                }
            }
        }

        public void ShowResult()
        {
            IsCorrect = (_totalMatched == _totalPairs);
        }
    }
}
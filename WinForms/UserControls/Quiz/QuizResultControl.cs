using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class QuizResultControl : UserControl
    {
        // Sự kiện để báo cho MainForm biết người dùng đã nhấn Hoàn thành
        public event Action? OnFinishClicked;

        public QuizResultControl()
        {
            InitializeComponent();

            // Đăng ký sự kiện Click cho nút Hoàn thành
            btnFinish.Click += (s, e) => OnFinishClicked?.Invoke();
        }

        // Chế độ Solo: Hiển thị % chính xác và thông báo SM-2
        public void DisplaySoloResult(int correct, int wrong, int total, TimeSpan time)
        {
            double accuracy = total > 0 ? (double)correct / total * 100 : 0;

            lblTitle.Text = "HOÀN THÀNH PHIÊN HỌC!";
            lblTitle.ForeColor = Color.FromArgb(193, 225, 127); // Màu xanh lá nhẹ

            lblMainStat.Text = $"{Math.Round(accuracy, 1)}%";
            lblDetails.Text = $"✅ Đúng: {correct}  |  ❌ Sai: {wrong}\n" +
                              $"⏱️ Thời gian: {time:mm\\:ss}\n\n" +
                              "Tiến độ ghi nhớ đã được cập nhật vào kho dữ liệu SM-2.";
        }

        // Chế độ Thách đấu: Hiển thị XP và trạng thái Thắng/Thua
        public void DisplayChallengeResult(int score, int correct, int wrong, bool isWinner, int pin)
        {
            double total = correct + wrong;
            double accuracy = total > 0 ? (double)correct / total : 0;

            if (isWinner)
            {
                lblTitle.ForeColor = Color.Gold;
                // Nếu thắng nhưng làm đúng dưới 60%, nhắc nhở cố gắng
                if (accuracy < 0.6)
                {
                    lblTitle.Text = "🏆 THẮNG SUÝT SAO!";
                    lblDetails.Text = "Bạn đã giành chiến thắng, nhưng tỷ lệ chính xác còn thấp.\n" +
                                      "Cả bạn và đối thủ đều cần cố gắng luyện tập thêm!";
                }
                else
                {
                    lblTitle.Text = "🏆 CHIẾN THẮNG!";
                    lblDetails.Text = "Màn trình diễn tuyệt vời! Bạn đã áp đảo đối thủ.";
                }
            }
            else
            {
                lblTitle.Text = "🏳️ CỐ GẮNG LẦN SAU";
                lblTitle.ForeColor = Color.FromArgb(255, 128, 128);
                lblDetails.Text = accuracy < 0.4 ? "Đừng nản chí! Hãy ôn tập lại kiến thức SM-2 nhé."
                                                 : "Bạn đã làm rất tốt, chỉ thiếu một chút may mắn thôi.";
            }

            lblMainStat.Text = $"{score} XP";
            lblDetails.Text += $"\n\nThống kê: {correct} Đúng - {wrong} Sai | Mã phòng: #{pin}";
        }
    }
}
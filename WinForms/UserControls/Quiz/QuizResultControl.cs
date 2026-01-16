using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class QuizResultControl : UserControl
    {
        public QuizResultControl()
        {
            InitializeComponent();
        }

        // Chế độ Solo: Hiển thị % chính xác và thông báo SM-2
        public void DisplaySoloResult(int correct, int wrong, int total, TimeSpan time)
        {
            double accuracy = total > 0 ? (double)correct / total * 100 : 0;

            lblTitle.Text = "HOÀN THÀNH PHIÊN HỌC!";
            lblTitle.ForeColor = Color.FromArgb(193, 225, 127);

            lblMainStat.Text = $"{Math.Round(accuracy, 1)}%";
            lblDetails.Text = $"✅ Đúng: {correct}  |  ❌ Sai: {wrong}\n" +
                              $"⏱️ Thời gian: {time:mm\\:ss}\n" +
                              $"Tiến độ ghi nhớ đã được cập nhật vào kho dữ liệu SM-2.";
        }

        // Chế độ Thách đấu: Hiển thị XP và trạng thái Thắng/Thua
        public void DisplayChallengeResult(int score, int correct, int wrong, bool isWinner, int pin)
        {
            lblTitle.Text = isWinner ? "🏆 CHIẾN THẮNG!" : "🏳️ CỐ GẮNG LẦN SAU";
            lblTitle.ForeColor = isWinner ? Color.Gold : Color.FromArgb(255, 128, 128);

            lblMainStat.Text = $"{score} XP";
            lblDetails.Text = $"Kết quả trận đấu: {correct} Đúng - {wrong} Sai\n" +
                              $"Mã phòng (PIN): #{pin}\n" +
                              $"Trận đấu đã được lưu vào lịch sử thách đấu vĩnh viễn.";
        }
    }
}

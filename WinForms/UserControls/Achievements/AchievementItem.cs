using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.User;

namespace WinForms.UserControls.Achievements
{
    public partial class AchievementItem : UserControl
    {
        

        public AchievementItem()
        {
            InitializeComponent();
            this.MouseEnter += (s, e) => { if (_isUnlocked) this.BackColor = Color.AliceBlue; };
            this.MouseLeave += (s, e) => { if (_isUnlocked) this.BackColor = Color.White; };
        }
        private bool _isUnlocked = false;


        public void SetData(ThanhTuuResponse data)
        {
            _isUnlocked = data.DaDatDuoc;
            // XỬ LÝ BÍ ẨN (Ẩn hết nếu chưa đạt)
            if (data.BiAn && !data.DaDatDuoc)
            {
                lblIcon.Text = "🔒";
                lblTenThanhTuu.Text = "???";
                lblMoTa.Text = "Thành tựu bí ẩn...";
                flpRewards.Controls.Clear();
                lblNgayDat.Visible = false;
                pnlRarity.BackColor = Color.Gray;
                this.BackColor = Color.WhiteSmoke;
                return;
            }

            //Hiển thị text
            lblIcon.Text = string.IsNullOrEmpty(data.BieuTuong) ? "🏆" : data.BieuTuong;
            lblTenThanhTuu.Text = data.TenThanhTuu;
            lblMoTa.Text = data.MoTa;

            // Tạo badge phần thưởng
            flpRewards.Controls.Clear();

            // Badge XP
            if (data.ThuongXp > 0)
                if (data.ThuongXp > 0)
                    flpRewards.Controls.Add(CreateBadge($"⚡ {data.ThuongXp} XP", Color.FromArgb(230, 255, 230), Color.Green));

            // Badge Vàng (Màu vàng nhạt)
            if (data.ThuongVang > 0)
                flpRewards.Controls.Add(CreateBadge($"🟡 {data.ThuongVang}", Color.FromArgb(255, 250, 220), Color.DarkGoldenrod));

            // Badge Kim Cương (Màu xanh dương nhạt)
            if (data.ThuongKimCuong > 0)
                flpRewards.Controls.Add(CreateBadge($"💎 {data.ThuongKimCuong}", Color.FromArgb(230, 245, 255), Color.DodgerBlue));

            // Độ hiểm
            switch (data.DoHiem)
            {
                case DoHiemEnum.HuyenThoai:
                    pnlRarity.BackColor = Color.Gold;
                    break;
                case DoHiemEnum.SuThi:
                    pnlRarity.BackColor = Color.Purple;
                    break;
                case DoHiemEnum.Hiem:
                    pnlRarity.BackColor = Color.DeepSkyBlue;
                    break;
                default:
                    pnlRarity.BackColor = Color.LightGray;
                    break;
            }

            //Trạng thái
            if (data.DaDatDuoc)
            {
                this.BackColor = Color.White;
                lblIcon.ForeColor = Color.Black;
                lblTenThanhTuu.ForeColor = Color.Black;
                lblNgayDat.Visible = true;
                lblNgayDat.Text = $"Đạt được: {data.NgayDat:dd/MM/yyyy}";
            }
            else
            {
                this.BackColor = Color.WhiteSmoke; // Xám mờ
                lblIcon.ForeColor = Color.Gray;
                lblTenThanhTuu.ForeColor = Color.Gray;
                lblNgayDat.Visible = false;
            }
        }

            // Hàm vẽ Badge nhỏ xinh
            private Label CreateBadge(string text, Color bg, Color fore)
            {
                Label lbl = new Label();
                lbl.Text = text;
                lbl.BackColor = bg;
                lbl.ForeColor = fore;
                lbl.Font = new Font("Segoe UI", 8, FontStyle.Bold); 
                lbl.AutoSize = true;
                lbl.Padding = new Padding(5, 2, 5, 2); 
                lbl.Margin = new Padding(0, 0, 5, 0);  
                return lbl;
            }
    }
}

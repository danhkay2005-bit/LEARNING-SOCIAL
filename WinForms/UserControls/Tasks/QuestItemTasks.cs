using StudyApp.DTO.Responses.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms.UserControls.Tasks
{
    public partial class QuestItemTasks : UserControl
    {
        // Change the event declaration to nullable to resolve CS8618
        public event EventHandler<int>? OnClaimClicked;
        private int _maNhiemVu;
        public QuestItemTasks()
        {
            InitializeComponent();
        }

        public void SetData(TienDoNhiemVuResponse data)
        {
            _maNhiemVu = data.MaNhiemVu;
            lblIcon.Text = string.IsNullOrEmpty(data.BieuTuong) ? "📜" : data.BieuTuong;
            lblTen.Text = data.TenNhiemVu;

            string rewards = $"XP: + {data.ThuongXP}";
            if (data.ThuongVang > 0) rewards += $" | 🟡 +{data.ThuongVang}";
            if (data.ThuongKimCuong > 0) rewards += $" |💎 +{data.ThuongKimCuong}";

            lblMoTa.Text = $"{data.MoTa}\nQuà: {rewards}";
            if (data.DieuKienDatDuoc > 0)
            {
                pbTienDo.Maximum = 100;
                pbTienDo.Value = data.PhanTramHoanThanh;
                lblTienDo.Text = $"{data.TienDoHienTai}/{data.DieuKienDatDuoc}";
            }   
            else
            {
                pbTienDo.Value = data.DaHoanThanh ? 100 : 0;
                lblTienDo.Text = data.DaHoanThanh ? "Hoàn thành" : "0/1";
            }
            if (data.DaHoanThanh)
            {
                btnAction.Text = "NHẬN THƯỞNG";
                btnAction.Enabled = true;
                btnAction.BackColor = Color.Gold;
                btnAction.Cursor = Cursors.Hand;
            }    
            else
            {
                btnAction.Text = "Đang làm";
                btnAction.Enabled = true;
                btnAction.BackColor = Color.LightGray;
            }    
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            OnClaimClicked?.Invoke(this, _maNhiemVu);
        }
    }
}

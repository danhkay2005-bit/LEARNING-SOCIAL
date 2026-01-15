using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.User;
using StudyApp.BLL.Services.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.User;
using StudyApp.DTO;
using WinForms.UserControls.Achievements;
using WinForms.Forms;


namespace WinForms.UserControls.Pages
{
    public partial class AchievementsPage : UserControl
    {
        private readonly IGamificationService _service;

        public AchievementsPage(IGamificationService service)
        {
            InitializeComponent();
            _service = service;
        }

        private async void AchievementsPage_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            await LoadData();
            this.Cursor = Cursors.Default;
        }

        public async Task LoadData()
        {
            if (!UserSession.IsLoggedIn || UserSession.CurrentUser == null)
            {
                MessageBox.Show("Vui lòng đăng nhập để xem thành tựu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var userId = UserSession.CurrentUser.MaNguoiDung;

                // Lấy danh sách thành tựu theo đúng userId (Guid)
                var allAchievements = await _service.GetAchievementsAsync(userId);

                ClearAllPanels();

                RenderListToPanel(flpHocTap, allAchievements.Where(x => x.LoaiThanhTuu == LoaiThanhTuuEnum.HocTap));
                RenderListToPanel(flpChuoiNgay, allAchievements.Where(x => x.LoaiThanhTuu == LoaiThanhTuuEnum.ChuoiNgay));
                RenderListToPanel(flpXaHoi, allAchievements.Where(x => x.LoaiThanhTuu == LoaiThanhTuuEnum.XaHoi));
                RenderListToPanel(flpThachDau, allAchievements.Where(x => x.LoaiThanhTuu == LoaiThanhTuuEnum.ThachDau));
                RenderListToPanel(flpSangTao, allAchievements.Where(x => x.LoaiThanhTuu == LoaiThanhTuuEnum.SangTao));
                RenderListToPanel(flpKhamPha, allAchievements.Where(x => x.LoaiThanhTuu == LoaiThanhTuuEnum.KhamPha));
                RenderListToPanel(flpAnDanh, allAchievements.Where(x => x.LoaiThanhTuu == LoaiThanhTuuEnum.AnDanh));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải thành tựu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderListToPanel(FlowLayoutPanel panel, IEnumerable<ThanhTuuResponse> items)
        {
            panel.SuspendLayout();

            if (!items.Any())
            {
                Label lblEmpty = new Label
                {
                    Text = "Chưa có thành tựu nào trong nhóm này.",
                    AutoSize = true,
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 10, FontStyle.Italic),
                    Padding = new Padding(20)
                };
                panel.Controls.Add(lblEmpty);
                panel.ResumeLayout();
                return;
            }

            foreach (var item in items)
            {
                var control = new AchievementItem();
                control.SetData(item);
                control.Width = panel.ClientSize.Width - 25;
                control.Margin = new Padding(0, 0, 0, 10);
                panel.Controls.Add(control);
            }

            panel.ResumeLayout();
        }

        private void ClearAllPanels()
        {
            flpHocTap.Controls.Clear();
            flpChuoiNgay.Controls.Clear();
            flpXaHoi.Controls.Clear();
            flpThachDau.Controls.Clear();
            flpSangTao.Controls.Clear();
            flpKhamPha.Controls.Clear();
            flpAnDanh.Controls.Clear();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }
    }
}

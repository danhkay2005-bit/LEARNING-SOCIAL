using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Services.User;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Forms;
using WinForms.UserControls.Tasks;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace WinForms.UserControls.Pages
{
    public partial class TaskPage : UserControl
    {
        private readonly IGamificationService _service;

        // Constructor nhận Interface vì AddBusinessServices() chỉ đăng ký .AsImplementedInterfaces()
        public TaskPage(IGamificationService service)
        {
            InitializeComponent();
            _service = service;
        }

        private async void TaskPage_Load(object sender, EventArgs e)
        {
            var userId = UserSession.CurrentUser!.MaNguoiDung;
            await _service.ResetDailyQuestCountAsync(userId);
            // Thêm 2 dòng trên

            await LoadAllQuests();
        }
        #region LoadAllQuests
        private async Task LoadAllQuests()
        {
            try
            {
                if (!UserSession.IsLoggedIn)
                {
                    MessageBox.Show("Vui lòng đăng nhập để xem nhiệm vụ.");
                    return;
                }

                var userId = UserSession.CurrentUser!.MaNguoiDung;
                var allQuests = await _service.GetMyQuestsAsync(userId);

                var dailyList = allQuests.Where(q => q.LoaiNhiemVu == LoaiNhiemVuEnum.HangNgay).ToList();
                RenderDataToPanel(flpDaily, dailyList);
               
                var weeklyList = allQuests.Where(q => q.LoaiNhiemVu == LoaiNhiemVuEnum.HangTuan).ToList();
                RenderDataToPanel(flpWeekly, weeklyList);

                var achieveList = allQuests.Where(q => q.LoaiNhiemVu == LoaiNhiemVuEnum.ThanhTuu).ToList();
                RenderDataToPanel(flpAchievement, achieveList);

                var eventList = allQuests.Where(q => q.LoaiNhiemVu == LoaiNhiemVuEnum.SuKien).ToList();
                RenderDataToPanel(flpEvent, eventList);

              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }
        #endregion

        #region RenderDataToPanel
        private void RenderDataToPanel(FlowLayoutPanel panel, List<TienDoNhiemVuResponse> list)
        {
            panel.Controls.Clear();

            if (list.Count == 0)
            {
                Label lbl = new Label
                {
                    Text = "Chưa có nhiệm vụ nào.",
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray,
                    Margin = new Padding(20),
                    Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Italic)
                };

                panel.Controls.Add(lbl);
                return;
            }

            foreach (var data in list)
            {
                var item = new QuestItemTasks();
                item.SetData(data);

                item.Width = panel.ClientSize.Width - 25;
                item.Margin = new Padding(0, 0, 0, 10);

                item.OnClaimClicked += Item_OnClaimClicked;

                panel.Controls.Add(item);
            }
        }
        #endregion

        #region Item_OnClaimClicked
      /*  private async void Item_OnClaimClicked(object? sender, int maNhiemVu)
        {
            try
            {
                if (!UserSession.IsLoggedIn)
                {
                    MessageBox.Show("Vui lòng đăng nhập để nhận thưởng.");
                    return;
                }

                var userId = UserSession.CurrentUser!.MaNguoiDung;
                var result = await _service.ClaimQuestRewardAsync(userId, maNhiemVu);

                if (result == "Thành công!")
                {
                    RewardPopupTask popup = new RewardPopupTask("🎉 Nhận thưởng thành công!\nKiểm tra ví của bạn nhé.");
                    popup.ShowDialog();
                    await LoadAllQuests();
                    
                }
                else
                {
                    MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }*/
        #endregion


        #region Item_OnClaimClicked2
        private async void Item_OnClaimClicked(object? sender, int maNhiemVu)
        {
            try
            {
                if (!UserSession.IsLoggedIn) return;

                var userId = UserSession.CurrentUser!.MaNguoiDung;
                var resultMsg = await _service.ClaimQuestRewardAsync(userId, maNhiemVu);

                if (resultMsg.StartsWith("Lỗi") || resultMsg.Contains("Chưa hoàn thành") || resultMsg.Contains("Đã nhận"))
                {
                    MessageBox.Show(resultMsg, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // === THÀNH CÔNG ===
                    var popup = new RewardPopupTask(resultMsg);
                    popup.ShowDialog();
                    await RefreshDashboardManual(maNhiemVu);

                    // 🔥 TRIGGER EVENT ĐỂ REFRESH TRANG CHỦ
                    AppEvents.OnUserStatsChanged();

                    // Refresh dashboard
                    
                    await LoadAllQuests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }
        #endregion

        #region RefreshDashboard
        private async Task RefreshDashboardManual(int maNhiemVu)
        {
            try
            {
                if (Program.ServiceProvider == null || UserSession.CurrentUser == null)
                    return;

                var db = Program.ServiceProvider.GetRequiredService<UserDbContext>();

                var user = await db.NguoiDungs.FindAsync(UserSession.CurrentUser.MaNguoiDung);
                if (user == null) return;

                var current = UserSession.CurrentUser;

                current.Vang = user.Vang ?? 0;
                current.KimCuong = user.KimCuong ?? 0;
                current.TongDiemXp = user.TongDiemXp ?? 0;
                current.ChuoiNgayHocLienTiep = user.ChuoiNgayHocLienTiep ?? 0;
                current.TongSoTheHoc = user.TongSoTheHoc ?? 0;
                current.TongSoTheDung = user.TongSoTheDung ?? 0;

                UserSession.Login(current);
            }
            catch
            {
                // bỏ qua
            }
        }
        #endregion

        public async Task RefreshQuests()
        {
            await LoadAllQuests();
        }
    }
}
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
            await LoadAllQuests();
        }

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

        private async void Item_OnClaimClicked(object? sender, int maNhiemVu)
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
        }
    }
}
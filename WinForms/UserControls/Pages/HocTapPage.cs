using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Forms;
using WinForms.UserControls.Quiz;

namespace WinForms.UserControls.Pages
{
    public partial class HocTapPage : UserControl
    {
        private readonly IBoDeHocService _boDeHocService;
        private readonly IThachDauService _thachDauService;
        private readonly HubConnection _hubConnection; // Kết nối SignalR dùng chung

        public HocTapPage(IBoDeHocService boDeHocService, IThachDauService thachDauService, HubConnection hubConnection)
        {
            InitializeComponent();
            _boDeHocService = boDeHocService;
            _thachDauService = thachDauService;
            _hubConnection = hubConnection; // Nhận kết nối Singleton từ DI

            this.Load += HocTapPage_Load;
        }

        private async void HocTapPage_Load(object? sender, EventArgs e)
        {
            await Task.WhenAll(
                LoadMyQuizzesAsync(),
                LoadPublicQuizzesAsync(),
                LoadTopicQuizzesAsync()
            );
        }

        // ======================================================
        // LOGIC THAM GIA THÁCH ĐẤU (NHẬP MÃ PIN)
        // ======================================================
        private async void btnThamGia_Click(object? sender, EventArgs e)
        {
            string pinInput = Microsoft.VisualBasic.Interaction.InputBox("Nhập mã PIN 6 số:", "THAM GIA", "");
            if (string.IsNullOrWhiteSpace(pinInput) || pinInput.Length != 6 || !int.TryParse(pinInput, out int pin)) return;
            if (UserSession.CurrentUser == null)
            {
                MessageBox.Show("Bạn cần đăng nhập để tham gia thách đấu.");
                return;
            }
            try
            {
                var joinReq = new ThamGiaThachDauRequest { MaThachDau = pin, MaNguoiDung = UserSession.CurrentUser.MaNguoiDung };
                bool isJoined = await _thachDauService.ThamGiaThachDauAsync(joinReq);

                if (isJoined)
                {
                    var room = await _thachDauService.GetByIdAsync(pin);
                    if (room == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin phòng thách đấu.");
                        return;
                    }
                    var mainForm = this.ParentForm as MainForm;
                    if (mainForm != null && Program.ServiceProvider != null)
                    {
                        // Lấy ChiTietBoDeControl từ DI
                        var chiTietPage = Program.ServiceProvider.GetRequiredService<ChiTietBoDeControl>();

                        // Gọi hàm khởi tạo với vai trò GUEST
                        await chiTietPage.JoinAsGuest(pin, room.MaBoDe);

                        mainForm.LoadPage(chiTietPage);

                        // Kích hoạt SignalR báo cho chủ phòng biết mình đã vào
                        if (_hubConnection.State == HubConnectionState.Disconnected) await _hubConnection.StartAsync();
                        await _hubConnection.InvokeAsync("TriggerStartMatch", pin.ToString());
                    }
                }
                else { MessageBox.Show("Phòng không tồn tại hoặc đã đầy!"); }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}"); }
        }

        // ======================================================
        // CÁC HÀM TẢI DỮ LIỆU UI (GIỮ NGUYÊN)
        // ======================================================

        public async Task LoadMyQuizzesAsync()
        {
            try
            {
                if (UserSession.CurrentUser != null)
                {
                    var ds = await _boDeHocService.GetByUserAsync(UserSession.CurrentUser.MaNguoiDung);
                    PopulateFlowPanel(flowBoDeCuaToi, ds);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public async Task LoadPublicQuizzesAsync()
        {
            try
            {
                var ds = await _boDeHocService.GetPublicRandomAsync(10);
                PopulateFlowPanel(flowBoDeCongKhai, ds);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public async Task LoadTopicQuizzesAsync()
        {
            try
            {
                var ds = await _boDeHocService.GetByTopicAsync(2);
                PopulateFlowPanel(flowChuDe, ds);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void PopulateFlowPanel(FlowLayoutPanel panel, IEnumerable<BoDeHocResponse> data)
        {
            if (panel.InvokeRequired)
            {
                panel.Invoke(new Action(() => PopulateFlowPanel(panel, data)));
                return;
            }

            panel.Controls.Clear();
            if (data == null || !data.Any()) return;

            foreach (var boDe in data)
            {
                var item = new BoDeItemControl();
                item.SetData(boDe.MaBoDe, boDe.TieuDe, boDe.SoLuongThe, 0);
                item.OnVaoThiClick += (s, ev) => StartQuiz(boDe.MaBoDe);
                panel.Controls.Add(item);
            }
        }

        private void StartQuiz(int maBoDe)
        {
            var mainForm = this.ParentForm as MainForm;
            if (mainForm != null && Program.ServiceProvider != null)
            {
                var chiTietPage = Program.ServiceProvider.GetRequiredService<ChiTietBoDeControl>();
                chiTietPage.MaBoDe = maBoDe;
                mainForm.LoadPage(chiTietPage);
            }
        }

        private void btnTaoQuiz_Click(object sender, EventArgs e)
        {
            var mainForm = this.ParentForm as MainForm;
            if (mainForm != null && Program.ServiceProvider != null)
            {
                var taoQuizPage = Program.ServiceProvider.GetRequiredService<TaoQuizPage>();
                mainForm.LoadPage(taoQuizPage);
            }
        }
    }
}
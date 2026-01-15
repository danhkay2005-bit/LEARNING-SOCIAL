using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Forms;

namespace WinForms.UserControls.Quiz
{
    public enum LobbyRole { None, Host, Guest }

    public partial class ChiTietBoDeControl : UserControl
    {
        private readonly IBoDeHocService _boDeHocService;
        private readonly IThachDauService _thachDauService;
        private readonly HubConnection _hubConnection;

        private BoDeHocResponse? _currentBoDe;
        private int _currentPin = 0;
        private LobbyRole _currentRole = LobbyRole.None;

        public ChiTietBoDeControl(IBoDeHocService boDeHocService, IThachDauService thachDauService, HubConnection hubConnection)
        {
            InitializeComponent();
            _boDeHocService = boDeHocService;
            _thachDauService = thachDauService;
            _hubConnection = hubConnection;

            // Đăng ký sự kiện mặc định
            btnStartSolo.Click += btnStartSolo_Click;
            btnCreateChallenge.Click += btnCreateChallenge_Click;

            RegisterSignalREvents();
        }

        public int MaBoDe { set => LoadDataById(value); }

        private void RegisterSignalREvents()
        {
            // 1. Khi đối thủ vào phòng (Dành cho Chủ phòng)
            _hubConnection.On("ReadyToStart", () =>
            {
                this.Invoke(() => {
                    if (_currentRole == LobbyRole.Host)
                    {
                        lblStatus.Text = "ĐỐI THỦ ĐÃ VÀO PHÒNG!";
                        lblStatus.ForeColor = Color.LimeGreen;
                        btnStartSolo.Enabled = true;
                        btnStartSolo.Text = "BẮT ĐẦU TRẬN ĐẤU";
                        btnStartSolo.BackColor = Color.LimeGreen;

                        btnStartSolo.Click -= btnStartSolo_Click;
                        btnStartSolo.Click += btnOwnerStartMatch_Click;
                    }
                });
            });

            // 2. Khi chủ phòng bấm Bắt đầu (Dành cho Khách)
            _hubConnection.On("StartMatchSignal", () =>
            {
                this.Invoke(async () => {
                    if (_currentRole == LobbyRole.Guest)
                    {
                        await ExecuteStart();
                    }
                });
            });
        }

        private async void LoadDataById(int id)
        {
            try
            {
                lblStatus.Text = "Đang tải...";
                var data = await _boDeHocService.GetByIdAsync(id);
                if (data != null) SetData(data);
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}"); }
        }

        public void SetData(BoDeHocResponse data)
        {
            _currentBoDe = data;
            lblMainTitle.Text = data.TieuDe.ToUpper();
            lblSideTitle.Text = data.TieuDe;
            lblSideInfo.Text = $"{data.SoLuongThe} thẻ • Độ khó: {data.MucDoKho}";
            if (!string.IsNullOrEmpty(data.AnhBia)) picThumb.ImageLocation = data.AnhBia;
        }

        // --- DÀNH CHO KHÁCH (GUEST) GỌI TỪ TRANG CHỦ ---
        public async Task JoinAsGuest(int pin, int maBoDe)
        {
            _currentRole = LobbyRole.Guest;
            _currentPin = pin;
            LoadDataById(maBoDe);

            btnCreateChallenge.Visible = false;
            btnStartSolo.Enabled = false;
            btnStartSolo.Text = "ĐANG ĐỢI CHỦ PHÒNG...";
            lblChallengeCode.Text = pin.ToString("D6");
            lblStatus.Text = "Đã tham gia thách đấu";

            if (_hubConnection.State == HubConnectionState.Disconnected)
                await _hubConnection.StartAsync();

            await _hubConnection.InvokeAsync("JoinRoom", pin.ToString());
        }

        // --- DÀNH CHO CHỦ PHÒNG (HOST) ---
        private async void btnCreateChallenge_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_currentBoDe == null) return;
                if (UserSession.CurrentUser == null)
                {
                    MessageBox.Show("Bạn cần đăng nhập để tạo thách đấu.");
                    return;
                }
                _currentRole = LobbyRole.Host;

                var req = new TaoThachDauRequest { MaBoDe = _currentBoDe.MaBoDe, NguoiTao = UserSession.CurrentUser.MaNguoiDung };
                var challenge = await _thachDauService.TaoThachDauAsync(req);
                _currentPin = challenge.MaThachDau;

                lblChallengeCode.Text = _currentPin.ToString("D6");
                btnCreateChallenge.Enabled = false;
                btnStartSolo.Enabled = false;
                btnStartSolo.Text = "ĐỢI ĐỐI THỦ...";

                if (_hubConnection.State == HubConnectionState.Disconnected)
                    await _hubConnection.StartAsync();

                await _hubConnection.InvokeAsync("JoinRoom", _currentPin.ToString());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async void btnOwnerStartMatch_Click(object? sender, EventArgs e)
        {
            // Gửi lệnh cho khách rồi tự chuyển trang
            await _hubConnection.InvokeAsync("SendStartSignal", _currentPin.ToString());
            await ExecuteStart();
        }

        private async void btnStartSolo_Click(object? sender, EventArgs e)
        {
            if (_currentBoDe == null) return;
            var data = await _boDeHocService.GetFullDataToLearnAsync(_currentBoDe.MaBoDe);
            NavigateToQuiz(data, CheDoHocEnum.HocMotMinh);
        }

        private async Task ExecuteStart()
        {
            var data = await _boDeHocService.GetFullDataToLearnAsync(_currentBoDe!.MaBoDe);
            NavigateToQuiz(data, CheDoHocEnum.ThachDau, _currentPin);
        }

        private void NavigateToQuiz(HocBoDeResponse data, CheDoHocEnum mode, int? pin = null)
        {
            var mainForm = this.ParentForm as MainForm;
            if (Program.ServiceProvider == null)
            {
                MessageBox.Show("ServiceProvider is not initialized.");
                return;
            }
            var hocPage = (HocBoDePage)Program.ServiceProvider.GetRequiredService(typeof(HocBoDePage));
            hocPage.Initialize(data, mode, pin);
            mainForm?.LoadPage(hocPage);
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Xóa các sự kiện để tránh rò rỉ bộ nhớ và chạy sai logic khi quay lại trang
            _hubConnection.Remove("ReadyToStart");
            _hubConnection.Remove("StartMatchSignal");

            base.OnHandleDestroyed(e);
        }
    }
}
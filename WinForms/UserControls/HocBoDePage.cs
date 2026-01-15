using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Forms;
using WinForms.UserControls.Pages;
using WinForms.UserControls.Quiz;

namespace WinForms.UserControls
{
    public partial class HocBoDePage : UserControl
    {
        private readonly IBoDeHocService _boDeHocService;
        private readonly IThachDauService _thachDauService;
        private readonly HubConnection _hubConnection; // Dùng chung Singleton từ DI

        private HocBoDeResponse? _data;
        private CheDoHocEnum _cheDo;
        private int? _maThachDau;

        private int _currentIndex = 0;
        private int _totalScore = 0;
        private int _correctCount = 0;
        private int _wrongCount = 0;
        private int _currentTimeLeft = 100;
        private Stopwatch _totalTimer = new Stopwatch();

        public HocBoDePage(IBoDeHocService boDeHocService, IThachDauService thachDauService, HubConnection hubConnection)
        {
            InitializeComponent();
            _boDeHocService = boDeHocService;
            _thachDauService = thachDauService;
            _hubConnection = hubConnection;

            timerTick.Tick += TimerTick_Tick;
        }

        public void Initialize(HocBoDeResponse data, CheDoHocEnum cheDo, int? maThachDau = null)
        {
            _cheDo = cheDo;
            _maThachDau = maThachDau;

            // 1. Lọc SM-2
            if (_cheDo == CheDoHocEnum.HocMotMinh)
            {
                data.DanhSachCauHoi = data.DanhSachCauHoi
                    .Where(q => q.ThongTinThe.NgayOnTapTiepTheo == null || q.ThongTinThe.NgayOnTapTiepTheo <= DateTime.Now)
                    .ToList();
            }

            if (data.DanhSachCauHoi == null || data.DanhSachCauHoi.Count == 0)
            {
                MessageBox.Show("Không có câu hỏi khả dụng!", "Thông báo");
                ReturnToLobby();
                return;
            }

            _data = data;
            _currentIndex = 0;
            _totalScore = 0;
            _correctCount = 0;
            _wrongCount = 0;
            _totalTimer.Restart();

            // 2. Lắng nghe điểm (Xóa cũ trước khi đăng ký mới)
            if (_cheDo == CheDoHocEnum.ThachDau)
            {
                RegisterScoreEvents();
            }

            ShowQuestion(_currentIndex);
        }

        private void RegisterScoreEvents()
        {
            _hubConnection.Remove("UpdateOpponentScore");
            _hubConnection.On<Guid, int>("UpdateOpponentScore", (userId, score) =>
            {
                if (UserSession.CurrentUser != null && userId != UserSession.CurrentUser.MaNguoiDung)
                {
                    this.Invoke(() => {
                        lblOpponentScore.Text = $"Đối thủ: {score} XP";
                        if (prgOpponent != null) prgOpponent.Value = Math.Min(score, prgOpponent.Maximum);
                    });
                }
            });
        }

        private void ShowQuestion(int index)
        {
            if (_data == null || index >= _data.DanhSachCauHoi.Count) return;

            timerTick.Stop();
            _currentTimeLeft = 100;
            prgTimeCountdown.Value = 100;

            prgStatus.Value = (int)((float)(index + 1) / _data.DanhSachCauHoi.Count * 100);
            lblProgress.Text = $"CÂU HỎI: {index + 1} / {_data.DanhSachCauHoi.Count}";

            pnlQuestionContent.Controls.Clear();
            var cauHoi = _data.DanhSachCauHoi[index];

            Control? questionUI = CreateQuestionControl(cauHoi);
            if (questionUI != null)
            {
                questionUI.Dock = DockStyle.Fill;
                pnlQuestionContent.Controls.Add(questionUI);
            }

            timerTick.Start();
        }

        private Control CreateQuestionControl(ChiTietCauHoiHocResponse cauHoi)
        {
            // Logic khởi tạo UserControl con tương ứng loại thẻ
            if (cauHoi.ThongTinThe.LoaiThe == LoaiTheEnum.TracNghiem)
                return new TracNghiemHocControl(cauHoi.ThongTinThe, cauHoi.TracNghiem ?? new List<DapAnTracNghiemResponse>());

            return new Label { Text = cauHoi.ThongTinThe.MatTruoc, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            timerTick.Stop();
            bool isCorrect = false;

            if (pnlQuestionContent.Controls.Count > 0 && pnlQuestionContent.Controls[0] is IQuestionControl qc)
            {
                isCorrect = qc.IsCorrect;
                qc.ShowResult();
                await Task.Delay(600);
            }

            if (isCorrect) _correctCount++; else _wrongCount++;

            // Tính chất lượng dựa trên độ chính xác và thời gian
            // Sử dụng LaTeX cho công thức tính chất lượng $q \in [1, 5]$
            int quality = CalculateQuality(isCorrect, _currentTimeLeft);
            _totalScore += (quality * 10);

            await ProcessNextStep(isCorrect, quality);
        }

        private int CalculateQuality(bool isCorrect, int timeLeft)
        {
            if (!isCorrect) return 1;
            if (timeLeft > 70) return 5;
            if (timeLeft > 30) return 4;
            return 3;
        }

        private async Task ProcessNextStep(bool isCorrect, int quality)
        {
            var currentUser = UserSession.CurrentUser;
            if (currentUser == null) return;

            // 1. Cập nhật SM-2 cá nhân
            if (_cheDo == CheDoHocEnum.HocMotMinh && _data != null)
            {
                var req = new CapNhatTienDoHocTapRequest
                {
                    MaThe = _data.DanhSachCauHoi[_currentIndex].ThongTinThe.MaThe,
                    MaNguoiDung = currentUser.MaNguoiDung,
                    TrangThai = (TrangThaiHocEnum)quality
                };
                await _boDeHocService.UpdateCardProgressAsync(req);
            }

            // 2. Gửi điểm Thách đấu (Real-time)
            if (_cheDo == CheDoHocEnum.ThachDau && _maThachDau.HasValue)
            {
                // Cập nhật Database trước
                var reqCapNhat = new CapNhatKetQuaThachDauRequest
                {
                    MaThachDau = _maThachDau.Value,
                    MaNguoiDung = currentUser.MaNguoiDung,
                    Diem = _totalScore,
                    SoTheDung = _correctCount,
                    SoTheSai = _wrongCount,
                    ThoiGianLamBaiGiay = (int)_totalTimer.Elapsed.TotalSeconds
                };
                await _thachDauService.CapNhatKetQuaNguoiChoiAsync(reqCapNhat);

                // Gửi qua Hub cho đối thủ
                if (_hubConnection.State == HubConnectionState.Connected)
                    await _hubConnection.InvokeAsync("SendScore", _maThachDau.Value.ToString(), currentUser.MaNguoiDung, _totalScore);
            }

            // Chuyển câu
            if (_currentIndex < (_data?.DanhSachCauHoi.Count ?? 0) - 1)
            {
                _currentIndex++;
                ShowQuestion(_currentIndex);
            }
            else
            {
                _totalTimer.Stop();
                await FinishQuiz();
            }
        }

        private async Task FinishQuiz()
        {
            if (_cheDo == CheDoHocEnum.ThachDau && _maThachDau.HasValue)
                await _thachDauService.HoanThanhVaCleanupAsync(_maThachDau.Value);

            MessageBox.Show($"Hoàn thành! Tổng điểm: {_totalScore}", "KẾT QUẢ");
            ReturnToLobby();
        }

        private async void TimerTick_Tick(object? sender, EventArgs e)
        {
            if (_currentTimeLeft > 0)
            {
                _currentTimeLeft--;
                prgTimeCountdown.Value = _currentTimeLeft;
            }
            else
            {
                timerTick.Stop();
                await ProcessNextStep(false, 0);
            }
        }

        private void ReturnToLobby()
        {
            var mainForm = this.ParentForm as MainForm;
            if (mainForm != null && Program.ServiceProvider != null)
            {
                var homePage = Program.ServiceProvider.GetRequiredService<HocTapPage>();
                mainForm.LoadPage(homePage);
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Dọn dẹp sự kiện SignalR khi đóng trang
            _hubConnection.Remove("UpdateOpponentScore");
            base.OnHandleDestroyed(e);
        }
    }
}
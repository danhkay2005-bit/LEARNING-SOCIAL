using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO;
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
        private readonly IChuDeService _chuDeService;
        private readonly HubConnection _hubConnection;

        private const int PAGE_SIZE = 4; // Số lượng hiển thị mỗi lần
        private bool _isDataLoading = false;

        // --- QUẢN LÝ DỮ LIỆU PHÂN TRANG: BỘ ĐỀ CỦA TÔI ---
        private List<BoDeHocResponse> _allMyQuizzes = new List<BoDeHocResponse>();
        private int _myCurrentPage = 0;

        // --- QUẢN LÝ DỮ LIỆU PHÂN TRANG: BỘ ĐỀ CÔNG KHAI ---
        private List<BoDeHocResponse> _allPublicQuizzes = new List<BoDeHocResponse>();
        private int _publicCurrentPage = 0;

        public HocTapPage(
            IBoDeHocService boDeHocService,
            IThachDauService thachDauService,
            IChuDeService chuDeService,
            HubConnection hubConnection)
        {
            InitializeComponent();
            _boDeHocService = boDeHocService;
            _thachDauService = thachDauService;
            _chuDeService = chuDeService;
            _hubConnection = hubConnection;
            this.pnlMainContent.Resize += PnlMainContent_Resize;

            this.Load += HocTapPage_Load;
            cbbLocChuDe.SelectedIndexChanged += cbbLocChuDe_SelectedIndexChanged;

            // Đăng ký sự kiện điều hướng cho "Bộ đề của tôi"
            btnNextMy.Click += (s, e) => ChangeMyPage(1);
            btnPrevMy.Click += (s, e) => ChangeMyPage(-1);

            // Đăng ký sự kiện điều hướng cho "Bộ đề công khai"
            btnNextPublic.Click += (s, e) => ChangePublicPage(1);
            btnPrevPublic.Click += (s, e) => ChangePublicPage(-1);

            SetDoubleBuffered(pnlMainContent);
            SetDoubleBuffered(flowBoDeCuaToi);
            SetDoubleBuffered(flowBoDeCongKhai);
    
        }

        private void PnlMainContent_Resize(object? sender, EventArgs e)
        {
            // Tính toán chiều rộng khả dụng (trừ đi Padding và khoảng cách Scrollbar)
            int targetWidth = pnlMainContent.ClientSize.Width - pnlMainContent.Padding.Left - pnlMainContent.Padding.Right - 20;

            // Ép các thành phần chính giãn theo chiều rộng này
            pnlHeader.Width = targetWidth;
            lblMyQuizzes.Width = targetWidth;
            flowBoDeCuaToi.Width = targetWidth;
            pnlFilterContainer.Width = targetWidth;
            lblPublicQuizzes.Width = targetWidth;
            flowBoDeCongKhai.Width = targetWidth;
        }

        private async void HocTapPage_Load(object? sender, EventArgs e)
        {
            try
            {
                _isDataLoading = true;
                await LoadFilterCategoriesAsync();

                // Tải dữ liệu cá nhân trước
                await LoadMyQuizzesAsync();

                _isDataLoading = false;
                // Sau đó mới tải dữ liệu công khai dựa trên ComboBox
                if (cbbLocChuDe.SelectedValue is int maChuDe)
                {
                    await LoadPublicQuizzesAsync(maChuDe);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Load Error] {ex.Message}");
            }
            finally { _isDataLoading = false; }
        }

        private async Task LoadFilterCategoriesAsync()
        {
            var dsChuDe = await _chuDeService.GetAllAsync();
            var filterList = dsChuDe.ToList();
            cbbLocChuDe.DataSource = filterList;
            cbbLocChuDe.DisplayMember = "TenChuDe";
            cbbLocChuDe.ValueMember = "MaChuDe";
        }

        private async void cbbLocChuDe_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isDataLoading || cbbLocChuDe.SelectedValue == null) return;
            if (cbbLocChuDe.SelectedValue is int maChuDe)
            {
                await LoadPublicQuizzesAsync(maChuDe);
            }
        }

        // ======================================================
        // LOGIC BỘ ĐỀ CỦA TÔI
        // ======================================================

        public async Task LoadMyQuizzesAsync()
        {
            if (UserSession.CurrentUser == null) return;
            try
            {
                var ds = await _boDeHocService.GetByUserAsync(UserSession.CurrentUser.MaNguoiDung);
                _allMyQuizzes = ds?.ToList() ?? new List<BoDeHocResponse>();
                _myCurrentPage = 0;
                DisplayMyQuizzesByPage();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[MyQuizzes Error] {ex.Message}");
            }
        }

        private void DisplayMyQuizzesByPage()
        {
            var pagedData = _allMyQuizzes
                .Skip(_myCurrentPage * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToList();

            PopulateFlowPanel(flowBoDeCuaToi, pagedData);
            UpdateMyNavigationButtons();
        }

        private void ChangeMyPage(int direction)
        {
            int newPage = _myCurrentPage + direction;
            if (newPage >= 0 && newPage * PAGE_SIZE < _allMyQuizzes.Count)
            {
                _myCurrentPage = newPage;
                DisplayMyQuizzesByPage();
            }
        }

        private void UpdateMyNavigationButtons()
        {
            btnPrevMy.Visible = _myCurrentPage > 0;
            btnNextMy.Visible = (_myCurrentPage + 1) * PAGE_SIZE < _allMyQuizzes.Count;
        }

        // ======================================================
        // LOGIC BỘ ĐỀ CÔNG KHAI
        // ======================================================

        public async Task LoadPublicQuizzesAsync(int maChuDe)
        {
            if (_isDataLoading) return;
            _isDataLoading = true;

            try
            {
                var ds = await _boDeHocService.GetByFilterAsync(maChuDe);
                _allPublicQuizzes = ds?.ToList() ?? new List<BoDeHocResponse>();
                _publicCurrentPage = 0;
                DisplayPublicQuizzesByPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải bộ đề công khai: " + ex.Message);
            }
            finally { _isDataLoading = false; }
        }

        private void DisplayPublicQuizzesByPage()
        {
            var pagedData = _allPublicQuizzes
                .Skip(_publicCurrentPage * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToList();

            PopulateFlowPanel(flowBoDeCongKhai, pagedData);
            UpdatePublicNavigationButtons();
        }

        private void ChangePublicPage(int direction)
        {
            int newPage = _publicCurrentPage + direction;
            if (newPage >= 0 && newPage * PAGE_SIZE < _allPublicQuizzes.Count)
            {
                _publicCurrentPage = newPage;
                DisplayPublicQuizzesByPage();
            }
        }

        private void UpdatePublicNavigationButtons()
        {
            btnPrevPublic.Visible = _publicCurrentPage > 0;
            btnNextPublic.Visible = (_publicCurrentPage + 1) * PAGE_SIZE < _allPublicQuizzes.Count;
        }

        // ======================================================
        // CƠ CHẾ HIỂN THỊ UI
        // ======================================================

        private void PopulateFlowPanel(FlowLayoutPanel panel, IEnumerable<BoDeHocResponse> data)
        {
            if (panel.InvokeRequired)
            {
                panel.Invoke(new Action(() => PopulateFlowPanel(panel, data)));
                return;
            }

            // --- BƯỚC 1: Khóa layout của panel chính để tránh nhảy trang ---
            pnlMainContent.SuspendLayout();
            panel.SuspendLayout();

            try
            {
                panel.Controls.Clear();

                if (data != null)
                {
                    foreach (var boDe in data)
                    {
                        var item = new BoDeItemControl();
                        item.SetData(boDe.MaBoDe, boDe.TieuDe, boDe.SoLuongThe, boDe.SoLuotHoc, boDe.AnhBia ?? "");
                        item.OnVaoThiClick += (s, ev) => StartQuiz(boDe.MaBoDe);
                        panel.Controls.Add(item);
                    }
                }
            }
            finally
            {
                // --- BƯỚC 2: Mở khóa và vẽ lại một lần duy nhất ---
                panel.ResumeLayout();
                pnlMainContent.ResumeLayout(true);
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

        private void SetDoubleBuffered(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }
    }
}
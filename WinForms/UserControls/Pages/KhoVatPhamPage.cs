using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO;
using StudyApp.DTO.Responses.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using WinForms.UserControls.Tasks;

namespace WinForms.UserControls.Pages
{
    public partial class KhoVatPhamPage : UserControl
    {
        private readonly IItemShopService _itemShopService;
        private List<KhoNguoiDungResponse> _inventoryList = new();

        public KhoVatPhamPage(IItemShopService itemShopService)
        {
            InitializeComponent();
            _itemShopService = itemShopService;
            this.Load += async (s, e) => await LoadInventory();

            // Lắng nghe event mua vật phẩm
            AppEvents.ItemBought += OnItemBought;
        }

        private void OnItemBought(int itemId)
        {
            // Reload kho khi có vật phẩm mới được mua
            _ = LoadInventory();
        }

        private async Task LoadInventory()
        {
            if (!UserSession.IsLoggedIn) return;

            // Xóa dữ liệu cũ
            flpInventory.Controls.Clear();

            // Hiển thị loading
            Label lblLoading = new Label 
            { 
                Text = "⏳ Đang tải kho vật phẩm...", 
                AutoSize = true, 
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.LightGray
            };
            flpInventory.Controls.Add(lblLoading);

            try
            {
                _inventoryList = await _itemShopService.GetMyInventoryAsync(UserSession.CurrentUser!.MaNguoiDung);

                flpInventory.Controls.Clear();

                if (_inventoryList.Count == 0)
                {
                    Label lblEmpty = new Label 
                    { 
                        Text = "📦 Kho trống! Hãy mua vật phẩm từ cửa hàng.", 
                        AutoSize = true,
                        ForeColor = Color.LightGray,
                        Font = new Font("Segoe UI", 12)
                    };
                    flpInventory.Controls.Add(lblEmpty);
                    return;
                }

                // Hiển thị từng vật phẩm trong kho
                foreach (var item in _inventoryList)
                {
                    Panel card = CreateInventoryItemCard(item);
                    flpInventory.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateInventoryItemCard(KhoNguoiDungResponse item)
        {
            Panel pnlCard = new Panel
            {
                Width = 220,
                Height = 320,
                BackColor = Color.FromArgb(45, 45, 48),
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };
            pnlCard.Paint += (s, e) => 
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(100, 100, 100), 2), pnlCard.ClientRectangle);
            };

            // Icon
            Label lblIcon = new Label
            {
                Text = item.TenVatPham?.Contains("Hồi Sinh") == true ? "❤️" : "📦",
                Font = new Font("Segoe UI", 50),
                Dock = DockStyle.Top,
                Height = 100,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(60, 60, 60)
            };

            // Tên vật phẩm
            Label lblName = new Label
            {
                Text = item.TenVatPham ?? "Không xác định",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 45,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                AutoSize = false,
                Padding = new Padding(5)
            };

            // Số lượng
            Label lblQuantity = new Label
            {
                Text = $"Số lượng: {item.SoLuong}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 35,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(0, 200, 100),
                BackColor = Color.FromArgb(35, 35, 35)
            };

            // Panel cho các nút
            Panel pnlButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(35, 35, 35)
            };

            // Nút Sử dụng
            Button btnUse = new Button
            {
                Text = "✓ SỬ DỤNG",
                Dock = DockStyle.Left,
                Width = 110,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 150, 100),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnUse.FlatAppearance.BorderSize = 0;
            btnUse.MouseEnter += (s, e) => btnUse.BackColor = Color.FromArgb(0, 180, 120);
            btnUse.MouseLeave += (s, e) => btnUse.BackColor = Color.FromArgb(0, 150, 100);
            btnUse.Click += async (s, e) => await HandleUseItem(item);

            // Nút Hủy
            Button btnCancel = new Button
            {
                Text = "✕ HỦY",
                Dock = DockStyle.Right,
                Width = 110,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(200, 50, 50),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.MouseEnter += (s, e) => btnCancel.BackColor = Color.FromArgb(220, 70, 70);
            btnCancel.MouseLeave += (s, e) => btnCancel.BackColor = Color.FromArgb(200, 50, 50);
            btnCancel.Click += (s, e) => HandleCancelItem(item);

            pnlButtons.Controls.Add(btnUse);
            pnlButtons.Controls.Add(btnCancel);

            pnlCard.Controls.Add(pnlButtons);
            pnlCard.Controls.Add(lblQuantity);
            pnlCard.Controls.Add(lblName);
            pnlCard.Controls.Add(lblIcon);

            return pnlCard;
        }

        private async Task HandleUseItem(KhoNguoiDungResponse item)
        {
            if (UserSession.CurrentUser == null)
            {
                ToastForm.ShowError("Vui lòng đăng nhập!");
                return;
            }

            if (item.SoLuong <= 0)
            {
                ToastForm.ShowError("Số lượng không đủ!");
                return;
            }

            if (string.IsNullOrEmpty(item.TenVatPham) || !item.TenVatPham.Contains("Hồi Sinh"))
            {
                ToastForm.ShowError("Vật phẩm này không thể sử dụng để hồi sinh chuỗi!");
                return;
            }

            if (MessageBox.Show(
                $"Sử dụng '{item.TenVatPham}' để hồi sinh chuỗi?\n\nChuỗi hiện tại: {UserSession.CurrentUser.ChuoiNgayHocLienTiep} ngày",
                "Xác nhận sử dụng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine($"[UseItem] Starting: {item.TenVatPham}, MaKho: {item.MaKho}");
                    
                    string result = await _itemShopService.UseResurrectItemAsync(UserSession.CurrentUser.MaNguoiDung, item.MaKho);

                    System.Diagnostics.Debug.WriteLine($"[UseItem] Result: {result}");

                    // ✅ KIỂM TRA RESPONSE - NẾU THẤT BẠI SHOW ERROR
                    if (result.Contains("❌") || result.Contains("Không thể sử dụng"))
                    {
                        ToastForm.ShowError(result);
                        return;
                    }

                    // ✅ NẾU THÀNH CÔNG
                    if (result.Contains("Đã hồi sinh") || result.Contains("Đã khởi động"))
                    {
                        ToastForm.ShowSuccess(result);
                        
                        // ✅ CẬP NHẬT USERSESSION
                        if (result.Contains("Chuỗi hiện tại: 2 ngày"))
                            UserSession.CurrentUser.ChuoiNgayHocLienTiep = 2;
                        else if (result.Contains("Chuỗi hiện tại: 1 ngày"))
                            UserSession.CurrentUser.ChuoiNgayHocLienTiep = 1;

                        // ✅ DELAY 3.5 GIÂY ĐỂ TOAST KỊP TẮT TRƯỚC KHI RELOAD
                        await Task.Delay(3500);
                        await LoadInventory();
                    }
                    else
                    {
                        ToastForm.ShowError(result);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[UseItem Exception] {ex.GetType().Name}: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"[UseItem Exception] StackTrace: {ex.StackTrace}");
                    
                    ToastForm.ShowError($"Lỗi: {ex.Message}");
                }
            }
        }

        private void HandleCancelItem(KhoNguoiDungResponse item)
        {
            // Không làm gì, chỉ đóng
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AppEvents.ItemBought -= OnItemBought;
            }
            base.Dispose(disposing);
        }
    }
}

using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WinForms.UserControls.Tasks;
using System.Threading.Tasks;
using StudyApp.DAL.Entities;

namespace WinForms.UserControls.Pages
{
    public partial class CuaHangPage : UserControl
    {
        private readonly IItemShopService _itemShopService;
        public CuaHangPage(IItemShopService itemShopService)
        {
            InitializeComponent();
            _itemShopService = itemShopService;
            this.Load += async (s, e) => await LoadShopData();
        }

        private async Task LoadShopData()
        {
            if (!UserSession.IsLoggedIn) return;

            // Cập nhật số tiền lên Label bạn đã kéo (lblBalance)
            UpdateBalanceLabel();

            // Xóa dữ liệu cũ (nếu có)
            flpContainer.Controls.Clear();

            // Hiển thị chữ "Đang tải..." tạm thời
            Label lblLoading = new Label { Text = "Đang lấy dữ liệu...", AutoSize = true, Font = new Font("Segoe UI", 12) };
            flpContainer.Controls.Add(lblLoading);

            try
            {
                var items = await _itemShopService.GetShopItemsAsync();

                flpContainer.Controls.Clear(); // Xóa chữ loading

                if (items.Count == 0)
                {
                    flpContainer.Controls.Add(new Label { Text = "Hết hàng!", AutoSize = true });
                    return;
                }

                // TẠO THẺ BÀI (CARD) CHO TỪNG MÓN
                // Phần này bắt buộc phải code vì số lượng vật phẩm là động (Dynamic)
                foreach (var item in items)
                {
                    Panel card = CreateItemCard(item) ;
                    flpContainer.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void UpdateBalanceLabel()
        {
            if (UserSession.CurrentUser == null) return;
            string vang = UserSession.CurrentUser.Vang.ToString("N0") ?? "0";
            string kc = UserSession.CurrentUser.KimCuong.ToString("N0") ?? "0";

            // Gán text vào Label bạn đã kéo thả
            lblbalance.Text = $"🪙 {vang}   |   💎 {kc}";
        }


        private Panel CreateItemCard(VatPhamResponse item)
        {
            Panel pnlCard = new Panel
            {
                Width = 200,
                Height = 280,
                BackColor = Color.White,
                Margin = new Padding(10)
            };
            pnlCard.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, pnlCard.ClientRectangle, Color.Silver, ButtonBorderStyle.Solid);

            // Icon
            Label lblIcon = new Label
            {
                Text = item.TenVatPham.Contains("Hồi Sinh") ? "❤️" : "📦",
                Font = new Font("Segoe UI", 40),
                Dock = DockStyle.Top,
                Height = 100,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Tên
            Label lblName = new Label
            {
                Text = item.TenVatPham,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Giá
            bool isVang = item.LoaiTienTe == LoaiTienTeEnum.Vang;
            Label lblPrice = new Label
            {
                Text = (isVang ? "🪙 " : "💎 ") + item.Gia.ToString("N0"),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = isVang ? Color.Orange : Color.DodgerBlue
            };

            // Nút Mua
            Button btnBuy = new Button
            {
                Text = "MUA",
                Dock = DockStyle.Bottom,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = isVang ? Color.Orange : Color.DodgerBlue,
                ForeColor = Color.White
            };
            btnBuy.FlatAppearance.BorderSize = 0;
            btnBuy.Click += async (s, e) => await HandleBuyAction(item);

            pnlCard.Controls.Add(lblPrice);
            pnlCard.Controls.Add(lblName);
            pnlCard.Controls.Add(lblIcon);
            pnlCard.Controls.Add(btnBuy);

            return pnlCard;
        }

        private async Task HandleBuyAction(VatPhamResponse item)
        {
            if (UserSession.CurrentUser == null)
            {
                ToastForm.ShowError("Vui lòng đăng nhập!");
                return;
            }

            if (MessageBox.Show($"Mua '{item.TenVatPham}'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string result = await _itemShopService.BuyItemAsync(UserSession.CurrentUser.MaNguoiDung, item.MaVatPham, 1);

                if (result == "Mua thành công!")
                {
                    // Cập nhật hiển thị tiền
                    if (item.LoaiTienTe == LoaiTienTeEnum.Vang)
                        UserSession.CurrentUser.Vang -= item.Gia;
                    else
                        UserSession.CurrentUser.KimCuong -= item.Gia;                

                    UpdateBalanceLabel();
                    
                    // ✅ KHÔNG SHOW TOAST, CHỈ LOAD LẠI
                    // ToastForm.ShowSuccess("Mua thành công!");
                    
                    AppEvents.RaiseItemBought(item.MaVatPham);
                    await LoadShopData();
                }
                else
                {
                    ToastForm.ShowError(result);
                }
            }
        }
    }
}

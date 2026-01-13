using System;
using System.Windows.Forms;
using StudyApp.DTO.Enums;

namespace WinForms.UserControls.Quiz
{
    public partial class LoaiTheSelectorControl : UserControl
    {
        // Event để thông báo về trang cha khi một loại thẻ được chọn
        public event Action<LoaiTheEnum>? OnTypeSelected;

        public LoaiTheSelectorControl()
        {
            InitializeComponent();
            RegisterEvents();
        }

        /// <summary>
        /// Phương thức tĩnh giúp khởi tạo Selector nhanh chóng từ trang cha
        /// </summary>
        public static LoaiTheSelectorControl Create(Action<LoaiTheEnum> onSelected)
        {
            var control = new LoaiTheSelectorControl();
            // Đăng ký sự kiện ngay khi khởi tạo
            control.OnTypeSelected += onSelected;
            return control;
        }

        private void RegisterEvents()
        {
            // Gán logic cho từng nút bấm dựa trên Enum bạn đã định nghĩa
            btnTracNghiem.Click += (s, e) => OnTypeSelected?.Invoke(LoaiTheEnum.TracNghiem);

            // Lưu ý: Bạn đang dùng LoaiTheEnum.CoBan cho Lật thẻ
            btnLatThe.Click += (s, e) => OnTypeSelected?.Invoke(LoaiTheEnum.CoBan);

            btnDienKhuyet.Click += (s, e) => OnTypeSelected?.Invoke(LoaiTheEnum.DienKhuyet);
            btnGhepCap.Click += (s, e) => OnTypeSelected?.Invoke(LoaiTheEnum.GhepCap);
            btnSapXep.Click += (s, e) => OnTypeSelected?.Invoke(LoaiTheEnum.SapXep);
        }
    }
}
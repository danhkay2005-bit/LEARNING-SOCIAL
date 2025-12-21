using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.BaiHoc
{
    public class TheDTO
    {
        //các thông tin cơ bản của thẻ :)))
        // Mã thẻ (Primary Key, Identity)
        public int MaThe { get; set; }

        // mã bộ đề đang chứa thẻ này (foreign key)
        public int MaBoDe { get; set; }

        // Loại thẻ:  Coban(thẻ lật flashcard), TracNghiem, DienKhuyet, GhepCap, SapXep, NgheViet, HinhAnh
        public string? LoaiThe { get; set; }

        
        // nội dung thẻ 
        

        // mặt trước của thẻ(câu hỏi / từ khoá)
        public string? MatTruoc { get; set; }

        // mặt sau của thẻ (câu trả lời / Định nghĩa)
        public string? MatSau { get; set; }



        //bổ sung 


        //giải thích chi tiết về đáp án 
        public string? GiaiThich { get; set; }

        // gợi ý
        public string? GoiY { get; set; }
        
        //viet tắt 
        public string? VietTat { get; set; }



        // MEDIA (Hình ảnh & Âm thanh)

        // URL hoặc Base64 của hình ảnh mặt trước
        public string? HinhAnhTruoc { get; set; }

        // URL hoặc Base64 của hình ảnh mặt sau
        public string? HinhAnhSau { get; set; }

        // URL file  âm thanh mặt trước (MP3,...)
        public string? AmThanhTruoc {get; set; }

        // URL file âm thanh mặt sau (MP3,...)
        public string? AmThanhSau { get; set; }


        // THÔNG TIN THỨ TỰ & ĐỘ KHÓ
        // ============================================================

        /// <summary>
        /// Thứ tự hiển thị thẻ trong bộ đề (0, 1, 2...)
        /// </summary>
        public int ThuTu { get; set; }

        /// <summary>
        /// Độ khó do người tạo đặt (1-5)
        /// </summary>
        public byte DoKho { get; set; }

        // ============================================================
        // THỐNG KÊ
        // ============================================================

        /// <summary>
        /// Tổng số lần thẻ được học
        /// </summary>
        public int SoLuotHoc { get; set; }

        /// <summary>
        /// Số lần trả lời đúng
        /// </summary>
        public int SoLanDung { get; set; }

        /// <summary>
        /// Số lần trả lời sai
        /// </summary>
        public int SoLanSai { get; set; }

        /// <summary>
        /// Tỷ lệ đúng trung bình (%) = SoLanDung / SoLuotHoc * 100
        /// </summary>
        public double TyLeDungTB { get; set; }

        // ============================================================
        // THỜI GIAN
        // ============================================================

        /// <summary>
        /// Thời điểm tạo thẻ
        /// </summary>
        public DateTime ThoiGianTao { get; set; }

        /// <summary>
        /// Lần cập nhật gần nhất
        /// </summary>
        public DateTime ThoiGianCapNhat { get; set; }




    }
}

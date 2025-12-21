using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.BaiHoc
{
    // DTO cho bảng PhienHoc
    // Đại diện cho một phiên học (session)
    public class PhienHocDTO
    {
        //mã phiên học (Primary Key, Identity)
        public int? MaPhien { get; set;  }

        //mã người dùng (Foreign Key → App_NguoiDung) . guid - là mã định danh duy nhất trên phạm vi toàn cục 
        public Guid MaNguoiDung { get; set; }


        //mã bộ đề // Bộ đề được học (Foreign Key), có thể null (học tự chọn thẻ)
        public int? MaBoDe { get; set; }

        //Loai phiên học:  Học mới, Ôn tập, Kiểm tra , thách đấu ,tuỳ chọn
        public string? LoaiPhien { get; set; }

        //thời gian bắt đầu phiên học
        public DateTime ThoiGianBatDau { get; set; }

        //thời gian kết thúc phiên học
        public DateTime? ThoiGianKetThuc { get; set; }


        //thời gian học (giây)
        public int ThoiGianHocGiay { get; set; }

        //tổng số thẻ trong phiên học
        public int TongSoThe { get; set; }



        // Số thẻ mới (chưa từng học)
        public int SoTheMoi { get; set; }



        // Số thẻ ôn tập (đã học trước đó)
        public int SoTheOnTap { get; set; }



        // Số thẻ trả lời đúng
        public int SoTheDung { get; set; }

        
        /// Số thẻ trả lời sai
        
        public int SoTheSai { get; set; }

        
        // Số thẻ bỏ qua (skip)
        
        public int SoTheBo { get; set; }

       
        // Điểm đạt được
        
        public int DiemDat { get; set; }

        
        // Điểm tối đa có thể đạt
        
        public int DiemToiDa { get; set; }

        
        // Tỷ lệ đúng (%)
        
        public double TyLeDung { get; set; }


        
        
        
        
                         // PHẦN THƯỞNG




        // Số XP nhận được sau phiên học
        public int XPNhan { get; set; }



        // Số vàng nhận được
        public int VangNhan { get; set; }



        // Cảm xúc sau khi học (1-5: 1=😢, 5=😄)
        public byte? CamXuc { get; set; }



        // Ghi chú của người dùng
        public string? GhiChu { get; set; }



        // Nếu là phiên thách đấu, lưu mã thách đấu
        public int? MaThachDau { get; set; }

        
        // THÔNG TIN BỔ SUNG (JOIN)
        



        //Tiêu đề bộ đề (JOIN từ BoDeHoc)
        public string? TieuDeBoDe { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.BaiHoc
{
    
    // DTO cho bảng DapAnTracNghiem
    //Chứa các đáp án cho thẻ trắc nghiệm
    
    public class DapAnTracNghiemDTO
    {
       
        // Mã đáp án (Primary Key)
        
        public int MaDapAn { get; set; }


        
        //Mã thẻ chứa đáp án này (Foreign Key)
        
        public int MaThe { get; set; }

        
        // Nội dung đáp án
        // VD: "Quả táo"
        public string? NoiDung { get; set; }

        
        // Đáp án này có đúng không?
        // true = Đáp án đúng, false = Đáp án sai
        public bool LaDapAnDung { get; set; }

        
        // Thứ tự hiển thị (A, B, C, D → 0, 1, 2, 3)
        public int ThuTu { get; set; }

        
        // Giải thích tại sao đáp án này đúng/sai
        public string? GiaiThich { get; set; }
    }
}

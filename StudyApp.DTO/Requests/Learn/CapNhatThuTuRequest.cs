using System.Collections.Generic;

namespace StudyApp.DTO.Requests.Learn
{
    public class CapNhatThuTuRequest
    {
        public int MaCha { get; set; } // ID Bộ đề hoặc Thư mục cha
        public Dictionary<int, int> ThuTuMoi { get; set; } = []; // Key=ID, Value=Order
    }

    
}
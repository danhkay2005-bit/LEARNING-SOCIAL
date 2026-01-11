using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class CapNhatTienDoRequest
    {
        [Required] public Guid MaNguoiDung { get; set; }
        [Required] public int MaThe { get; set; }

        public bool KetQuaDung { get; set; }

        // Thông số thuật toán SRS (Spaced Repetition)
        public float HeSoDeMoi { get; set; }
        public int KhoangCachNgayMoi { get; set; }
        public DateTime NgayOnTapTiepTheo { get; set; }
    }
}
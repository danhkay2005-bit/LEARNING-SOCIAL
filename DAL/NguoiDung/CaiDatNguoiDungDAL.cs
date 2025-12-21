using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class CaiDatNguoiDungDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        private readonly DatabaseHelper _dbHelper = new();
        // ============================================================
        // TẠO MẶC ĐỊNH
        // ============================================================

        public int TaoMacDinh(Guid maNguoiDung)
        {
            string query = @"INSERT INTO CaiDatNguoiDung (MaNguoiDung) 
                            VALUES (@MaNguoiDung)";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // LẤY DỮ LIỆU
        // ============================================================

        public DataTable LayTheoNguoiDung(Guid maNguoiDung)
        {
            string query = "SELECT * FROM CaiDatNguoiDung WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT GIAO DIỆN
        // ============================================================

        public int CapNhatGiaoDien(Guid maNguoiDung, bool cheDoToi, bool coHieuUng)
        {
            string query = @"UPDATE CaiDatNguoiDung 
                            SET CheDoToi = @CheDoToi, 
                                CoHieuUng = @CoHieuUng,
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@CheDoToi", cheDoToi),
                new SqlParameter("@CoHieuUng", coHieuUng)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT THÔNG BÁO
        // ============================================================

        public int CapNhatThongBao(Guid maNguoiDung, bool thongBaoNhacHoc, TimeSpan gioNhacHoc,
                                   bool thongBaoThanhTuu, bool thongBaoXaHoi, bool thongBaoThachDau)
        {
            string query = @"UPDATE CaiDatNguoiDung 
                            SET ThongBaoNhacHoc = @ThongBaoNhacHoc, 
                                GioNhacHoc = @GioNhacHoc,
                                ThongBaoThanhTuu = @ThongBaoThanhTuu,
                                ThongBaoXaHoi = @ThongBaoXaHoi,
                                ThongBaoThachDau = @ThongBaoThachDau,
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@ThongBaoNhacHoc", thongBaoNhacHoc),
                new SqlParameter("@GioNhacHoc", gioNhacHoc),
                new SqlParameter("@ThongBaoThanhTuu", thongBaoThanhTuu),
                new SqlParameter("@ThongBaoXaHoi", thongBaoXaHoi),
                new SqlParameter("@ThongBaoThachDau", thongBaoThachDau)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT QUYỀN RIÊNG TƯ
        // ============================================================

        public int CapNhatQuyenRiengTu(Guid maNguoiDung, bool hienThiTrangThai, bool hienThiThongKe,
                                       bool choPhepThachDau, bool choPhepNhanTin, bool choPhepTag)
        {
            string query = @"UPDATE CaiDatNguoiDung 
                            SET HienThiTrangThai = @HienThiTrangThai, 
                                HienThiThongKe = @HienThiThongKe,
                                ChoPhepThachDau = @ChoPhepThachDau,
                                ChoPhepNhanTin = @ChoPhepNhanTin,
                                ChoPhepTagTrongBaiDang = @ChoPhepTag,
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@HienThiTrangThai", hienThiTrangThai),
                new SqlParameter("@HienThiThongKe", hienThiThongKe),
                new SqlParameter("@ChoPhepThachDau", choPhepThachDau),
                new SqlParameter("@ChoPhepNhanTin", choPhepNhanTin),
                new SqlParameter("@ChoPhepTag", choPhepTag)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }
    }
}
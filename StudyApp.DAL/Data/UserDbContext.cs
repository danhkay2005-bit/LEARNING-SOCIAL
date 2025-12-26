using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudyApp.DAL.Entities.User;

namespace StudyApp.DAL.Data;

public partial class UserDbContext : DbContext
{
    public UserDbContext()
    {
    }

    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaoVeChuoiNgay> BaoVeChuoiNgays { get; set; }

    public virtual DbSet<BoostDangHoatDong> BoostDangHoatDongs { get; set; }

    public virtual DbSet<CaiDatNguoiDung> CaiDatNguoiDungs { get; set; }

    public virtual DbSet<CapDo> CapDos { get; set; }

    public virtual DbSet<CauHinhDiemDanh> CauHinhDiemDanhs { get; set; }

    public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; }

    public virtual DbSet<DiemDanhHangNgay> DiemDanhHangNgays { get; set; }

    public virtual DbSet<KhoNguoiDung> KhoNguoiDungs { get; set; }

    public virtual DbSet<LichSuGiaoDich> LichSuGiaoDiches { get; set; }

    public virtual DbSet<LichSuHoatDong> LichSuHoatDongs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhiemVu> NhiemVus { get; set; }

    public virtual DbSet<ThanhTuu> ThanhTuus { get; set; }

    public virtual DbSet<ThanhTuuDatDuoc> ThanhTuuDatDuocs { get; set; }

    public virtual DbSet<TienDoNhiemVu> TienDoNhiemVus { get; set; }

    public virtual DbSet<TuyChinhProfile> TuyChinhProfiles { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    public virtual DbSet<VatPham> VatPhams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UserDb"].ConnectionString;
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaoVeChuoiNgay>(entity =>
        {
            entity.HasKey(e => e.MaBaoVe).HasName("PK__BaoVeChu__E3227940636CE262");

            entity.ToTable("BaoVeChuoiNgay");

            entity.Property(e => e.LoaiBaoVe)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.BaoVeChuoiNgays)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("FK_BaoVe_NguoiDung");
        });

        modelBuilder.Entity<BoostDangHoatDong>(entity =>
        {
            entity.HasKey(e => e.MaBoost).HasName("PK__BoostDan__B8FEA1502AEDF93C");

            entity.ToTable("BoostDangHoatDong");

            entity.HasIndex(e => new { e.ConHieuLuc, e.ThoiGianKetThuc }, "IX_Boost_ConHieuLuc");

            entity.HasIndex(e => e.MaNguoiDung, "IX_Boost_NguoiDung");

            entity.Property(e => e.ConHieuLuc).HasDefaultValue(true);
            entity.Property(e => e.LoaiBoost)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ThoiGianBatDau)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.BoostDangHoatDongs)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("FK_Boost_NguoiDung");

            entity.HasOne(d => d.MaVatPhamNavigation).WithMany(p => p.BoostDangHoatDongs)
                .HasForeignKey(d => d.MaVatPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Boost_VatPham");
        });

        modelBuilder.Entity<CaiDatNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__CaiDatNg__C539D762F788F6B9");

            entity.ToTable("CaiDatNguoiDung");

            entity.Property(e => e.MaNguoiDung).ValueGeneratedNever();
            entity.Property(e => e.CheDoToi).HasDefaultValue(false);
            entity.Property(e => e.ChoPhepNhanTin).HasDefaultValue(true);
            entity.Property(e => e.ChoPhepTagTrongBaiDang).HasDefaultValue(true);
            entity.Property(e => e.ChoPhepThachDau).HasDefaultValue(true);
            entity.Property(e => e.CoHieuUng).HasDefaultValue(true);
            entity.Property(e => e.GioNhacHoc).HasDefaultValue(new TimeOnly(20, 0, 0));
            entity.Property(e => e.HienThiThongKe).HasDefaultValue(true);
            entity.Property(e => e.HienThiTrangThai).HasDefaultValue(true);
            entity.Property(e => e.ThoiGianCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThongBaoNhacHoc).HasDefaultValue(true);
            entity.Property(e => e.ThongBaoThachDau).HasDefaultValue(true);
            entity.Property(e => e.ThongBaoThanhTuu).HasDefaultValue(true);
            entity.Property(e => e.ThongBaoXaHoi).HasDefaultValue(true);

            entity.HasOne(d => d.MaNguoiDungNavigation).WithOne(p => p.CaiDatNguoiDung)
                .HasForeignKey<CaiDatNguoiDung>(d => d.MaNguoiDung)
                .HasConstraintName("FK_CaiDat_NguoiDung");
        });

        modelBuilder.Entity<CapDo>(entity =>
        {
            entity.HasKey(e => e.MaCapDo).HasName("PK__CapDo__40B881FC8079B203");

            entity.ToTable("CapDo");

            entity.Property(e => e.BieuTuong).HasMaxLength(10);
            entity.Property(e => e.MucXptoiDa).HasColumnName("MucXPToiDa");
            entity.Property(e => e.MucXptoiThieu).HasColumnName("MucXPToiThieu");
            entity.Property(e => e.TenCapDo).HasMaxLength(50);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CauHinhDiemDanh>(entity =>
        {
            entity.HasKey(e => e.NgayThu).HasName("PK__CauHinhD__5F2DCF22A2ABD61A");

            entity.ToTable("CauHinhDiemDanh");

            entity.Property(e => e.NgayThu).ValueGeneratedNever();
            entity.Property(e => e.ThuongDacBiet).HasMaxLength(100);
            entity.Property(e => e.ThuongVang).HasDefaultValue(10);
            entity.Property(e => e.ThuongXp)
                .HasDefaultValue(5)
                .HasColumnName("ThuongXP");
        });

        modelBuilder.Entity<DanhMucSanPham>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMucS__B3750887E3DE6263");

            entity.ToTable("DanhMucSanPham");

            entity.HasIndex(e => e.TenDanhMuc, "UQ__DanhMucS__650CAE4EB36F2FB2").IsUnique();

            entity.Property(e => e.BieuTuong).HasMaxLength(10);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(100);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThuTuHienThi).HasDefaultValue(0);
        });

        modelBuilder.Entity<DiemDanhHangNgay>(entity =>
        {
            entity.HasKey(e => e.MaDiemDanh).HasName("PK__DiemDanh__1512439D0F9459EE");

            entity.ToTable("DiemDanhHangNgay");

            entity.HasIndex(e => new { e.MaNguoiDung, e.NgayDiemDanh }, "UQ_DiemDanh").IsUnique();

            entity.Property(e => e.NgayThuMay).HasDefaultValue(1);
            entity.Property(e => e.ThuongDacBiet).HasMaxLength(100);
            entity.Property(e => e.ThuongVang).HasDefaultValue(0);
            entity.Property(e => e.ThuongXp)
                .HasDefaultValue(0)
                .HasColumnName("ThuongXP");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.DiemDanhHangNgays)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("FK_DiemDanh_NguoiDung");
        });

        modelBuilder.Entity<KhoNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaKho).HasName("PK__KhoNguoi__3BDA9350826358D8");

            entity.ToTable("KhoNguoiDung");

            entity.HasIndex(e => e.MaNguoiDung, "IX_Kho_NguoiDung");

            entity.HasIndex(e => new { e.MaNguoiDung, e.MaVatPham }, "UQ_Kho_NguoiDung_VatPham").IsUnique();

            entity.Property(e => e.DaTrangBi).HasDefaultValue(false);
            entity.Property(e => e.SoLuong).HasDefaultValue(1);
            entity.Property(e => e.ThoiGianHetHan).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianMua)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.KhoNguoiDungs)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("FK_Kho_NguoiDung");

            entity.HasOne(d => d.MaVatPhamNavigation).WithMany(p => p.KhoNguoiDungs)
                .HasForeignKey(d => d.MaVatPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kho_VatPham");
        });

        modelBuilder.Entity<LichSuGiaoDich>(entity =>
        {
            entity.HasKey(e => e.MaGiaoDich).HasName("PK__LichSuGi__0A2A24EBE1F8BC55");

            entity.ToTable("LichSuGiaoDich");

            entity.HasIndex(e => e.MaNguoiDung, "IX_GiaoDich_NguoiDung");

            entity.HasIndex(e => e.ThoiGian, "IX_GiaoDich_ThoiGian").IsDescending();

            entity.Property(e => e.LoaiGiaoDich)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LoaiTien)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.LichSuGiaoDiches)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("FK_GiaoDich_NguoiDung");

            entity.HasOne(d => d.MaVatPhamNavigation).WithMany(p => p.LichSuGiaoDiches)
                .HasForeignKey(d => d.MaVatPham)
                .HasConstraintName("FK_GiaoDich_VatPham");
        });

        modelBuilder.Entity<LichSuHoatDong>(entity =>
        {
            entity.HasKey(e => e.MaHoatDong).HasName("PK__LichSuHo__BD808BE7156169A8");

            entity.ToTable("LichSuHoatDong");

            entity.HasIndex(e => e.LoaiHoatDong, "IX_LichSuHD_LoaiHoatDong");

            entity.HasIndex(e => e.MaNguoiDung, "IX_LichSuHD_NguoiDung");

            entity.HasIndex(e => e.ThoiGian, "IX_LichSuHD_ThoiGian").IsDescending();

            entity.Property(e => e.LoaiHoatDong)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.LichSuHoatDongs)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("FK_LichSuHD_NguoiDung");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__NguoiDun__C539D762591CEF31");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.Email, "IX_NguoiDung_Email");

            entity.HasIndex(e => e.TenDangNhap, "IX_NguoiDung_TenDangNhap");

            entity.HasIndex(e => e.TongDiemXp, "IX_NguoiDung_TongDiemXP").IsDescending();

            entity.HasIndex(e => e.TenDangNhap, "UQ__NguoiDun__55F68FC08CE17F44").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D105345B7C9569").IsUnique();

            entity.Property(e => e.MaNguoiDung).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ChuoiNgayDaiNhat).HasDefaultValue(0);
            entity.Property(e => e.ChuoiNgayHocLienTiep).HasDefaultValue(0);
            entity.Property(e => e.DaXacThucEmail).HasDefaultValue(false);
            entity.Property(e => e.DaXoa).HasDefaultValue(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HoVaTen)
                .HasMaxLength(100)
                .HasDefaultValue("Thành Viên Mới");
            entity.Property(e => e.KimCuong).HasDefaultValue(5);
            entity.Property(e => e.LanOnlineCuoi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MaCapDo).HasDefaultValue(1);
            entity.Property(e => e.MaVaiTro).HasDefaultValue(3);
            entity.Property(e => e.MatKhauMaHoa)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NgayHoatDongCuoi).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SoStreakFreeze).HasDefaultValue(2);
            entity.Property(e => e.SoStreakHoiSinh).HasDefaultValue(1);
            entity.Property(e => e.SoTranThachDau).HasDefaultValue(0);
            entity.Property(e => e.SoTranThang).HasDefaultValue(0);
            entity.Property(e => e.SoTranThua).HasDefaultValue(0);
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TieuSu).HasMaxLength(500);
            entity.Property(e => e.TongDiemXp)
                .HasDefaultValue(0)
                .HasColumnName("TongDiemXP");
            entity.Property(e => e.TongSoTheDung).HasDefaultValue(0);
            entity.Property(e => e.TongSoTheHoc).HasDefaultValue(0);
            entity.Property(e => e.TongThoiGianHocPhut).HasDefaultValue(0);
            entity.Property(e => e.TrangThaiOnline).HasDefaultValue(false);
            entity.Property(e => e.Vang).HasDefaultValue(100);

            entity.HasOne(d => d.MaCapDoNavigation).WithMany(p => p.NguoiDungs)
                .HasForeignKey(d => d.MaCapDo)
                .HasConstraintName("FK_NguoiDung_CapDo");

            entity.HasOne(d => d.MaVaiTroNavigation).WithMany(p => p.NguoiDungs)
                .HasForeignKey(d => d.MaVaiTro)
                .HasConstraintName("FK_NguoiDung_VaiTro");
        });

        modelBuilder.Entity<NhiemVu>(entity =>
        {
            entity.HasKey(e => e.MaNhiemVu).HasName("PK__NhiemVu__69582B2F9D56ADFE");

            entity.ToTable("NhiemVu");

            entity.Property(e => e.BieuTuong).HasMaxLength(10);
            entity.Property(e => e.ConHieuLuc).HasDefaultValue(true);
            entity.Property(e => e.LoaiDieuKien)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LoaiNhiemVu)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.TenNhiemVu).HasMaxLength(200);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThuongKimCuong).HasDefaultValue(0);
            entity.Property(e => e.ThuongVang).HasDefaultValue(0);
            entity.Property(e => e.ThuongXp)
                .HasDefaultValue(0)
                .HasColumnName("ThuongXP");
        });

        modelBuilder.Entity<ThanhTuu>(entity =>
        {
            entity.HasKey(e => e.MaThanhTuu).HasName("PK__ThanhTuu__F624200FDD05876E");

            entity.ToTable("ThanhTuu");

            entity.Property(e => e.BiAn).HasDefaultValue(false);
            entity.Property(e => e.BieuTuong).HasMaxLength(10);
            entity.Property(e => e.DieuKienLoai)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DoHiem).HasDefaultValue((byte)1);
            entity.Property(e => e.LoaiThanhTuu)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenThanhTuu).HasMaxLength(100);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThuongKimCuong).HasDefaultValue(0);
            entity.Property(e => e.ThuongVang).HasDefaultValue(0);
            entity.Property(e => e.ThuongXp)
                .HasDefaultValue(0)
                .HasColumnName("ThuongXP");
        });

        modelBuilder.Entity<ThanhTuuDatDuoc>(entity =>
        {
            entity.HasKey(e => new { e.MaNguoiDung, e.MaThanhTuu }).HasName("PK__ThanhTuu__2A5B95620F8F08A8");

            entity.ToTable("ThanhTuuDatDuoc");

            entity.Property(e => e.DaChiaSe).HasDefaultValue(false);
            entity.Property(e => e.DaXem).HasDefaultValue(false);
            entity.Property(e => e.NgayDat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.ThanhTuuDatDuocs)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("FK_ThanhTuuDat_NguoiDung");

            entity.HasOne(d => d.MaThanhTuuNavigation).WithMany(p => p.ThanhTuuDatDuocs)
                .HasForeignKey(d => d.MaThanhTuu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThanhTuuDat_ThanhTuu");
        });

        modelBuilder.Entity<TienDoNhiemVu>(entity =>
        {
            entity.HasKey(e => new { e.MaNguoiDung, e.MaNhiemVu }).HasName("PK__TienDoNh__23AC55D0E28C1DBE");

            entity.ToTable("TienDoNhiemVu");

            entity.Property(e => e.DaHoanThanh).HasDefaultValue(false);
            entity.Property(e => e.DaNhanThuong).HasDefaultValue(false);
            entity.Property(e => e.NgayBatDau).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.NgayHoanThanh).HasColumnType("datetime");
            entity.Property(e => e.TienDoHienTai).HasDefaultValue(0);

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.TienDoNhiemVus)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("FK_TienDoNV_NguoiDung");

            entity.HasOne(d => d.MaNhiemVuNavigation).WithMany(p => p.TienDoNhiemVus)
                .HasForeignKey(d => d.MaNhiemVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TienDoNV_NhiemVu");
        });

        modelBuilder.Entity<TuyChinhProfile>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__TuyChinh__C539D762154A1415");

            entity.ToTable("TuyChinhProfile");

            entity.Property(e => e.MaNguoiDung).ValueGeneratedNever();
            entity.Property(e => e.CauChamNgon).HasMaxLength(255);
            entity.Property(e => e.ThoiGianCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaAvatarDangDungNavigation).WithMany(p => p.TuyChinhProfileMaAvatarDangDungNavigations)
                .HasForeignKey(d => d.MaAvatarDangDung)
                .HasConstraintName("FK_TuyChinh_Avatar");

            entity.HasOne(d => d.MaBadgeDangDungNavigation).WithMany(p => p.TuyChinhProfileMaBadgeDangDungNavigations)
                .HasForeignKey(d => d.MaBadgeDangDung)
                .HasConstraintName("FK_TuyChinh_Badge");

            entity.HasOne(d => d.MaHieuUngDangDungNavigation).WithMany(p => p.TuyChinhProfileMaHieuUngDangDungNavigations)
                .HasForeignKey(d => d.MaHieuUngDangDung)
                .HasConstraintName("FK_TuyChinh_HieuUng");

            entity.HasOne(d => d.MaHinhNenDangDungNavigation).WithMany(p => p.TuyChinhProfileMaHinhNenDangDungNavigations)
                .HasForeignKey(d => d.MaHinhNenDangDung)
                .HasConstraintName("FK_TuyChinh_HinhNen");

            entity.HasOne(d => d.MaHuyHieuHienThiNavigation).WithMany(p => p.TuyChinhProfiles)
                .HasForeignKey(d => d.MaHuyHieuHienThi)
                .HasConstraintName("FK_TuyChinh_HuyHieu");

            entity.HasOne(d => d.MaKhungDangDungNavigation).WithMany(p => p.TuyChinhProfileMaKhungDangDungNavigations)
                .HasForeignKey(d => d.MaKhungDangDung)
                .HasConstraintName("FK_TuyChinh_Khung");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithOne(p => p.TuyChinhProfile)
                .HasForeignKey<TuyChinhProfile>(d => d.MaNguoiDung)
                .HasConstraintName("FK_TuyChinh_NguoiDung");

            entity.HasOne(d => d.MaNhacNenDangDungNavigation).WithMany(p => p.TuyChinhProfileMaNhacNenDangDungNavigations)
                .HasForeignKey(d => d.MaNhacNenDangDung)
                .HasConstraintName("FK_TuyChinh_NhacNen");

            entity.HasOne(d => d.MaThemeDangDungNavigation).WithMany(p => p.TuyChinhProfileMaThemeDangDungNavigations)
                .HasForeignKey(d => d.MaThemeDangDung)
                .HasConstraintName("FK_TuyChinh_Theme");
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVaiTro).HasName("PK__VaiTro__C24C41CF37BCE20E");

            entity.ToTable("VaiTro");

            entity.HasIndex(e => e.TenVaiTro, "UQ__VaiTro__1DA558144EEEEE75").IsUnique();

            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenVaiTro).HasMaxLength(50);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<VatPham>(entity =>
        {
            entity.HasKey(e => e.MaVatPham).HasName("PK__VatPham__92DB0A9813419485");

            entity.ToTable("VatPham");

            entity.HasIndex(e => e.MaDanhMuc, "IX_VatPham_DanhMuc");

            entity.HasIndex(e => e.Gia, "IX_VatPham_Gia");

            entity.Property(e => e.ConHang).HasDefaultValue(true);
            entity.Property(e => e.DoHiem).HasDefaultValue((byte)1);
            entity.Property(e => e.GioiHanSoLuong).HasDefaultValue(-1);
            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.SoLuongBanRa).HasDefaultValue(0);
            entity.Property(e => e.TenVatPham).HasMaxLength(100);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.VatPhams)
                .HasForeignKey(d => d.MaDanhMuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VatPham_DanhMuc");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

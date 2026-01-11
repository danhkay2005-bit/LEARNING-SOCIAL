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

    public virtual DbSet<CapDo> CapDos { get; set; }

    public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; }

    public virtual DbSet<DiemDanhHangNgay> DiemDanhHangNgays { get; set; }

    public virtual DbSet<KhoNguoiDung> KhoNguoiDungs { get; set; }

    public virtual DbSet<LichSuGiaoDich> LichSuGiaoDiches { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhiemVu> NhiemVus { get; set; }

    public virtual DbSet<ThanhTuu> ThanhTuus { get; set; }

    public virtual DbSet<ThanhTuuDatDuoc> ThanhTuuDatDuocs { get; set; }

    public virtual DbSet<TienDoNhiemVu> TienDoNhiemVus { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    public virtual DbSet<VatPham> VatPhams { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaoVeChuoiNgay>(entity =>
        {
            entity.HasKey(e => e.MaBaoVe).HasName("PK__BaoVeChu__E322794080103E5D");

            entity.ToTable("BaoVeChuoiNgay");

            entity.Property(e => e.LoaiBaoVe)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.BaoVeChuoiNgays)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("FK_BaoVe_NguoiDung");
        });

        modelBuilder.Entity<CapDo>(entity =>
        {
            entity.HasKey(e => e.MaCapDo).HasName("PK__CapDo__40B881FC3CEFD127");

            entity.ToTable("CapDo");

            entity.Property(e => e.BieuTuong).HasMaxLength(10);
            entity.Property(e => e.MucXptoiDa).HasColumnName("MucXPToiDa");
            entity.Property(e => e.MucXptoiThieu).HasColumnName("MucXPToiThieu");
            entity.Property(e => e.TenCapDo).HasMaxLength(50);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<DanhMucSanPham>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMucS__B375088735D6A87C");

            entity.ToTable("DanhMucSanPham");

            entity.HasIndex(e => e.TenDanhMuc, "UQ__DanhMucS__650CAE4ED4AA50C6").IsUnique();

            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(100);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThuTuHienThi).HasDefaultValue(0);
        });

        modelBuilder.Entity<DiemDanhHangNgay>(entity =>
        {
            entity.HasKey(e => e.MaDiemDanh).HasName("PK__DiemDanh__1512439DBF125F25");

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
            entity.HasKey(e => e.MaKho).HasName("PK__KhoNguoi__3BDA93503742328B");

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
            entity.HasKey(e => e.MaGiaoDich).HasName("PK__LichSuGi__0A2A24EBC7058F8D");

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

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__NguoiDun__C539D762026473AD");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.Email, "IX_NguoiDung_Email");

            entity.HasIndex(e => e.TenDangNhap, "IX_NguoiDung_TenDangNhap");

            entity.HasIndex(e => e.TongDiemXp, "IX_NguoiDung_TongDiemXP").IsDescending();

            entity.HasIndex(e => e.TenDangNhap, "UQ__NguoiDun__55F68FC0DAE64EB2").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D10534A759A215").IsUnique();

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
            entity.HasKey(e => e.MaNhiemVu).HasName("PK__NhiemVu__69582B2F74153D16");

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
            entity.HasKey(e => e.MaThanhTuu).HasName("PK__ThanhTuu__F624200F481AB50A");

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
            entity.HasKey(e => new { e.MaNguoiDung, e.MaThanhTuu }).HasName("PK__ThanhTuu__2A5B95629A71EEF7");

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
            entity.HasKey(e => new { e.MaNguoiDung, e.MaNhiemVu }).HasName("PK__TienDoNh__23AC55D0ECCA8B5B");

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

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVaiTro).HasName("PK__VaiTro__C24C41CFDE3FCE10");

            entity.ToTable("VaiTro");

            entity.HasIndex(e => e.TenVaiTro, "UQ__VaiTro__1DA55814B7634524").IsUnique();

            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenVaiTro).HasMaxLength(50);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<VatPham>(entity =>
        {
            entity.HasKey(e => e.MaVatPham).HasName("PK__VatPham__92DB0A9814149E20");

            entity.ToTable("VatPham");

            entity.HasIndex(e => e.MaDanhMuc, "IX_VatPham_DanhMuc");

            entity.HasIndex(e => e.Gia, "IX_VatPham_Gia");

            entity.Property(e => e.ConHang).HasDefaultValue(true);
            entity.Property(e => e.DoHiem).HasDefaultValue((byte)1);
            entity.Property(e => e.MoTa).HasMaxLength(500);
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

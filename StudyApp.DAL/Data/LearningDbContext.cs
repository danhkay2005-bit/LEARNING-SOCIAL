using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudyApp.DAL.Entities.Learn;

namespace StudyApp.DAL.Data;

public partial class LearningDbContext : DbContext
{
    public LearningDbContext()
    {
    }

    public LearningDbContext(DbContextOptions<LearningDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BoDeHoc> BoDeHocs { get; set; }

    public virtual DbSet<CapGhep> CapGheps { get; set; }

    public virtual DbSet<ChiTietTraLoi> ChiTietTraLois { get; set; }

    public virtual DbSet<ChuDe> ChuDes { get; set; }

    public virtual DbSet<DapAnTracNghiem> DapAnTracNghiems { get; set; }

    public virtual DbSet<LichSuHocBoDe> LichSuHocBoDes { get; set; }

    public virtual DbSet<LogsGenerateAi> LogsGenerateAis { get; set; }

    public virtual DbSet<PhanTuSapXep> PhanTuSapXeps { get; set; }

    public virtual DbSet<PhienHoc> PhienHocs { get; set; }
    public virtual DbSet<LichSuThachDau> LichSuThachDaus { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagBoDe> TagBoDes { get; set; }

    public virtual DbSet<ThachDau> ThachDaus { get; set; }

    public virtual DbSet<ThachDauNguoiChoi> ThachDauNguoiChois { get; set; }

    public virtual DbSet<TheFlashcard> TheFlashcards { get; set; }

    public virtual DbSet<ThuMuc> ThuMucs { get; set; }

    public virtual DbSet<TienDoHocTap> TienDoHocTaps { get; set; }

    public virtual DbSet<TuDienKhuyet> TuDienKhuyets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoDeHoc>(entity =>
        {
            entity.HasKey(e => e.MaBoDe).HasName("PK__BoDeHoc__86C69C248D6D551D");

            entity.ToTable("BoDeHoc");

            entity.Property(e => e.ChoPhepBinhLuan).HasDefaultValue(true);
            entity.Property(e => e.DaXoa).HasDefaultValue(false);
            entity.Property(e => e.LaCongKhai).HasDefaultValue(true);
            entity.Property(e => e.MoTa).HasMaxLength(1000);
            entity.Property(e => e.MucDoKho).HasDefaultValue((byte)1);
            entity.Property(e => e.SoLuongThe).HasDefaultValue(0);
            entity.Property(e => e.SoLuotChiaSe).HasDefaultValue(0);
            entity.Property(e => e.SoLuotHoc).HasDefaultValue(0);
            entity.Property(e => e.ThoiGianCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TieuDe).HasMaxLength(200);

            entity.HasOne(d => d.MaBoDeGocNavigation).WithMany(p => p.InverseMaBoDeGocNavigation)
                .HasForeignKey(d => d.MaBoDeGoc)
                .HasConstraintName("FK_BoDe_Goc");

            entity.HasOne(d => d.MaChuDeNavigation).WithMany(p => p.BoDeHocs)
                .HasForeignKey(d => d.MaChuDe)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_BoDe_ChuDe");

            entity.HasOne(d => d.MaThuMucNavigation).WithMany(p => p.BoDeHocs)
                .HasForeignKey(d => d.MaThuMuc)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_BoDe_ThuMuc");
        });

        modelBuilder.Entity<CapGhep>(entity =>
        {
            entity.HasKey(e => e.MaCap).HasName("PK__CapGhep__3DCA8D353D0DEACC");

            entity.ToTable("CapGhep");

            entity.Property(e => e.ThuTu).HasDefaultValue(0);
            entity.Property(e => e.VePhai).HasMaxLength(200);
            entity.Property(e => e.VeTrai).HasMaxLength(200);

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.CapGheps)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_CapGhep_The");
        });

        modelBuilder.Entity<LichSuThachDau>(entity =>
        {
            entity.HasKey(e => e.MaLichSu).HasName("PK_LichSuThachDau");

            entity.ToTable("LichSuThachDau");

            entity.Property(e => e.Diem).HasDefaultValue(0);
            entity.Property(e => e.SoTheDung).HasDefaultValue(0);
            entity.Property(e => e.SoTheSai).HasDefaultValue(0);
            entity.Property(e => e.ThoiGianLamBaiGiay).HasDefaultValue(0);
            entity.Property(e => e.LaNguoiThang).HasDefaultValue(false);

            entity.Property(e => e.ThoiGianKetThuc)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            // Cấu hình Khóa ngoại với Bộ Đề
            entity.HasOne(d => d.MaBoDeNavigation)
                .WithMany(p => p.LichSuThachDaus) // Đảm bảo đã thêm ICollection này vào class BoDeHoc
                .HasForeignKey(d => d.MaBoDe)
                .OnDelete(DeleteBehavior.Cascade) // Xóa bộ đề thì xóa luôn lịch sử liên quan
                .HasConstraintName("FK_LichSuTD_BoDe");

            // Lưu ý: MaNguoiDung không cấu hình HasOne vì nằm khác Database
        });

        modelBuilder.Entity<ChiTietTraLoi>(entity =>
        {
            entity.HasKey(e => e.MaTraLoi).HasName("PK__ChiTietT__33F7A78D7F9EDFA1");

            entity.ToTable("ChiTietTraLoi");

            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTraLoiGiay).HasDefaultValue(0);

            entity.HasOne(d => d.MaPhienNavigation).WithMany(p => p.ChiTietTraLois)
                .HasForeignKey(d => d.MaPhien)
                .HasConstraintName("FK_CTL_Phien");
        });

        modelBuilder.Entity<ChuDe>(entity =>
        {
            entity.HasKey(e => e.MaChuDe).HasName("PK__ChuDe__358545113C49C5A6");

            entity.ToTable("ChuDe");

            entity.HasIndex(e => e.TenChuDe, "UQ__ChuDe__19B17CFBE8CF1638").IsUnique();

            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.SoLuotDung).HasDefaultValue(0);
            entity.Property(e => e.TenChuDe).HasMaxLength(100);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<DapAnTracNghiem>(entity =>
        {
            entity.HasKey(e => e.MaDapAn).HasName("PK__DapAnTra__6F78E57DA4518E19");

            entity.ToTable("DapAnTracNghiem");

            entity.Property(e => e.LaDapAnDung).HasDefaultValue(false);
            entity.Property(e => e.ThuTu).HasDefaultValue(0);

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.DapAnTracNghiems)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_DapAn_The");
        });

        modelBuilder.Entity<LichSuHocBoDe>(entity =>
        {
            entity.HasKey(e => e.MaLichSu).HasName("PK__LichSuHo__C443222A0CECB319");

            entity.ToTable("LichSuHocBoDe");

            entity.Property(e => e.SoTheHoc).HasDefaultValue(0);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianHocPhut).HasDefaultValue(0);
            entity.Property(e => e.TyLeDung).HasDefaultValue(0.0);

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.LichSuHocBoDes)
                .HasForeignKey(d => d.MaBoDe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LichSu_BoDe");

            entity.HasOne(d => d.MaPhienNavigation).WithMany(p => p.LichSuHocBoDes)
                .HasForeignKey(d => d.MaPhien)
                .HasConstraintName("FK_LichSu_Phien");
        });

        modelBuilder.Entity<LogsGenerateAi>(entity =>
        {
            entity.HasKey(e => e.MaLog).HasName("PK__LogsGene__3B98D24AABFA6384");

            entity.ToTable("LogsGenerateAI");

            entity.Property(e => e.Loi).HasMaxLength(500);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("ThanhCong");

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.LogsGenerateAis)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_LogsAI_The");
        });

        modelBuilder.Entity<PhanTuSapXep>(entity =>
        {
            entity.HasKey(e => e.MaPhanTu).HasName("PK__PhanTuSa__6EEFCD0EC028A096");

            entity.ToTable("PhanTuSapXep");

            entity.Property(e => e.NoiDung).HasMaxLength(200);

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.PhanTuSapXeps)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_SapXep_The");
        });

        modelBuilder.Entity<PhienHoc>(entity =>
        {
            entity.HasKey(e => e.MaPhien).HasName("PK__PhienHoc__2660BFEF29BC62C9");

            entity.ToTable("PhienHoc");

            entity.Property(e => e.LoaiPhien)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("HocMoi");
            entity.Property(e => e.SoTheDung).HasDefaultValue(0);
            entity.Property(e => e.SoTheMoi).HasDefaultValue(0);
            entity.Property(e => e.SoTheOnTap).HasDefaultValue(0);
            entity.Property(e => e.SoTheSai).HasDefaultValue(0);
            entity.Property(e => e.ThoiGianBatDau)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianHocGiay).HasDefaultValue(0);
            entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");
            entity.Property(e => e.TongSoThe).HasDefaultValue(0);
            entity.Property(e => e.TyLeDung).HasDefaultValue(0.0);

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.PhienHocs)
                .HasForeignKey(d => d.MaBoDe)
                .HasConstraintName("FK_PhienHoc_BoDe");

            entity.HasOne(d => d.MaThachDauNavigation).WithMany(p => p.PhienHocs)
                .HasForeignKey(d => d.MaThachDau)
                .HasConstraintName("FK_PhienHoc_ThachDau");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.MaTag).HasName("PK__Tag__314EC214BECCEE2B");

            entity.ToTable("Tag");

            entity.HasIndex(e => e.TenTag, "UQ__Tag__CD4F893E04AE1748").IsUnique();

            entity.Property(e => e.SoLuotDung).HasDefaultValue(0);
            entity.Property(e => e.TenTag).HasMaxLength(50);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TagBoDe>(entity =>
        {
            entity.HasKey(e => new { e.MaBoDe, e.MaTag }).HasName("PK__TagBoDe__D5D27005A470EFF9");

            entity.ToTable("TagBoDe");

            entity.Property(e => e.ThoiGianThem)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.TagBoDes)
                .HasForeignKey(d => d.MaBoDe)
                .HasConstraintName("FK_TagBoDe_BoDe");

            entity.HasOne(d => d.MaTagNavigation).WithMany(p => p.TagBoDes)
                .HasForeignKey(d => d.MaTag)
                .HasConstraintName("FK_TagBoDe_Tag");
        });

        modelBuilder.Entity<ThachDau>(entity =>
        {
            entity.HasKey(e => e.MaThachDau).HasName("PK__ThachDau__EE11E59184EC6E15");

            entity.ToTable("ThachDau");

            entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("ChoNguoiChoi");

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.ThachDaus)
                .HasForeignKey(d => d.MaBoDe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThachDau_BoDe");
        });

        modelBuilder.Entity<ThachDauNguoiChoi>(entity =>
        {
            entity.HasKey(e => new { e.MaThachDau, e.MaNguoiDung }).HasName("PK__ThachDau__D24278E77C2D1BBE");

            entity.ToTable("ThachDauNguoiChoi");

            entity.Property(e => e.Diem).HasDefaultValue(0);
            entity.Property(e => e.LaNguoiThang).HasDefaultValue(false);
            entity.Property(e => e.SoTheDung).HasDefaultValue(0);
            entity.Property(e => e.SoTheSai).HasDefaultValue(0);
            entity.Property(e => e.ThoiGianLamBaiGiay).HasDefaultValue(0);

            entity.HasOne(d => d.MaThachDauNavigation).WithMany(p => p.ThachDauNguoiChois)
                .HasForeignKey(d => d.MaThachDau)
                .HasConstraintName("FK_TDN_ThachDau");
        });

        modelBuilder.Entity<TheFlashcard>(entity =>
        {
            entity.HasKey(e => e.MaThe).HasName("PK__TheFlash__314EEAAF2605BA93");

            entity.ToTable("TheFlashcard");

            entity.Property(e => e.DoKho).HasDefaultValue((byte)3);
            entity.Property(e => e.LoaiThe)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("CoBan");
            entity.Property(e => e.SoLanDung).HasDefaultValue(0);
            entity.Property(e => e.SoLanSai).HasDefaultValue(0);
            entity.Property(e => e.SoLuongHoc).HasDefaultValue(0);
            entity.Property(e => e.ThoiGianCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThuTu).HasDefaultValue(0);

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.TheFlashcards)
                .HasForeignKey(d => d.MaBoDe)
                .HasConstraintName("FK_The_BoDe");
        });

        modelBuilder.Entity<ThuMuc>(entity =>
        {
            entity.HasKey(e => e.MaThuMuc).HasName("PK__ThuMuc__3BE7F0B1144C89BE");

            entity.ToTable("ThuMuc");

            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenThuMuc).HasMaxLength(100);
            entity.Property(e => e.ThoiGianCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThuTu).HasDefaultValue(0);

            entity.HasOne(d => d.MaThuMucChaNavigation).WithMany(p => p.InverseMaThuMucChaNavigation)
                .HasForeignKey(d => d.MaThuMucCha)
                .HasConstraintName("FK_ThuMuc_Cha");
        });

        modelBuilder.Entity<TienDoHocTap>(entity =>
        {
            entity.HasKey(e => e.MaTienDo).HasName("PK__TienDoHo__C5D04CAE586BCF91");

            entity.ToTable("TienDoHocTap");

            entity.HasIndex(e => new { e.MaNguoiDung, e.MaThe }, "UQ_TienDo").IsUnique();

            entity.Property(e => e.HeSoDe).HasDefaultValue(2.5);
            entity.Property(e => e.KhoangCachNgay).HasDefaultValue(0);
            entity.Property(e => e.LanHocCuoi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NgayOnTapTiepTheo).HasColumnType("datetime");
            entity.Property(e => e.SoLanDung).HasDefaultValue(0);
            entity.Property(e => e.SoLanLap).HasDefaultValue(0);
            entity.Property(e => e.SoLanSai).HasDefaultValue(0);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasDefaultValue((byte)0);

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.TienDoHocTaps)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_TienDo_The");
        });

        modelBuilder.Entity<TuDienKhuyet>(entity =>
        {
            entity.HasKey(e => e.MaTuDienKhuyet).HasName("PK__TuDienKh__32677EA45AAE19E8");

            entity.ToTable("TuDienKhuyet");

            entity.Property(e => e.TuCanDien).HasMaxLength(100);

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.TuDienKhuyets)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_DienKhuyet_The");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

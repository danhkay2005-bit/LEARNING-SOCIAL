using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudyApp.DAL.Entities.Learning;

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

    public virtual DbSet<BaoCaoBoDe> BaoCaoBoDes { get; set; }

    public virtual DbSet<BinhLuanBoDe> BinhLuanBoDes { get; set; }

    public virtual DbSet<BoDeHoc> BoDeHocs { get; set; }

    public virtual DbSet<BoDeYeuThich> BoDeYeuThiches { get; set; }

    public virtual DbSet<CapGhep> CapGheps { get; set; }

    public virtual DbSet<CheDoHocCaNhan> CheDoHocCaNhans { get; set; }

    public virtual DbSet<ChiTietTraLoi> ChiTietTraLois { get; set; }

    public virtual DbSet<ChuDe> ChuDes { get; set; }

    public virtual DbSet<DanhDauThe> DanhDauThes { get; set; }

    public virtual DbSet<DanhGiaBoDe> DanhGiaBoDes { get; set; }

    public virtual DbSet<DapAnTracNghiem> DapAnTracNghiems { get; set; }

    public virtual DbSet<GhiChuThe> GhiChuThes { get; set; }

    public virtual DbSet<LichSuClone> LichSuClones { get; set; }

    public virtual DbSet<LichSuHocBoDe> LichSuHocBoDes { get; set; }

    public virtual DbSet<MucTieuCaNhan> MucTieuCaNhans { get; set; }

    public virtual DbSet<PhanTuSapXep> PhanTuSapXeps { get; set; }

    public virtual DbSet<PhienHoc> PhienHocs { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagBoDe> TagBoDes { get; set; }

    public virtual DbSet<TheFlashcard> TheFlashcards { get; set; }

    public virtual DbSet<ThichBinhLuanBoDe> ThichBinhLuanBoDes { get; set; }

    public virtual DbSet<ThongKeNgay> ThongKeNgays { get; set; }

    public virtual DbSet<ThuMuc> ThuMucs { get; set; }

    public virtual DbSet<TienDoHocTap> TienDoHocTaps { get; set; }

    public virtual DbSet<TuDienKhuyet> TuDienKhuyets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LearningDb"].ConnectionString;
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaoCaoBoDe>(entity =>
        {
            entity.HasKey(e => e.MaBaoCao).HasName("PK__BaoCaoBo__25A9188C1817EE38");

            entity.ToTable("BaoCaoBoDe");

            entity.Property(e => e.LyDo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MoTaChiTiet).HasMaxLength(500);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("ChoDuyet");

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.BaoCaoBoDes)
                .HasForeignKey(d => d.MaBoDe)
                .HasConstraintName("FK_BaoCao_BoDe");
        });

        modelBuilder.Entity<BinhLuanBoDe>(entity =>
        {
            entity.HasKey(e => e.MaBinhLuan).HasName("PK__BinhLuan__87CB66A078961BBC");

            entity.ToTable("BinhLuanBoDe");

            entity.HasIndex(e => e.MaBoDe, "IX_BinhLuanBD_BoDe");

            entity.Property(e => e.DaChinhSua).HasDefaultValue(false);
            entity.Property(e => e.DaXoa).HasDefaultValue(false);
            entity.Property(e => e.NoiDung).HasMaxLength(2000);
            entity.Property(e => e.SoLuotThich).HasDefaultValue(0);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianSua).HasColumnType("datetime");

            entity.HasOne(d => d.MaBinhLuanChaNavigation).WithMany(p => p.InverseMaBinhLuanChaNavigation)
                .HasForeignKey(d => d.MaBinhLuanCha)
                .HasConstraintName("FK_BinhLuanBD_Cha");

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.BinhLuanBoDes)
                .HasForeignKey(d => d.MaBoDe)
                .HasConstraintName("FK_BinhLuanBD_BoDe");
        });

        modelBuilder.Entity<BoDeHoc>(entity =>
        {
            entity.HasKey(e => e.MaBoDe).HasName("PK__BoDeHoc__86C69C2430228D0A");

            entity.ToTable("BoDeHoc");

            entity.HasIndex(e => e.MaChuDe, "IX_BoDe_ChuDe");

            entity.HasIndex(e => e.LaCongKhai, "IX_BoDe_CongKhai");

            entity.HasIndex(e => e.MaNguoiDung, "IX_BoDe_NguoiDung");

            entity.Property(e => e.ChoPhepBinhLuan).HasDefaultValue(true);
            entity.Property(e => e.DaXoa).HasDefaultValue(false);
            entity.Property(e => e.DiemDanhGiaTb)
                .HasDefaultValue(0.0)
                .HasColumnName("DiemDanhGiaTB");
            entity.Property(e => e.LaCongKhai).HasDefaultValue(true);
            entity.Property(e => e.MoTa).HasMaxLength(1000);
            entity.Property(e => e.MucDoKho).HasDefaultValue((byte)1);
            entity.Property(e => e.SoDanhGia).HasDefaultValue(0);
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
                .HasConstraintName("FK_BoDe_BoDeGoc");

            entity.HasOne(d => d.MaChuDeNavigation).WithMany(p => p.BoDeHocs)
                .HasForeignKey(d => d.MaChuDe)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_BoDe_ChuDe");

            entity.HasOne(d => d.MaThuMucNavigation).WithMany(p => p.BoDeHocs)
                .HasForeignKey(d => d.MaThuMuc)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_BoDe_ThuMuc");
        });

        modelBuilder.Entity<BoDeYeuThich>(entity =>
        {
            entity.HasKey(e => new { e.MaNguoiDung, e.MaBoDe }).HasName("PK__BoDeYeuT__9D55BEA0DEFF86D6");

            entity.ToTable("BoDeYeuThich");

            entity.HasIndex(e => e.MaNguoiDung, "IX_YeuThich_NguoiDung");

            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.ThoiGianLuu)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.BoDeYeuThiches)
                .HasForeignKey(d => d.MaBoDe)
                .HasConstraintName("FK_YeuThich_BoDe");
        });

        modelBuilder.Entity<CapGhep>(entity =>
        {
            entity.HasKey(e => e.MaCap).HasName("PK__CapGhep__3DCA8D3597DDF0FA");

            entity.ToTable("CapGhep");

            entity.Property(e => e.ThuTu).HasDefaultValue(0);
            entity.Property(e => e.VePhai).HasMaxLength(200);
            entity.Property(e => e.VeTrai).HasMaxLength(200);

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.CapGheps)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_CapGhep_The");
        });

        modelBuilder.Entity<CheDoHocCaNhan>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__CheDoHoc__C539D762DB00867B");

            entity.ToTable("CheDoHocCaNhan");

            entity.Property(e => e.MaNguoiDung).ValueGeneratedNever();
            entity.Property(e => e.BatAmThanh).HasDefaultValue(true);
            entity.Property(e => e.HienGiaiThich).HasDefaultValue(true);
            entity.Property(e => e.HienGoiY).HasDefaultValue(true);
            entity.Property(e => e.HienThongKeSauPhien).HasDefaultValue(true);
            entity.Property(e => e.SoTheMoiMoiNgay).HasDefaultValue(20);
            entity.Property(e => e.SoTheOnTapToiDa).HasDefaultValue(100);
            entity.Property(e => e.ThoiGianCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianHienCauHoi).HasDefaultValue(0);
            entity.Property(e => e.ThoiGianHienDapAn).HasDefaultValue(3);
            entity.Property(e => e.ThoiGianMoiTheToiDa).HasDefaultValue(60);
            entity.Property(e => e.ThuTuHoc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("TuDong");
            entity.Property(e => e.TronDapAnTracNghiem).HasDefaultValue(true);
            entity.Property(e => e.TuDongPhatAm).HasDefaultValue(false);
            entity.Property(e => e.UuTienTheKho).HasDefaultValue(true);
            entity.Property(e => e.UuTienTheSapHetHan).HasDefaultValue(true);
        });

        modelBuilder.Entity<ChiTietTraLoi>(entity =>
        {
            entity.HasKey(e => e.MaTraLoi).HasName("PK__ChiTietT__33F7A78DF7F2BFAE");

            entity.ToTable("ChiTietTraLoi");

            entity.HasIndex(e => e.MaPhien, "IX_ChiTiet_Phien");

            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTraLoiGiay).HasDefaultValue(0);

            entity.HasOne(d => d.MaPhienNavigation).WithMany(p => p.ChiTietTraLois)
                .HasForeignKey(d => d.MaPhien)
                .HasConstraintName("FK_ChiTiet_Phien");

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.ChiTietTraLois)
                .HasForeignKey(d => d.MaThe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTiet_The");
        });

        modelBuilder.Entity<ChuDe>(entity =>
        {
            entity.HasKey(e => e.MaChuDe).HasName("PK__ChuDe__3585451193B0A011");

            entity.ToTable("ChuDe");

            entity.HasIndex(e => e.TenChuDe, "UQ__ChuDe__19B17CFB18CB354B").IsUnique();

            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.SoLuotDung).HasDefaultValue(0);
            entity.Property(e => e.TenChuDe).HasMaxLength(100);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<DanhDauThe>(entity =>
        {
            entity.HasKey(e => e.MaDanhDau).HasName("PK__DanhDauT__99B69AC9B8576C3D");

            entity.ToTable("DanhDauThe");

            entity.HasIndex(e => e.MaNguoiDung, "IX_DanhDau_NguoiDung");

            entity.HasIndex(e => new { e.MaNguoiDung, e.MaThe, e.LoaiDanhDau }, "UQ_DanhDau").IsUnique();

            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.LoaiDanhDau)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("YeuThich");
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.DanhDauThes)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_DanhDau_The");
        });

        modelBuilder.Entity<DanhGiaBoDe>(entity =>
        {
            entity.HasKey(e => e.MaDanhGia).HasName("PK__DanhGiaB__AA9515BFE6A52925");

            entity.ToTable("DanhGiaBoDe");

            entity.HasIndex(e => e.MaBoDe, "IX_DanhGia_BoDe");

            entity.HasIndex(e => new { e.MaBoDe, e.MaNguoiDung }, "UQ_DanhGia").IsUnique();

            entity.Property(e => e.NoiDung).HasMaxLength(1000);
            entity.Property(e => e.SoLuotThich).HasDefaultValue(0);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.DanhGiaBoDes)
                .HasForeignKey(d => d.MaBoDe)
                .HasConstraintName("FK_DanhGia_BoDe");
        });

        modelBuilder.Entity<DapAnTracNghiem>(entity =>
        {
            entity.HasKey(e => e.MaDapAn).HasName("PK__DapAnTra__6F78E57DB312DEE6");

            entity.ToTable("DapAnTracNghiem");

            entity.HasIndex(e => e.MaThe, "IX_DapAn_The");

            entity.Property(e => e.GiaiThich).HasMaxLength(500);
            entity.Property(e => e.LaDapAnDung).HasDefaultValue(false);
            entity.Property(e => e.ThuTu).HasDefaultValue(0);

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.DapAnTracNghiems)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_DapAn_The");
        });

        modelBuilder.Entity<GhiChuThe>(entity =>
        {
            entity.HasKey(e => e.MaGhiChu).HasName("PK__GhiChuTh__097741773A30F4EF");

            entity.ToTable("GhiChuThe");

            entity.HasIndex(e => new { e.MaNguoiDung, e.MaThe }, "UQ_GhiChu").IsUnique();

            entity.Property(e => e.MauNen)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ThoiGianSua)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.GhiChuThes)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_GhiChu_The");
        });

        modelBuilder.Entity<LichSuClone>(entity =>
        {
            entity.HasKey(e => e.MaClone).HasName("PK__LichSuCl__4562F75A8C0211AB");

            entity.ToTable("LichSuClone");

            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBoDeCloneNavigation).WithMany(p => p.LichSuCloneMaBoDeCloneNavigations)
                .HasForeignKey(d => d.MaBoDeClone)
                .HasConstraintName("FK_Clone_BoDeClone");

            entity.HasOne(d => d.MaBoDeGocNavigation).WithMany(p => p.LichSuCloneMaBoDeGocNavigations)
                .HasForeignKey(d => d.MaBoDeGoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clone_BoDeGoc");
        });

        modelBuilder.Entity<LichSuHocBoDe>(entity =>
        {
            entity.HasKey(e => e.MaLichSu).HasName("PK__LichSuHo__C443222AB9B79FE8");

            entity.ToTable("LichSuHocBoDe");

            entity.HasIndex(e => e.MaBoDe, "IX_LichSuHoc_BoDe");

            entity.HasIndex(e => e.MaNguoiDung, "IX_LichSuHoc_NguoiDung");

            entity.Property(e => e.SoTheHoc).HasDefaultValue(0);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianHocPhut).HasDefaultValue(0);
            entity.Property(e => e.TyLeDung).HasDefaultValue(0.0);

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.LichSuHocBoDes)
                .HasForeignKey(d => d.MaBoDe)
                .HasConstraintName("FK_LichSuHoc_BoDe");

            entity.HasOne(d => d.MaPhienNavigation).WithMany(p => p.LichSuHocBoDes)
                .HasForeignKey(d => d.MaPhien)
                .HasConstraintName("FK_LichSuHoc_Phien");
        });

        modelBuilder.Entity<MucTieuCaNhan>(entity =>
        {
            entity.HasKey(e => e.MaMucTieu).HasName("PK__MucTieuC__E587A32986B1F8AE");

            entity.ToTable("MucTieuCaNhan");

            entity.HasIndex(e => e.MaNguoiDung, "IX_MucTieu_NguoiDung");

            entity.Property(e => e.DaHoanThanh).HasDefaultValue(false);
            entity.Property(e => e.DonVi).HasMaxLength(20);
            entity.Property(e => e.GiaTriHienTai).HasDefaultValue(0);
            entity.Property(e => e.LoaiMucTieu)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NgayBatDau).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.TenMucTieu).HasMaxLength(100);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<PhanTuSapXep>(entity =>
        {
            entity.HasKey(e => e.MaPhanTu).HasName("PK__PhanTuSa__6EEFCD0E0A54C04F");

            entity.ToTable("PhanTuSapXep");

            entity.Property(e => e.NoiDung).HasMaxLength(200);

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.PhanTuSapXeps)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_SapXep_The");
        });

        modelBuilder.Entity<PhienHoc>(entity =>
        {
            entity.HasKey(e => e.MaPhien).HasName("PK__PhienHoc__2660BFEFBDB4FA86");

            entity.ToTable("PhienHoc");

            entity.HasIndex(e => e.MaBoDe, "IX_Phien_BoDe");

            entity.HasIndex(e => e.MaNguoiDung, "IX_Phien_NguoiDung");

            entity.HasIndex(e => e.ThoiGianBatDau, "IX_Phien_ThoiGian");

            entity.Property(e => e.DiemDat).HasDefaultValue(0);
            entity.Property(e => e.DiemToiDa).HasDefaultValue(0);
            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.LoaiPhien)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("HocMoi");
            entity.Property(e => e.SoTheBo).HasDefaultValue(0);
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
            entity.Property(e => e.VangNhan).HasDefaultValue(0);
            entity.Property(e => e.Xpnhan)
                .HasDefaultValue(0)
                .HasColumnName("XPNhan");

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.PhienHocs)
                .HasForeignKey(d => d.MaBoDe)
                .HasConstraintName("FK_Phien_BoDe");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.MaTag).HasName("PK__Tag__314EC21487CDA918");

            entity.ToTable("Tag");

            entity.HasIndex(e => e.TenTag, "UQ__Tag__CD4F893E7078A825").IsUnique();

            entity.Property(e => e.SoLuotDung).HasDefaultValue(0);
            entity.Property(e => e.TenTag).HasMaxLength(50);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TagBoDe>(entity =>
        {
            entity.HasKey(e => new { e.MaBoDe, e.MaTag }).HasName("PK__TagBoDe__D5D270057E55FC9D");

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

        modelBuilder.Entity<TheFlashcard>(entity =>
        {
            entity.HasKey(e => e.MaThe).HasName("PK__TheFlash__314EEAAF00FA0E3B");

            entity.ToTable("TheFlashcard");

            entity.HasIndex(e => e.MaBoDe, "IX_The_BoDe");

            entity.HasIndex(e => e.LoaiThe, "IX_The_LoaiThe");

            entity.Property(e => e.DoKho).HasDefaultValue((byte)3);
            entity.Property(e => e.GoiY).HasMaxLength(500);
            entity.Property(e => e.LoaiThe)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("CoBan");
            entity.Property(e => e.SoLanDung).HasDefaultValue(0);
            entity.Property(e => e.SoLanSai).HasDefaultValue(0);
            entity.Property(e => e.SoLuotHoc).HasDefaultValue(0);
            entity.Property(e => e.ThoiGianCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThuTu).HasDefaultValue(0);
            entity.Property(e => e.TyLeDungTb)
                .HasDefaultValue(0.0)
                .HasColumnName("TyLeDungTB");
            entity.Property(e => e.VietTat).HasMaxLength(100);

            entity.HasOne(d => d.MaBoDeNavigation).WithMany(p => p.TheFlashcards)
                .HasForeignKey(d => d.MaBoDe)
                .HasConstraintName("FK_The_BoDe");
        });

        modelBuilder.Entity<ThichBinhLuanBoDe>(entity =>
        {
            entity.HasKey(e => new { e.MaNguoiDung, e.MaBinhLuan }).HasName("PK__ThichBin__DD456108617815B8");

            entity.ToTable("ThichBinhLuanBoDe");

            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBinhLuanNavigation).WithMany(p => p.ThichBinhLuanBoDes)
                .HasForeignKey(d => d.MaBinhLuan)
                .HasConstraintName("FK_ThichBL_BinhLuan");
        });

        modelBuilder.Entity<ThongKeNgay>(entity =>
        {
            entity.HasKey(e => e.MaThongKe).HasName("PK__ThongKeN__60E521F4CA77FA0C");

            entity.ToTable("ThongKeNgay");

            entity.HasIndex(e => e.NgayHoc, "IX_ThongKeNgay_Ngay");

            entity.HasIndex(e => e.MaNguoiDung, "IX_ThongKeNgay_NguoiDung");

            entity.HasIndex(e => new { e.MaNguoiDung, e.NgayHoc }, "UQ_ThongKeNgay").IsUnique();

            entity.Property(e => e.DaHoanThanhMucTieu).HasDefaultValue(false);
            entity.Property(e => e.SoBoDeHoc).HasDefaultValue(0);
            entity.Property(e => e.SoPhienHoc).HasDefaultValue(0);
            entity.Property(e => e.SoTheDung).HasDefaultValue(0);
            entity.Property(e => e.SoTheMoi).HasDefaultValue(0);
            entity.Property(e => e.SoTheOnTap).HasDefaultValue(0);
            entity.Property(e => e.SoTheSai).HasDefaultValue(0);
            entity.Property(e => e.ThoiGianHocPhut).HasDefaultValue(0);
            entity.Property(e => e.TongTheHoc).HasDefaultValue(0);
            entity.Property(e => e.TyLeDung).HasDefaultValue(0.0);
            entity.Property(e => e.VangNhan).HasDefaultValue(0);
            entity.Property(e => e.Xpnhan)
                .HasDefaultValue(0)
                .HasColumnName("XPNhan");
        });

        modelBuilder.Entity<ThuMuc>(entity =>
        {
            entity.HasKey(e => e.MaThuMuc).HasName("PK__ThuMuc__3BE7F0B1F54E4D11");

            entity.ToTable("ThuMuc");

            entity.HasIndex(e => e.MaThuMucCha, "IX_ThuMuc_Cha");

            entity.HasIndex(e => e.MaNguoiDung, "IX_ThuMuc_NguoiDung");

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
            entity.HasKey(e => e.MaTienDo).HasName("PK__TienDoHo__C5D04CAE49E1BA59");

            entity.ToTable("TienDoHocTap");

            entity.HasIndex(e => e.NgayOnTapTiepTheo, "IX_TienDo_NgayOnTap");

            entity.HasIndex(e => e.MaNguoiDung, "IX_TienDo_NguoiDung");

            entity.HasIndex(e => e.TrangThai, "IX_TienDo_TrangThai");

            entity.HasIndex(e => new { e.MaNguoiDung, e.MaThe }, "UQ_TienDo").IsUnique();

            entity.Property(e => e.DoKhoCanNhan).HasDefaultValue((byte)3);
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
            entity.Property(e => e.ThoiGianTraLoiTbgiay)
                .HasDefaultValue(0)
                .HasColumnName("ThoiGianTraLoiTBGiay");
            entity.Property(e => e.TrangThai).HasDefaultValue((byte)0);
            entity.Property(e => e.TyLeDung).HasDefaultValue(0.0);

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.TienDoHocTaps)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_TienDo_The");
        });

        modelBuilder.Entity<TuDienKhuyet>(entity =>
        {
            entity.HasKey(e => e.MaTuDienKhuyet).HasName("PK__TuDienKh__32677EA4EB25B1B1");

            entity.ToTable("TuDienKhuyet");

            entity.Property(e => e.GoiY).HasMaxLength(100);
            entity.Property(e => e.TuCanDien).HasMaxLength(100);

            entity.HasOne(d => d.MaTheNavigation).WithMany(p => p.TuDienKhuyets)
                .HasForeignKey(d => d.MaThe)
                .HasConstraintName("FK_DienKhuyet_The");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

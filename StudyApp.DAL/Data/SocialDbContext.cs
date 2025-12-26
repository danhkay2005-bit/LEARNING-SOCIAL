using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudyApp.DAL.Entities.Social;

namespace StudyApp.DAL.Data;

public partial class SocialDbContext : DbContext
{
    public SocialDbContext()
    {
    }

    public SocialDbContext(DbContextOptions<SocialDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaiDang> BaiDangs { get; set; }

    public virtual DbSet<BanBe> BanBes { get; set; }

    public virtual DbSet<BinhLuanBaiDang> BinhLuanBaiDangs { get; set; }

    public virtual DbSet<ChanNguoiDung> ChanNguoiDungs { get; set; }

    public virtual DbSet<ChiaSeBaiDang> ChiaSeBaiDangs { get; set; }

    public virtual DbSet<CuocTroChuyen> CuocTroChuyens { get; set; }

    public virtual DbSet<DaXemTinNhan> DaXemTinNhans { get; set; }

    public virtual DbSet<Hashtag> Hashtags { get; set; }

    public virtual DbSet<MentionBaiDang> MentionBaiDangs { get; set; }

    public virtual DbSet<MentionBinhLuan> MentionBinhLuans { get; set; }

    public virtual DbSet<ReactionBaiDang> ReactionBaiDangs { get; set; }

    public virtual DbSet<ReactionTinNhan> ReactionTinNhans { get; set; }

    public virtual DbSet<ThanhVienCuocTroChuyen> ThanhVienCuocTroChuyens { get; set; }

    public virtual DbSet<TheoDoi> TheoDois { get; set; }

    public virtual DbSet<ThichBinhLuan> ThichBinhLuans { get; set; }

    public virtual DbSet<TinNhan> TinNhans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SocialDb"].ConnectionString;
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaiDang>(entity =>
        {
            entity.HasKey(e => e.MaBaiDang).HasName("PK__BaiDang__BF5D50C5EA768A8B");

            entity.ToTable("BaiDang");

            entity.HasIndex(e => new { e.QuyenRiengTu, e.DaXoa }, "IX_BaiDang_CongKhai");

            entity.HasIndex(e => e.LoaiBaiDang, "IX_BaiDang_LoaiBaiDang");

            entity.HasIndex(e => e.MaNguoiDung, "IX_BaiDang_NguoiDung");

            entity.HasIndex(e => e.ThoiGianTao, "IX_BaiDang_ThoiGian").IsDescending();

            entity.Property(e => e.DaChinhSua).HasDefaultValue(false);
            entity.Property(e => e.DaXoa).HasDefaultValue(false);
            entity.Property(e => e.GhimBaiDang).HasDefaultValue(false);
            entity.Property(e => e.LoaiBaiDang)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("VanBan");
            entity.Property(e => e.QuyenRiengTu).HasDefaultValue((byte)1);
            entity.Property(e => e.SoBinhLuan).HasDefaultValue(0);
            entity.Property(e => e.SoLuotChiaSe).HasDefaultValue(0);
            entity.Property(e => e.SoLuotXem).HasDefaultValue(0);
            entity.Property(e => e.SoReaction).HasDefaultValue(0);
            entity.Property(e => e.TatBinhLuan).HasDefaultValue(false);
            entity.Property(e => e.ThoiGianSua).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasMany(d => d.MaHashtags).WithMany(p => p.MaBaiDangs)
                .UsingEntity<Dictionary<string, object>>(
                    "HashtagBaiDang",
                    r => r.HasOne<Hashtag>().WithMany()
                        .HasForeignKey("MaHashtag")
                        .HasConstraintName("FK_HashtagBD_Hashtag"),
                    l => l.HasOne<BaiDang>().WithMany()
                        .HasForeignKey("MaBaiDang")
                        .HasConstraintName("FK_HashtagBD_BaiDang"),
                    j =>
                    {
                        j.HasKey("MaBaiDang", "MaHashtag").HasName("PK__HashtagB__A9686E99AD164384");
                        j.ToTable("HashtagBaiDang");
                    });
        });

        modelBuilder.Entity<BanBe>(entity =>
        {
            entity.HasKey(e => new { e.MaNguoiDung1, e.MaNguoiDung2 }).HasName("PK__BanBe__F45A77DBF3F68349");

            entity.ToTable("BanBe");

            entity.Property(e => e.ThoiGianChapNhan).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianGui)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("ChoDuyet");
        });

        modelBuilder.Entity<BinhLuanBaiDang>(entity =>
        {
            entity.HasKey(e => e.MaBinhLuan).HasName("PK__BinhLuan__87CB66A0199023F5");

            entity.ToTable("BinhLuanBaiDang");

            entity.HasIndex(e => e.MaBaiDang, "IX_BinhLuan_BaiDang");

            entity.HasIndex(e => e.MaNguoiDung, "IX_BinhLuan_NguoiDung");

            entity.Property(e => e.DaChinhSua).HasDefaultValue(false);
            entity.Property(e => e.DaXoa).HasDefaultValue(false);
            entity.Property(e => e.SoLuotThich).HasDefaultValue(0);
            entity.Property(e => e.ThoiGianSua).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBaiDangNavigation).WithMany(p => p.BinhLuanBaiDangs)
                .HasForeignKey(d => d.MaBaiDang)
                .HasConstraintName("FK_BinhLuan_BaiDang");

            entity.HasOne(d => d.MaBinhLuanChaNavigation).WithMany(p => p.InverseMaBinhLuanChaNavigation)
                .HasForeignKey(d => d.MaBinhLuanCha)
                .HasConstraintName("FK_BinhLuan_Cha");
        });

        modelBuilder.Entity<ChanNguoiDung>(entity =>
        {
            entity.HasKey(e => new { e.MaNguoiChan, e.MaNguoiBiChan }).HasName("PK__ChanNguo__44ADFB2878744761");

            entity.ToTable("ChanNguoiDung");

            entity.Property(e => e.LyDo).HasMaxLength(255);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ChiaSeBaiDang>(entity =>
        {
            entity.HasKey(e => e.MaChiaSe).HasName("PK__ChiaSeBa__54AE718FA7AB2859");

            entity.ToTable("ChiaSeBaiDang");

            entity.Property(e => e.NoiDungThem).HasMaxLength(500);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBaiDangGocNavigation).WithMany(p => p.ChiaSeBaiDangMaBaiDangGocNavigations)
                .HasForeignKey(d => d.MaBaiDangGoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiaSe_BaiDangGoc");

            entity.HasOne(d => d.MaBaiDangMoiNavigation).WithMany(p => p.ChiaSeBaiDangMaBaiDangMoiNavigations)
                .HasForeignKey(d => d.MaBaiDangMoi)
                .HasConstraintName("FK_ChiaSe_BaiDangMoi");
        });

        modelBuilder.Entity<CuocTroChuyen>(entity =>
        {
            entity.HasKey(e => e.MaCuocTroChuyen).HasName("PK__CuocTroC__2E5CA12A40393916");

            entity.ToTable("CuocTroChuyen");

            entity.HasIndex(e => e.ThoiGianTinCuoi, "IX_CuocTro_ThoiGianTin").IsDescending();

            entity.Property(e => e.LoaiCuocTroChuyen)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("CaNhan");
            entity.Property(e => e.NoiDungTinCuoi).HasMaxLength(255);
            entity.Property(e => e.TenNhomChat).HasMaxLength(100);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTinCuoi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<DaXemTinNhan>(entity =>
        {
            entity.HasKey(e => new { e.MaTinNhan, e.MaNguoiXem }).HasName("PK__DaXemTin__4AD0671C91367699");

            entity.ToTable("DaXemTinNhan");

            entity.Property(e => e.ThoiGianXem)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaTinNhanNavigation).WithMany(p => p.DaXemTinNhans)
                .HasForeignKey(d => d.MaTinNhan)
                .HasConstraintName("FK_DaXem_TinNhan");
        });

        modelBuilder.Entity<Hashtag>(entity =>
        {
            entity.HasKey(e => e.MaHashtag).HasName("PK__Hashtag__6353E5C04A3297F8");

            entity.ToTable("Hashtag");

            entity.HasIndex(e => e.TenHashtag, "IX_Hashtag_Ten");

            entity.HasIndex(e => new { e.DangThinhHanh, e.SoLuotDung }, "IX_Hashtag_ThinhHanh").IsDescending(false, true);

            entity.HasIndex(e => e.TenHashtag, "UQ__Hashtag__275D80E65CE54FB7").IsUnique();

            entity.Property(e => e.DangThinhHanh).HasDefaultValue(false);
            entity.Property(e => e.SoLuotDung).HasDefaultValue(0);
            entity.Property(e => e.TenHashtag).HasMaxLength(100);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<MentionBaiDang>(entity =>
        {
            entity.HasKey(e => new { e.MaBaiDang, e.MaNguoiDuocMention }).HasName("PK__MentionB__121549D08E6F3C8D");

            entity.ToTable("MentionBaiDang");

            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBaiDangNavigation).WithMany(p => p.MentionBaiDangs)
                .HasForeignKey(d => d.MaBaiDang)
                .HasConstraintName("FK_Mention_BaiDang");
        });

        modelBuilder.Entity<MentionBinhLuan>(entity =>
        {
            entity.HasKey(e => new { e.MaBinhLuan, e.MaNguoiDuocMention }).HasName("PK__MentionB__2A837FB53B8BF5E2");

            entity.ToTable("MentionBinhLuan");

            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBinhLuanNavigation).WithMany(p => p.MentionBinhLuans)
                .HasForeignKey(d => d.MaBinhLuan)
                .HasConstraintName("FK_MentionBL_BinhLuan");
        });

        modelBuilder.Entity<ReactionBaiDang>(entity =>
        {
            entity.HasKey(e => new { e.MaBaiDang, e.MaNguoiDung }).HasName("PK__Reaction__830ECDB338FE273C");

            entity.ToTable("ReactionBaiDang");

            entity.HasIndex(e => e.MaNguoiDung, "IX_Reaction_NguoiDung");

            entity.Property(e => e.LoaiReaction)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Thich");
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBaiDangNavigation).WithMany(p => p.ReactionBaiDangs)
                .HasForeignKey(d => d.MaBaiDang)
                .HasConstraintName("FK_Reaction_BaiDang");
        });

        modelBuilder.Entity<ReactionTinNhan>(entity =>
        {
            entity.HasKey(e => new { e.MaTinNhan, e.MaNguoiDung }).HasName("PK__Reaction__D9E09B5C4B55B79E");

            entity.ToTable("ReactionTinNhan");

            entity.Property(e => e.Emoji).HasMaxLength(10);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaTinNhanNavigation).WithMany(p => p.ReactionTinNhans)
                .HasForeignKey(d => d.MaTinNhan)
                .HasConstraintName("FK_ReactionTN_TinNhan");
        });

        modelBuilder.Entity<ThanhVienCuocTroChuyen>(entity =>
        {
            entity.HasKey(e => new { e.MaCuocTroChuyen, e.MaNguoiDung }).HasName("PK__ThanhVie__120F3C5CFEAD1DF6");

            entity.ToTable("ThanhVienCuocTroChuyen");

            entity.HasIndex(e => e.MaNguoiDung, "IX_ThanhVienChat_NguoiDung");

            entity.Property(e => e.BiDanh).HasMaxLength(50);
            entity.Property(e => e.DaRoiNhom).HasDefaultValue(false);
            entity.Property(e => e.GhimCuocTro).HasDefaultValue(false);
            entity.Property(e => e.SoTinChuaDoc).HasDefaultValue(0);
            entity.Property(e => e.TatThongBao).HasDefaultValue(false);
            entity.Property(e => e.ThoiGianThamGia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianXemCuoi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VaiTro)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("ThanhVien");

            entity.HasOne(d => d.MaCuocTroChuyenNavigation).WithMany(p => p.ThanhVienCuocTroChuyens)
                .HasForeignKey(d => d.MaCuocTroChuyen)
                .HasConstraintName("FK_ThanhVien_CuocTro");
        });

        modelBuilder.Entity<TheoDoi>(entity =>
        {
            entity.HasKey(e => new { e.MaNguoiTheoDoi, e.MaNguoiDuocTheoDoi }).HasName("PK__TheoDoi__1DBD570D42787C40");

            entity.ToTable("TheoDoi");

            entity.HasIndex(e => e.MaNguoiDuocTheoDoi, "IX_TheoDoi_NguoiDuocTheoDoi");

            entity.HasIndex(e => e.MaNguoiTheoDoi, "IX_TheoDoi_NguoiTheoDoi");

            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThongBaoBaiMoi).HasDefaultValue(true);
        });

        modelBuilder.Entity<ThichBinhLuan>(entity =>
        {
            entity.HasKey(e => new { e.MaBinhLuan, e.MaNguoiDung }).HasName("PK__ThichBin__BB98FBD6276F1EAE");

            entity.ToTable("ThichBinhLuan");

            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBinhLuanNavigation).WithMany(p => p.ThichBinhLuans)
                .HasForeignKey(d => d.MaBinhLuan)
                .HasConstraintName("FK_ThichBL_BinhLuan");
        });

        modelBuilder.Entity<TinNhan>(entity =>
        {
            entity.HasKey(e => e.MaTinNhan).HasName("PK__TinNhan__E5B3062A23030AE7");

            entity.ToTable("TinNhan");

            entity.HasIndex(e => new { e.MaCuocTroChuyen, e.ThoiGianGui }, "IX_TinNhan_CuocTro").IsDescending(false, true);

            entity.HasIndex(e => e.MaNguoiGui, "IX_TinNhan_NguoiGui");

            entity.Property(e => e.DaThuHoi).HasDefaultValue(false);
            entity.Property(e => e.LoaiTinNhan)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("VanBan");
            entity.Property(e => e.TenFile).HasMaxLength(255);
            entity.Property(e => e.ThoiGianGui)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianThuHoi).HasColumnType("datetime");

            entity.HasOne(d => d.MaCuocTroChuyenNavigation).WithMany(p => p.TinNhans)
                .HasForeignKey(d => d.MaCuocTroChuyen)
                .HasConstraintName("FK_TinNhan_CuocTro");

            entity.HasOne(d => d.ReplyTo).WithMany(p => p.InverseReplyTo)
                .HasForeignKey(d => d.ReplyToId)
                .HasConstraintName("FK_TinNhan_Reply");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

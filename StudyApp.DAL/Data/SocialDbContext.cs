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

    public virtual DbSet<BinhLuanBaiDang> BinhLuanBaiDangs { get; set; }

    public virtual DbSet<ChiaSeBaiDang> ChiaSeBaiDangs { get; set; }

    public virtual DbSet<Hashtag> Hashtags { get; set; }

    public virtual DbSet<MentionBaiDang> MentionBaiDangs { get; set; }

    public virtual DbSet<MentionBinhLuan> MentionBinhLuans { get; set; }

    public virtual DbSet<ReactionBaiDang> ReactionBaiDangs { get; set; }

    public virtual DbSet<ReactionBinhLuan> ReactionBinhLuans { get; set; }

    public virtual DbSet<TheoDoiNguoiDung> TheoDoiNguoiDungs { get; set; }

    public virtual DbSet<ThongBao> ThongBaos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaiDang>(entity =>
        {
            entity.HasKey(e => e.MaBaiDang).HasName("PK__BaiDang__BF5D50C52CE22EC7");

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
                        j.HasKey("MaBaiDang", "MaHashtag").HasName("PK__HashtagB__A9686E996B2ADCA0");
                        j.ToTable("HashtagBaiDang");
                    });
        });

        modelBuilder.Entity<BinhLuanBaiDang>(entity =>
        {
            entity.HasKey(e => e.MaBinhLuan).HasName("PK__BinhLuan__87CB66A035B9F53B");

            entity.ToTable("BinhLuanBaiDang");

            entity.HasIndex(e => e.MaBaiDang, "IX_BinhLuan_BaiDang");

            entity.HasIndex(e => e.MaNguoiDung, "IX_BinhLuan_NguoiDung");

            entity.Property(e => e.DaChinhSua).HasDefaultValue(false);
            entity.Property(e => e.DaXoa).HasDefaultValue(false);
            entity.Property(e => e.SoLuotReaction).HasDefaultValue(0);
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

        modelBuilder.Entity<ChiaSeBaiDang>(entity =>
        {
            entity.HasKey(e => e.MaChiaSe).HasName("PK__ChiaSeBa__54AE718F4D1D49F6");

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

        modelBuilder.Entity<Hashtag>(entity =>
        {
            entity.HasKey(e => e.MaHashtag).HasName("PK__Hashtag__6353E5C07543AD5A");

            entity.ToTable("Hashtag");

            entity.HasIndex(e => e.TenHashtag, "IX_Hashtag_Ten");

            entity.HasIndex(e => new { e.DangThinhHanh, e.SoLuotDung }, "IX_Hashtag_ThinhHanh").IsDescending(false, true);

            entity.HasIndex(e => e.TenHashtag, "UQ__Hashtag__275D80E6AF21DC7D").IsUnique();

            entity.Property(e => e.DangThinhHanh).HasDefaultValue(false);
            entity.Property(e => e.SoLuotDung).HasDefaultValue(0);
            entity.Property(e => e.TenHashtag).HasMaxLength(100);
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<MentionBaiDang>(entity =>
        {
            entity.HasKey(e => new { e.MaBaiDang, e.MaNguoiDuocMention }).HasName("PK__MentionB__121549D0AF720B94");

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
            entity.HasKey(e => new { e.MaBinhLuan, e.MaNguoiDuocMention }).HasName("PK__MentionB__2A837FB53EB3A88F");

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
            entity.HasKey(e => new { e.MaBaiDang, e.MaNguoiDung }).HasName("PK__Reaction__830ECDB384603D4A");

            entity.ToTable("ReactionBaiDang");

            entity.HasIndex(e => e.MaNguoiDung, "IX_Reaction_NguoiDung");

            entity.Property(e => e.LoaiReaction)
                .HasMaxLength(50)
                .IsUnicode(true)  // ✅ BẬT UNICODE
                .HasDefaultValue("Thich");
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBaiDangNavigation).WithMany(p => p.ReactionBaiDangs)
                .HasForeignKey(d => d.MaBaiDang)
                .HasConstraintName("FK_Reaction_BaiDang");
        });

        modelBuilder.Entity<ReactionBinhLuan>(entity =>
        {
            entity.HasKey(e => new { e.MaBinhLuan, e.MaNguoiDung }).HasName("PK__Reaction__BB98FBD6ED3D9CED");

            entity.ToTable("ReactionBinhLuan");

            entity.HasIndex(e => e.MaNguoiDung, "IX_ReactionBL_NguoiDung");

            entity.Property(e => e.LoaiReaction)
                .HasMaxLength(50)
                .IsUnicode(true)  // ✅ BẬT UNICODE
                .HasDefaultValue("Thich");
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBinhLuanNavigation).WithMany(p => p.ReactionBinhLuans)
                .HasForeignKey(d => d.MaBinhLuan)
                .HasConstraintName("FK_ReactionBL_BinhLuan");
        });

        modelBuilder.Entity<TheoDoiNguoiDung>(entity =>
        {
            entity.HasKey(e => new { e.MaNguoiTheoDoi, e.MaNguoiDuocTheoDoi }).HasName("PK__TheoDoiN__1DBD570DD0EC919A");

            entity.ToTable("TheoDoiNguoiDung");

            entity.HasIndex(e => e.MaNguoiDuocTheoDoi, "IX_TheoDoi_NguoiDuocTheoDoi");

            entity.HasIndex(e => e.MaNguoiTheoDoi, "IX_TheoDoi_NguoiTheoDoi");

            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ThongBao>(entity =>
        {
            entity.HasKey(e => e.MaThongBao).HasName("PK__ThongBao__04DEB54E554C2F69");

            entity.ToTable("ThongBao");

            entity.HasIndex(e => e.DaDoc, "IX_ThongBao_DaDoc");

            entity.HasIndex(e => e.LoaiThongBao, "IX_ThongBao_LoaiThongBao");

            entity.HasIndex(e => e.MaNguoiNhan, "IX_ThongBao_MaNguoiNhan");

            entity.Property(e => e.NoiDung).HasMaxLength(500);
            entity.Property(e => e.ThoiGian).HasDefaultValueSql("(sysdatetime())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

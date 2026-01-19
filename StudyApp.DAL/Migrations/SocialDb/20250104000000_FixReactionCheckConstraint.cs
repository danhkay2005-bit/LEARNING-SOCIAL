using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyApp.DAL.Migrations.SocialDb
{
    /// <inheritdoc />
    public partial class FixReactionCheckConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ? B??C 1: Drop CHECK constraint c? (n?u có)
            migrationBuilder.Sql(@"
                DECLARE @ConstraintName NVARCHAR(128);
                SELECT @ConstraintName = cc.name
                FROM sys.check_constraints cc
                WHERE OBJECT_NAME(cc.parent_object_id) = 'ReactionBinhLuan'
                  AND COL_NAME(cc.parent_object_id, cc.parent_column_id) = 'LoaiReaction';

                IF @ConstraintName IS NOT NULL
                BEGIN
                    DECLARE @SQL NVARCHAR(500) = 'ALTER TABLE ReactionBinhLuan DROP CONSTRAINT ' + QUOTENAME(@ConstraintName);
                    EXEC sp_executesql @SQL;
                END
            ");

            // ? B??C 2: T?o CHECK constraint m?i v?i giá tr? ?úng
            migrationBuilder.Sql(@"
                ALTER TABLE ReactionBinhLuan
                ADD CONSTRAINT CK_ReactionBinhLuan_LoaiReaction
                CHECK (LoaiReaction IN (N'Thich', N'YeuThich', N'Haha', N'Wow', N'Buon', N'TucGian'));
            ");
            
            // ? B??C 3: Làm t??ng t? cho ReactionBaiDang
            migrationBuilder.Sql(@"
                DECLARE @ConstraintName NVARCHAR(128);
                SELECT @ConstraintName = cc.name
                FROM sys.check_constraints cc
                WHERE OBJECT_NAME(cc.parent_object_id) = 'ReactionBaiDang'
                  AND COL_NAME(cc.parent_object_id, cc.parent_column_id) = 'LoaiReaction';

                IF @ConstraintName IS NOT NULL
                BEGIN
                    DECLARE @SQL NVARCHAR(500) = 'ALTER TABLE ReactionBaiDang DROP CONSTRAINT ' + QUOTENAME(@ConstraintName);
                    EXEC sp_executesql @SQL;
                END
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ReactionBaiDang
                ADD CONSTRAINT CK_ReactionBaiDang_LoaiReaction
                CHECK (LoaiReaction IN (N'Thich', N'YeuThich', N'Haha', N'Wow', N'Buon', N'TucGian'));
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Rollback: Drop constraints ?ã t?o
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_ReactionBinhLuan_LoaiReaction')
                    ALTER TABLE ReactionBinhLuan DROP CONSTRAINT CK_ReactionBinhLuan_LoaiReaction;
            ");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_ReactionBaiDang_LoaiReaction')
                    ALTER TABLE ReactionBaiDang DROP CONSTRAINT CK_ReactionBaiDang_LoaiReaction;
            ");
        }
    }
}

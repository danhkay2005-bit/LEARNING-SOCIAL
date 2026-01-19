using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyApp.DAL.Migrations.SocialDb
{
    /// <inheritdoc />
    public partial class FixReactionUnicode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Fix ReactionBaiDang
            migrationBuilder.AlterColumn<string>(
                name: "LoaiReaction",
                table: "ReactionBaiDang",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldUnicode: false);

            // Fix ReactionBinhLuan
            migrationBuilder.AlterColumn<string>(
                name: "LoaiReaction",
                table: "ReactionBinhLuan",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldUnicode: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Rollback ReactionBaiDang
            migrationBuilder.AlterColumn<string>(
                name: "LoaiReaction",
                table: "ReactionBaiDang",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                unicode: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            // Rollback ReactionBinhLuan
            migrationBuilder.AlterColumn<string>(
                name: "LoaiReaction",
                table: "ReactionBinhLuan",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                unicode: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}

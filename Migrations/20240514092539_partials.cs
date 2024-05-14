using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekty.Migrations
{
    /// <inheritdoc />
    public partial class partials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "Docs",
                type: "xml",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "xml");

            migrationBuilder.AddColumn<string>(
                name: "Part",
                table: "Docs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Part",
                table: "Docs");

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "Docs",
                type: "xml",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "xml",
                oldNullable: true);
        }
    }
}

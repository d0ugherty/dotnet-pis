using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetPIS.Data.Migrations
{
    /// <inheritdoc />
    public partial class SourceBools : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasInfoBoard",
                table: "Sources",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMap",
                table: "Sources",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasNextToArrive",
                table: "Sources",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasInfoBoard",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "HasMap",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "HasNextToArrive",
                table: "Sources");
        }
    }
}

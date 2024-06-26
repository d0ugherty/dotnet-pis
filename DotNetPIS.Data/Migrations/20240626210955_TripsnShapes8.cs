using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetPIS.Data.Migrations
{
    /// <inheritdoc />
    public partial class TripsnShapes8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "TripShapes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TripShapes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}

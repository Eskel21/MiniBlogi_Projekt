using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBlogiv2.Data.Migrations
{
    public partial class poprawki_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Note");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Note",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

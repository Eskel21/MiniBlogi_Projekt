using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBlogiv2.Data.Migrations
{
    public partial class UsersLiked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsersLiked",
                table: "Note",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsersLiked",
                table: "Note");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBlogi_Projekt.Data.Migrations
{
    public partial class User_Note : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Note",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Note",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Note_UserId",
                table: "Note",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_AspNetUsers_UserId",
                table: "Note",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_AspNetUsers_UserId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Note_UserId",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Note");
        }
    }
}

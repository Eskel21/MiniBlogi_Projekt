using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBlogiv2.Data.Migrations
{
    public partial class migracja2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Note",
                type: "varchar(MAX)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Note",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)",
                oldMaxLength: 500);

            
        }
    }
}

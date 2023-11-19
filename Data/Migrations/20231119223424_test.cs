using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBlogi_Projekt.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id_image",
                table: "Image",
                newName: "ImageId");

            migrationBuilder.CreateTable(
                name: "ImageNote",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageNote", x => new { x.ImageId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_ImageNote_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageNote_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageNote_NoteId",
                table: "ImageNote",
                column: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageNote");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Image",
                newName: "Id_image");
        }
    }
}

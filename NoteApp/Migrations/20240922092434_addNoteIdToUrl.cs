using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteApp.Migrations
{
    /// <inheritdoc />
    public partial class addNoteIdToUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orijinal",
                table: "URLs");

            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "URLs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "URLs");

            migrationBuilder.AddColumn<string>(
                name: "Orijinal",
                table: "URLs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

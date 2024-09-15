using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailToken",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailToken",
                table: "Users");
        }
    }
}

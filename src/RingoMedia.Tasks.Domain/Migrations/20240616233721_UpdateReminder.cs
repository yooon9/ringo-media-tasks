using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingoMedia.Tasks.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReminder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Reminders");
        }
    }
}

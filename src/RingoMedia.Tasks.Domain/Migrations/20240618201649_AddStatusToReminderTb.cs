using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingoMedia.Tasks.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToReminderTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reminders");
        }
    }
}

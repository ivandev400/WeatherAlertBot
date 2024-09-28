using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherAlertBot.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "UserSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Users");
        }
    }
}

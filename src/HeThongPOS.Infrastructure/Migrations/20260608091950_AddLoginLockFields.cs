using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeThongPOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginLockFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailedLoginAttempts",
                table: "NHAN_VIEN",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "NHAN_VIEN",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedLoginAttempts",
                table: "NHAN_VIEN");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "NHAN_VIEN");
        }
    }
}

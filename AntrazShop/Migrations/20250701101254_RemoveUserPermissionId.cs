using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntrazShop.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserPermissionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserPermissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserPermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

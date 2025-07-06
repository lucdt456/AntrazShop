using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntrazShop.Migrations
{
    /// <inheritdoc />
    public partial class EditRolePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "RolePermissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

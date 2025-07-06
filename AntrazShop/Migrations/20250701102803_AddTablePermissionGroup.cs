using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntrazShop.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePermissionGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PermissionGroupId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PermissionGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionGroupId",
                table: "Permissions",
                column: "PermissionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_PermissionGroups_PermissionGroupId",
                table: "Permissions",
                column: "PermissionGroupId",
                principalTable: "PermissionGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_PermissionGroups_PermissionGroupId",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "PermissionGroups");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_PermissionGroupId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "PermissionGroupId",
                table: "Permissions");
        }
    }
}

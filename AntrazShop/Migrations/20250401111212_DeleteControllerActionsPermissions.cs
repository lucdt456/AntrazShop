using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntrazShop.Migrations
{
    /// <inheritdoc />
    public partial class DeleteControllerActionsPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControllerActionsPermissions");

            migrationBuilder.RenameColumn(
                name: "avatar",
                table: "Users",
                newName: "Avatar");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Products",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "Users",
                newName: "avatar");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Products",
                newName: "status");

            migrationBuilder.CreateTable(
                name: "ControllerActionsPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControllerActionsPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControllerActionsPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControllerActionsPermissions_PermissionId",
                table: "ControllerActionsPermissions",
                column: "PermissionId");
        }
    }
}

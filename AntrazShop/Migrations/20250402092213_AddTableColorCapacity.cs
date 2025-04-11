using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntrazShop.Migrations
{
    /// <inheritdoc />
    public partial class AddTableColorCapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Colors_ColorId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Capacities_CapacityId",
                table: "Colors");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Colors_ColorId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Colors_ColorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Colors_ColorId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_Colors_CapacityId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "CapacityId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Colors");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "WishLists",
                newName: "ColorCapacityId");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_ColorId",
                table: "WishLists",
                newName: "IX_WishLists_ColorCapacityId");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "Reviews",
                newName: "ColorCapacityId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ColorId",
                table: "Reviews",
                newName: "IX_Reviews_ColorCapacityId");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "OrderDetails",
                newName: "ColorCapacityId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ColorId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ColorCapacityId");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "Carts",
                newName: "ColorCapacityId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ColorId",
                table: "Carts",
                newName: "IX_Carts_ColorCapacityId");

            migrationBuilder.CreateTable(
                name: "ColorCapacities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    CapacityId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorCapacities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColorCapacities_Capacities_CapacityId",
                        column: x => x.CapacityId,
                        principalTable: "Capacities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorCapacities_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorCapacities_CapacityId",
                table: "ColorCapacities",
                column: "CapacityId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorCapacities_ColorId",
                table: "ColorCapacities",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ColorCapacities_ColorCapacityId",
                table: "Carts",
                column: "ColorCapacityId",
                principalTable: "ColorCapacities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ColorCapacities_ColorCapacityId",
                table: "OrderDetails",
                column: "ColorCapacityId",
                principalTable: "ColorCapacities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ColorCapacities_ColorCapacityId",
                table: "Reviews",
                column: "ColorCapacityId",
                principalTable: "ColorCapacities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_ColorCapacities_ColorCapacityId",
                table: "WishLists",
                column: "ColorCapacityId",
                principalTable: "ColorCapacities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ColorCapacities_ColorCapacityId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ColorCapacities_ColorCapacityId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ColorCapacities_ColorCapacityId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_ColorCapacities_ColorCapacityId",
                table: "WishLists");

            migrationBuilder.DropTable(
                name: "ColorCapacities");

            migrationBuilder.RenameColumn(
                name: "ColorCapacityId",
                table: "WishLists",
                newName: "ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_ColorCapacityId",
                table: "WishLists",
                newName: "IX_WishLists_ColorId");

            migrationBuilder.RenameColumn(
                name: "ColorCapacityId",
                table: "Reviews",
                newName: "ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ColorCapacityId",
                table: "Reviews",
                newName: "IX_Reviews_ColorId");

            migrationBuilder.RenameColumn(
                name: "ColorCapacityId",
                table: "OrderDetails",
                newName: "ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ColorCapacityId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ColorId");

            migrationBuilder.RenameColumn(
                name: "ColorCapacityId",
                table: "Carts",
                newName: "ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ColorCapacityId",
                table: "Carts",
                newName: "IX_Carts_ColorId");

            migrationBuilder.AddColumn<int>(
                name: "CapacityId",
                table: "Colors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Colors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Colors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Colors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Colors_CapacityId",
                table: "Colors",
                column: "CapacityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Colors_ColorId",
                table: "Carts",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Capacities_CapacityId",
                table: "Colors",
                column: "CapacityId",
                principalTable: "Capacities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Colors_ColorId",
                table: "OrderDetails",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Colors_ColorId",
                table: "Reviews",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Colors_ColorId",
                table: "WishLists",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntrazShop.Migrations
{
    /// <inheritdoc />
    public partial class ProductColorCapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Capacities_Products_ProductId",
                table: "Capacities");

            migrationBuilder.DropIndex(
                name: "IX_Capacities_ProductId",
                table: "Capacities");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Capacities");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ColorCapacities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ColorCapacities_ProductId",
                table: "ColorCapacities",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorCapacities_Products_ProductId",
                table: "ColorCapacities",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorCapacities_Products_ProductId",
                table: "ColorCapacities");

            migrationBuilder.DropIndex(
                name: "IX_ColorCapacities_ProductId",
                table: "ColorCapacities");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ColorCapacities");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Capacities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Capacities_ProductId",
                table: "Capacities",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Capacities_Products_ProductId",
                table: "Capacities",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

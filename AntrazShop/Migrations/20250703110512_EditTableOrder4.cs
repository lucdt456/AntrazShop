using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntrazShop.Migrations
{
    /// <inheritdoc />
    public partial class EditTableOrder4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Orders");
        }
    }
}

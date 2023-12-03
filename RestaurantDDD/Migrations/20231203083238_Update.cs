using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantDDD.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TypeOfPay_TypeOfPayId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TypeOfPayId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TypOfPayId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TypeOfPayId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "StatusOfOrders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "StatusOfOrders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypOfPayId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfPayId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TypeOfPayId",
                table: "Orders",
                column: "TypeOfPayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_TypeOfPay_TypeOfPayId",
                table: "Orders",
                column: "TypeOfPayId",
                principalTable: "TypeOfPay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fa22LBT.Migrations
{
    /// <inheritdoc />
    public partial class SPUserOnetoOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPortfolios_AspNetUsers_CustomerId",
                table: "StockPortfolios");

            migrationBuilder.DropIndex(
                name: "IX_StockPortfolios_CustomerId",
                table: "StockPortfolios");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "StockPortfolios",
                newName: "AppUserForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_StockPortfolios_AppUserForeignKey",
                table: "StockPortfolios",
                column: "AppUserForeignKey",
                unique: true,
                filter: "[AppUserForeignKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_StockPortfolios_AspNetUsers_AppUserForeignKey",
                table: "StockPortfolios",
                column: "AppUserForeignKey",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPortfolios_AspNetUsers_AppUserForeignKey",
                table: "StockPortfolios");

            migrationBuilder.DropIndex(
                name: "IX_StockPortfolios_AppUserForeignKey",
                table: "StockPortfolios");

            migrationBuilder.RenameColumn(
                name: "AppUserForeignKey",
                table: "StockPortfolios",
                newName: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_StockPortfolios_CustomerId",
                table: "StockPortfolios",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockPortfolios_AspNetUsers_CustomerId",
                table: "StockPortfolios",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

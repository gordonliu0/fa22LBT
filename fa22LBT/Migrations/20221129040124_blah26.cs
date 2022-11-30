using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fa22LBT.Migrations
{
    /// <inheritdoc />
    public partial class blah26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockHolding_StockPortfolios_StockPortfolioAccountID",
                table: "StockHolding");

            migrationBuilder.DropForeignKey(
                name: "FK_StockHolding_Stocks_StockID",
                table: "StockHolding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockHolding",
                table: "StockHolding");

            migrationBuilder.RenameTable(
                name: "StockHolding",
                newName: "StockHoldings");

            migrationBuilder.RenameIndex(
                name: "IX_StockHolding_StockPortfolioAccountID",
                table: "StockHoldings",
                newName: "IX_StockHoldings_StockPortfolioAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_StockHolding_StockID",
                table: "StockHoldings",
                newName: "IX_StockHoldings_StockID");

            migrationBuilder.AlterColumn<string>(
                name: "StockPortfolioAccountID",
                table: "StockHoldings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockHoldings",
                table: "StockHoldings",
                column: "StockHoldingID");

            migrationBuilder.AddForeignKey(
                name: "FK_StockHoldings_StockPortfolios_StockPortfolioAccountID",
                table: "StockHoldings",
                column: "StockPortfolioAccountID",
                principalTable: "StockPortfolios",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockHoldings_Stocks_StockID",
                table: "StockHoldings",
                column: "StockID",
                principalTable: "Stocks",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockHoldings_StockPortfolios_StockPortfolioAccountID",
                table: "StockHoldings");

            migrationBuilder.DropForeignKey(
                name: "FK_StockHoldings_Stocks_StockID",
                table: "StockHoldings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockHoldings",
                table: "StockHoldings");

            migrationBuilder.RenameTable(
                name: "StockHoldings",
                newName: "StockHolding");

            migrationBuilder.RenameIndex(
                name: "IX_StockHoldings_StockPortfolioAccountID",
                table: "StockHolding",
                newName: "IX_StockHolding_StockPortfolioAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_StockHoldings_StockID",
                table: "StockHolding",
                newName: "IX_StockHolding_StockID");

            migrationBuilder.AlterColumn<string>(
                name: "StockPortfolioAccountID",
                table: "StockHolding",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockHolding",
                table: "StockHolding",
                column: "StockHoldingID");

            migrationBuilder.AddForeignKey(
                name: "FK_StockHolding_StockPortfolios_StockPortfolioAccountID",
                table: "StockHolding",
                column: "StockPortfolioAccountID",
                principalTable: "StockPortfolios",
                principalColumn: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_StockHolding_Stocks_StockID",
                table: "StockHolding",
                column: "StockID",
                principalTable: "Stocks",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

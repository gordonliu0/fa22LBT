using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fa22LBT.Migrations
{
    /// <inheritdoc />
    public partial class blah23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_StockPortfolios_StockPortfolioAccountID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_StockPortfolioAccountID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "StockPortfolioAccountID",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "BankAccountForeignKey",
                table: "StockPortfolios",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockPortfolios_BankAccountForeignKey",
                table: "StockPortfolios",
                column: "BankAccountForeignKey",
                unique: true,
                filter: "[BankAccountForeignKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_StockPortfolios_BankAccounts_BankAccountForeignKey",
                table: "StockPortfolios",
                column: "BankAccountForeignKey",
                principalTable: "BankAccounts",
                principalColumn: "AccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPortfolios_BankAccounts_BankAccountForeignKey",
                table: "StockPortfolios");

            migrationBuilder.DropIndex(
                name: "IX_StockPortfolios_BankAccountForeignKey",
                table: "StockPortfolios");

            migrationBuilder.DropColumn(
                name: "BankAccountForeignKey",
                table: "StockPortfolios");

            migrationBuilder.AddColumn<string>(
                name: "StockPortfolioAccountID",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_StockPortfolioAccountID",
                table: "Transactions",
                column: "StockPortfolioAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_StockPortfolios_StockPortfolioAccountID",
                table: "Transactions",
                column: "StockPortfolioAccountID",
                principalTable: "StockPortfolios",
                principalColumn: "AccountID");
        }
    }
}

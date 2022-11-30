using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fa22LBT.Migrations
{
    /// <inheritdoc />
    public partial class blah25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockHolding",
                columns: table => new
                {
                    StockHoldingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantityShares = table.Column<int>(type: "int", nullable: false),
                    StockPortfolioAccountID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StockID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHolding", x => x.StockHoldingID);
                    table.ForeignKey(
                        name: "FK_StockHolding_StockPortfolios_StockPortfolioAccountID",
                        column: x => x.StockPortfolioAccountID,
                        principalTable: "StockPortfolios",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_StockHolding_Stocks_StockID",
                        column: x => x.StockID,
                        principalTable: "Stocks",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockHolding_StockID",
                table: "StockHolding",
                column: "StockID");

            migrationBuilder.CreateIndex(
                name: "IX_StockHolding_StockPortfolioAccountID",
                table: "StockHolding",
                column: "StockPortfolioAccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockHolding");
        }
    }
}

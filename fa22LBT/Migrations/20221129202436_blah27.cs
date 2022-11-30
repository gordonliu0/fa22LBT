using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fa22LBT.Migrations
{
    /// <inheritdoc />
    public partial class blah27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "STransactionNo",
                table: "StockTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STransactionNo",
                table: "StockTransactions");
        }
    }
}

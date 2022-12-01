using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fa22LBT.Migrations
{
    /// <inheritdoc />
    public partial class blah28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminComments",
                table: "Disputes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminEmail",
                table: "Disputes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminComments",
                table: "Disputes");

            migrationBuilder.DropColumn(
                name: "AdminEmail",
                table: "Disputes");
        }
    }
}

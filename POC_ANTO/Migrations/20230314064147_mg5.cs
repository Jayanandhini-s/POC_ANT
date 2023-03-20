using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC_ANTO.Migrations
{
    /// <inheritdoc />
    public partial class mg5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingCost",
                table: "Invoice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShippingCost",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

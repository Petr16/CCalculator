using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCalculator.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsInDataInner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalSumPayment",
                table: "DataInners",
                type: "decimal(18,9)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSumPaymentByBody",
                table: "DataInners",
                type: "decimal(18,9)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSumPaymentByPercent",
                table: "DataInners",
                type: "decimal(18,9)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSumPayment",
                table: "DataInners");

            migrationBuilder.DropColumn(
                name: "TotalSumPaymentByBody",
                table: "DataInners");

            migrationBuilder.DropColumn(
                name: "TotalSumPaymentByPercent",
                table: "DataInners");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCalculator.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFieldNamePamentByPercentToPaymentByPercentInPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PamentByPercent",
                table: "Payments",
                newName: "PaymentByPercent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentByPercent",
                table: "Payments",
                newName: "PamentByPercent");
        }
    }
}

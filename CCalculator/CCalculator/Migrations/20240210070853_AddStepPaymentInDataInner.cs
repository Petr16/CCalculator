using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCalculator.Migrations
{
    /// <inheritdoc />
    public partial class AddStepPaymentInDataInner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StepPayment",
                table: "DataInners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepPayment",
                table: "DataInners");
        }
    }
}

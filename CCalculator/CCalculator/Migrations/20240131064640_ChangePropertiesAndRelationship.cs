using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCalculator.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertiesAndRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_DataInnerId",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DataInnerId",
                table: "Payments",
                column: "DataInnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_DataInnerId",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DataInnerId",
                table: "Payments",
                column: "DataInnerId",
                unique: true);
        }
    }
}

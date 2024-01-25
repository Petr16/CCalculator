using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCalculator.Migrations
{
    /// <inheritdoc />
    public partial class AddPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DataInner",
                table: "DataInner");

            migrationBuilder.RenameTable(
                name: "DataInner",
                newName: "DataInners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataInners",
                table: "DataInners",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PaymentByBody = table.Column<decimal>(type: "decimal(18,9)", nullable: false),
                    PamentByPercent = table.Column<decimal>(type: "decimal(18,9)", nullable: false),
                    BalanceOwed = table.Column<decimal>(type: "decimal(18,9)", nullable: false),
                    DataInnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_DataInners_DataInnerId",
                        column: x => x.DataInnerId,
                        principalTable: "DataInners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DataInnerId",
                table: "Payments",
                column: "DataInnerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataInners",
                table: "DataInners");

            migrationBuilder.RenameTable(
                name: "DataInners",
                newName: "DataInner");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataInner",
                table: "DataInner",
                column: "Id");
        }
    }
}

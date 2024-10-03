using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations.ImportProduct
{
    /// <inheritdoc />
    public partial class InitImportProductDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportProducts",
                columns: table => new
                {
                    ImportProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateImport = table.Column<DateOnly>(type: "date", nullable: false),
                    PersonInCharge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Payment = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportProducts", x => x.ImportProductId);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptProducts",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportProductId = table.Column<int>(type: "int", nullable: false),
                    ProId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    ProName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptProducts", x => x.ReceiptId);
                    table.ForeignKey(
                        name: "FK_ReceiptProducts_ImportProducts_ImportProductId",
                        column: x => x.ImportProductId,
                        principalTable: "ImportProducts",
                        principalColumn: "ImportProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptProducts_ImportProductId",
                table: "ReceiptProducts",
                column: "ImportProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptProducts");

            migrationBuilder.DropTable(
                name: "ImportProducts");
        }
    }
}

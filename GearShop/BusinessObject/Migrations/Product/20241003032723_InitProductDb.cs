using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations.Product
{
    /// <inheritdoc />
    public partial class InitProductDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    BrandName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    BrandLogo = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    CateId = table.Column<int>(type: "int", nullable: false),
                    CateName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Keyword = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.CateId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    CateId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ProName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProQuan = table.Column<int>(type: "int", nullable: false),
                    ProPrice = table.Column<double>(type: "float", nullable: false),
                    ProDes = table.Column<string>(type: "text", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CategoryCateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProId);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categorys_CategoryCateId",
                        column: x => x.CategoryCateId,
                        principalTable: "Categorys",
                        principalColumn: "CateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    ProId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Feature = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ProductProId = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.ProId);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductProId",
                        column: x => x.ProductProId,
                        principalTable: "Products",
                        principalColumn: "ProId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    ProImg = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    ProductProId = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ProId);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductProId",
                        column: x => x.ProductProId,
                        principalTable: "Products",
                        principalColumn: "ProId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductProId",
                table: "ProductAttributes",
                column: "ProductProId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductProId",
                table: "ProductImages",
                column: "ProductProId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryCateId",
                table: "Products",
                column: "CategoryCateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categorys");
        }
    }
}

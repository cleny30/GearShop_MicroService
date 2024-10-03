using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations.Customer
{
    /// <inheritdoc />
    public partial class InitCustomerDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Phone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Phone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Specific = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CustomerUsername = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Customers_CustomerUsername",
                        column: x => x.CustomerUsername,
                        principalTable: "Customers",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_CustomerUsername",
                table: "DeliveryAddresses",
                column: "CustomerUsername");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryAddresses");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}

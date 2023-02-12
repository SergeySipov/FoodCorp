using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodCorp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerAndPerformerProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerProduct",
                schema: "product",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProduct", x => new { x.CustomerId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CustomerProduct_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "user",
                        principalTable: "Customer",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformerProduct",
                schema: "product",
                columns: table => new
                {
                    PerformerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformerProduct", x => new { x.PerformerId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_PerformerProduct_Performer_PerformerId",
                        column: x => x.PerformerId,
                        principalSchema: "user",
                        principalTable: "Performer",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerformerProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProduct_ProductId",
                schema: "product",
                table: "CustomerProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformerProduct_ProductId",
                schema: "product",
                table: "PerformerProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerProduct",
                schema: "product");

            migrationBuilder.DropTable(
                name: "PerformerProduct",
                schema: "product");
        }
    }
}

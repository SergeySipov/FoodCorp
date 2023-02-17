using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodCorp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSchemaNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "usr");

            migrationBuilder.EnsureSchema(
                name: "prod");

            migrationBuilder.EnsureSchema(
                name: "ord");

            migrationBuilder.RenameTable(
                name: "UserShowcaseImage",
                schema: "user",
                newName: "UserShowcaseImage",
                newSchema: "usr");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "user",
                newName: "User",
                newSchema: "usr");

            migrationBuilder.RenameTable(
                name: "ProductImage",
                schema: "product",
                newName: "ProductImage",
                newSchema: "prod");

            migrationBuilder.RenameTable(
                name: "Product",
                schema: "product",
                newName: "Product",
                newSchema: "prod");

            migrationBuilder.RenameTable(
                name: "PerformerProduct",
                schema: "product",
                newName: "PerformerProduct",
                newSchema: "prod");

            migrationBuilder.RenameTable(
                name: "Performer",
                schema: "user",
                newName: "Performer",
                newSchema: "usr");

            migrationBuilder.RenameTable(
                name: "OrderOfferChatMessage",
                schema: "order",
                newName: "OrderOfferChatMessage",
                newSchema: "ord");

            migrationBuilder.RenameTable(
                name: "OrderOffer",
                schema: "order",
                newName: "OrderOffer",
                newSchema: "ord");

            migrationBuilder.RenameTable(
                name: "OrderFeedback",
                schema: "order",
                newName: "OrderFeedback",
                newSchema: "ord");

            migrationBuilder.RenameTable(
                name: "OrderDeliveryAndPaymentType",
                schema: "order",
                newName: "OrderDeliveryAndPaymentType",
                newSchema: "ord");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "order",
                newName: "Order",
                newSchema: "ord");

            migrationBuilder.RenameTable(
                name: "CustomerProduct",
                schema: "product",
                newName: "CustomerProduct",
                newSchema: "prod");

            migrationBuilder.RenameTable(
                name: "Customer",
                schema: "user",
                newName: "Customer",
                newSchema: "usr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.EnsureSchema(
                name: "order");

            migrationBuilder.RenameTable(
                name: "UserShowcaseImage",
                schema: "usr",
                newName: "UserShowcaseImage",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "usr",
                newName: "User",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "ProductImage",
                schema: "prod",
                newName: "ProductImage",
                newSchema: "product");

            migrationBuilder.RenameTable(
                name: "Product",
                schema: "prod",
                newName: "Product",
                newSchema: "product");

            migrationBuilder.RenameTable(
                name: "PerformerProduct",
                schema: "prod",
                newName: "PerformerProduct",
                newSchema: "product");

            migrationBuilder.RenameTable(
                name: "Performer",
                schema: "usr",
                newName: "Performer",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "OrderOfferChatMessage",
                schema: "ord",
                newName: "OrderOfferChatMessage",
                newSchema: "order");

            migrationBuilder.RenameTable(
                name: "OrderOffer",
                schema: "ord",
                newName: "OrderOffer",
                newSchema: "order");

            migrationBuilder.RenameTable(
                name: "OrderFeedback",
                schema: "ord",
                newName: "OrderFeedback",
                newSchema: "order");

            migrationBuilder.RenameTable(
                name: "OrderDeliveryAndPaymentType",
                schema: "ord",
                newName: "OrderDeliveryAndPaymentType",
                newSchema: "order");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "ord",
                newName: "Order",
                newSchema: "order");

            migrationBuilder.RenameTable(
                name: "CustomerProduct",
                schema: "prod",
                newName: "CustomerProduct",
                newSchema: "product");

            migrationBuilder.RenameTable(
                name: "Customer",
                schema: "usr",
                newName: "Customer",
                newSchema: "user");
        }
    }
}

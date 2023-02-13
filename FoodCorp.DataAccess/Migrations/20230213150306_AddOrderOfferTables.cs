using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodCorp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderOfferTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryMethod",
                schema: "ref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(48)", maxLength: 48, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderFeedback",
                schema: "order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "Float(2)", precision: 2, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderFeedback", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderFeedback_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "order",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderOffer",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "SmallDateTime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Price = table.Column<decimal>(type: "Decimal(9,0)", precision: 9, nullable: false),
                    IsSelected = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PerformerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderOffer_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "order",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderOffer_Performer_PerformerId",
                        column: x => x.PerformerId,
                        principalSchema: "user",
                        principalTable: "Performer",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                schema: "ref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(48)", maxLength: 48, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderOfferChatMessage",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "SmallDateTime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Message = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    OrderOfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderOfferChatMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderOfferChatMessage_OrderOffer_OrderOfferId",
                        column: x => x.OrderOfferId,
                        principalSchema: "order",
                        principalTable: "OrderOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDeliveryAndPaymentType",
                schema: "order",
                columns: table => new
                {
                    OrderOfferId = table.Column<int>(type: "int", nullable: false),
                    DeliveryMethodId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDeliveryAndPaymentType", x => x.OrderOfferId);
                    table.ForeignKey(
                        name: "FK_OrderDeliveryAndPaymentType_DeliveryMethod_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalSchema: "ref",
                        principalTable: "DeliveryMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDeliveryAndPaymentType_OrderOffer_OrderOfferId",
                        column: x => x.OrderOfferId,
                        principalSchema: "order",
                        principalTable: "OrderOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDeliveryAndPaymentType_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalSchema: "ref",
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "ref",
                table: "DeliveryMethod",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Classic Delivery" },
                    { 2, "Self Delivery" },
                    { 3, "Door Delivery" }
                });

            migrationBuilder.InsertData(
                schema: "ref",
                table: "PaymentMethod",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cash" },
                    { 2, "Credit Card" },
                    { 3, "Crypto Currency" },
                    { 4, "Electronic Wallet" },
                    { 5, "Debit Card" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryAndPaymentType_DeliveryMethodId",
                schema: "order",
                table: "OrderDeliveryAndPaymentType",
                column: "DeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryAndPaymentType_PaymentMethodId",
                schema: "order",
                table: "OrderDeliveryAndPaymentType",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderOffer_OrderId",
                schema: "order",
                table: "OrderOffer",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderOffer_PerformerId",
                schema: "order",
                table: "OrderOffer",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderOfferChatMessage_OrderOfferId",
                schema: "order",
                table: "OrderOfferChatMessage",
                column: "OrderOfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDeliveryAndPaymentType",
                schema: "order");

            migrationBuilder.DropTable(
                name: "OrderFeedback",
                schema: "order");

            migrationBuilder.DropTable(
                name: "OrderOfferChatMessage",
                schema: "order");

            migrationBuilder.DropTable(
                name: "DeliveryMethod",
                schema: "ref");

            migrationBuilder.DropTable(
                name: "PaymentMethod",
                schema: "ref");

            migrationBuilder.DropTable(
                name: "OrderOffer",
                schema: "order");
        }
    }
}

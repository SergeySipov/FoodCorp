using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodCorp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPerformerAndCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.RenameTable(
                name: "UserShowcaseImage",
                schema: "dbo",
                newName: "UserShowcaseImage",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "dbo",
                newName: "User",
                newSchema: "user");

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "user",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "Float(2)", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Customer_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Performer",
                schema: "user",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "Float(2)", precision: 2, nullable: false),
                    CountOfCompletedOrders = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performer", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Performer_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer",
                schema: "user");

            migrationBuilder.DropTable(
                name: "Performer",
                schema: "user");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "UserShowcaseImage",
                schema: "user",
                newName: "UserShowcaseImage",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "user",
                newName: "User",
                newSchema: "dbo");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagement.Infrastructure.Data.Migartions
{
    public partial class NewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_Name = table.Column<string>(nullable: true),
                    Book_Type = table.Column<string>(nullable: true),
                    Book_Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_Date = table.Column<DateTime>(nullable: false),
                    User_Id = table.Column<int>(nullable: false),
                    Order_Id = table.Column<string>(nullable: true),
                    Bill_Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Book_Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Book_Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_Name = table.Column<string>(nullable: true),
                    Book_Type = table.Column<string>(nullable: true),
                    Book_Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Order_Id = table.Column<string>(nullable: true),
                    CartId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Items_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Items_CartId",
                table: "Order_Items",
                column: "CartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Carts");
        }
    }
}

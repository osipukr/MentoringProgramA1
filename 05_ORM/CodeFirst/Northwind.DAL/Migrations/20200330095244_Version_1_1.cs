using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.DAL.Migrations
{
    public partial class Version_1_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditCardID",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Credit Cards",
                columns: table => new
                {
                    CreditCardID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardExpirationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CardHolderName = table.Column<string>(maxLength: 20, nullable: true),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credit Cards", x => x.CreditCardID);
                    table.ForeignKey(
                        name: "FK_Credit Cards_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credit Cards_EmployeeID",
                table: "Credit Cards",
                column: "EmployeeID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Credit Cards");

            migrationBuilder.DropColumn(name: "CreditCardID", table: "Employees");
        }
    }
}
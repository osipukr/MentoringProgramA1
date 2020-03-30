using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.DAL.Migrations
{
    public partial class Version_1_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Region",
                table: "Region");

            migrationBuilder.RenameTable(
                name: "Region",
                newName: "Regions");

            migrationBuilder.AddColumn<DateTime>(
                name: "EstablishmentDate",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                    name: "PK_Regions",
                    table: "Regions",
                    column: "RegionID")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "EstablishmentDate",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Region");

            migrationBuilder.AddPrimaryKey(
                    name: "PK_Region",
                    table: "Region",
                    column: "RegionID")
                .Annotation("SqlServer:Clustered", false);
        }
    }
}
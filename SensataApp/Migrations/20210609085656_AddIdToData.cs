using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SensataApp.Migrations
{
    public partial class AddIdToData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Data",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Data");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Data",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Data",
                table: "Data",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Data",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Data");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Data",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Data",
                table: "Data",
                column: "CreateTime");
        }
    }
}

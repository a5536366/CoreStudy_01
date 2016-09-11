using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreStudy_05.Migrations
{
    public partial class AddAuthr_News_defaultvalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderNo",
                table: "News",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR shared.OrderNumbers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "News",
                type: "datetime",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "News");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "News",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2016, 9, 11, 14, 39, 39, 102, DateTimeKind.Local));
        }
    }
}

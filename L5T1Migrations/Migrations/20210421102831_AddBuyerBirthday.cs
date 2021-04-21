using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace L5T1Migrations.Migrations
{
    public partial class AddBuyerBirthday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Buyers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Buyers");
        }
    }
}

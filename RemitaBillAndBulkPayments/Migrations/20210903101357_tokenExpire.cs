using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RemitaBillAndBulkPayments.Migrations
{
    public partial class tokenExpire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDateTime",
                table: "TokenData",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDateTime",
                table: "TokenData");
        }
    }
}

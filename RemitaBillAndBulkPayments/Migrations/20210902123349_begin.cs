using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RemitaBillAndBulkPayments.Migrations
{
    public partial class begin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BulkRequest",
                columns: table => new
                {
                    batchRef = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    totalAmount = table.Column<double>(type: "float", nullable: false),
                    sourceAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sourceAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sourceBankCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sourceNarration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    originalAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    originalBankCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customReference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkRequest", x => x.batchRef);
                });

            migrationBuilder.CreateTable(
                name: "BulkResponse",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datastr = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkResponse", x => x.id);
                });

           

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    transactionRef = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<double>(type: "float", nullable: false),
                    destinationAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    destinationAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    destinationBankCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    destinationNarration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    batchRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BulkRequestbatchRef = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.transactionRef);
                    table.ForeignKey(
                        name: "FK_Transaction_BulkRequest_BulkRequestbatchRef",
                        column: x => x.BulkRequestbatchRef,
                        principalTable: "BulkRequest",
                        principalColumn: "batchRef",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BulkRequestbatchRef",
                table: "Transaction",
                column: "BulkRequestbatchRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulkResponse");

            migrationBuilder.DropTable(
                name: "PaymentRequest");

            migrationBuilder.DropTable(
                name: "PaymentResponse");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "BulkRequest");
        }
    }
}

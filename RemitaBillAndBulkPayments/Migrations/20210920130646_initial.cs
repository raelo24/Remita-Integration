using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RemitaBillAndBulkPayments.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BulkRequests",
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
                    table.PrimaryKey("PK_BulkRequests", x => x.batchRef);
                });

            migrationBuilder.CreateTable(
                name: "BulkResponses",
                columns: table => new
                {
                    BulkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkResponses", x => x.BulkId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rrr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    incomeAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    debittedAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentAuthCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentChannel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tellerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    branchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amountDebitted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fundingSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transactionId = table.Column<long>(type: "bigint", nullable: false),
                    RequestDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentResponses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rrr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalAmount = table.Column<double>(type: "float", nullable: false),
                    balanceDue = table.Column<double>(type: "float", nullable: false),
                    paymentRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    debittedAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amountDebitted = table.Column<double>(type: "float", nullable: false),
                    transactionId = table.Column<long>(type: "bigint", nullable: false),
                    responseMsg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    responseCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentResponses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TokenData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accessToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expiresIn = table.Column<int>(type: "int", nullable: false),
                    ExpireDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transactionRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<double>(type: "float", nullable: false),
                    destinationAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    destinationAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    destinationBankCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    destinationNarration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BulkRequestbatchRef = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transaction_BulkRequests_BulkRequestbatchRef",
                        column: x => x.BulkRequestbatchRef,
                        principalTable: "BulkRequests",
                        principalColumn: "batchRef",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BulkData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    batchRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalAmount = table.Column<double>(type: "float", nullable: false),
                    authorizationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transactionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BulkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkData", x => x.id);
                    table.ForeignKey(
                        name: "FK_BulkData_BulkResponses_BulkId",
                        column: x => x.BulkId,
                        principalTable: "BulkResponses",
                        principalColumn: "BulkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BulkData_BulkId",
                table: "BulkData",
                column: "BulkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BulkRequestbatchRef",
                table: "Transaction",
                column: "BulkRequestbatchRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulkData");

            migrationBuilder.DropTable(
                name: "PaymentRequests");

            migrationBuilder.DropTable(
                name: "PaymentResponses");

            migrationBuilder.DropTable(
                name: "TokenData");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "BulkResponses");

            migrationBuilder.DropTable(
                name: "BulkRequests");
        }
    }
}

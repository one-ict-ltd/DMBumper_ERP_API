using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ONEERP.Migrations
{
    public partial class addTndrFinalChallanDetails063001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFinal",
                table: "TndrChallanMaster",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "finalChallanDetailsId",
                table: "SalSalesInvoiceDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "challanMasterId",
                table: "SalSalesInvoice",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TndrFinalChallanDetails",
                columns: table => new
                {
                    finalChallanDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isActive = table.Column<bool>(nullable: true),
                    isDelete = table.Column<bool>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    challanDetailsId = table.Column<int>(nullable: true),
                    challanMasterId = table.Column<int>(nullable: true),
                    quotationDetailsId = table.Column<int>(nullable: false),
                    quotationMasterId = table.Column<int>(nullable: true),
                    productId = table.Column<int>(nullable: true),
                    productWiseSpecificationId = table.Column<int>(nullable: true),
                    challanQty = table.Column<decimal>(nullable: true),
                    convertionQty = table.Column<decimal>(nullable: true),
                    CtnQty = table.Column<decimal>(nullable: true),
                    toUomId = table.Column<int>(nullable: true),
                    price = table.Column<decimal>(nullable: true),
                    vat = table.Column<decimal>(nullable: true),
                    unitVat = table.Column<decimal>(nullable: true),
                    tradePrice = table.Column<decimal>(nullable: true),
                    ait = table.Column<decimal>(nullable: true),
                    discountAmount = table.Column<decimal>(nullable: true),
                    Total = table.Column<decimal>(nullable: true),
                    barcodeId = table.Column<int>(nullable: true),
                    serialNumber = table.Column<string>(nullable: true),
                    batchNo = table.Column<string>(nullable: true),
                    specification = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    deliveryStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TndrFinalChallanDetails", x => x.finalChallanDetailsId);
                    table.ForeignKey(
                        name: "FK_TndrFinalChallanDetails_InvStockInWithBarcode_barcodeId",
                        column: x => x.barcodeId,
                        principalTable: "InvStockInWithBarcode",
                        principalColumn: "barcodeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrFinalChallanDetails_TndrChallanDetails_challanDetailsId",
                        column: x => x.challanDetailsId,
                        principalTable: "TndrChallanDetails",
                        principalColumn: "challanDetailsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrFinalChallanDetails_TndrChallanMaster_challanMasterId",
                        column: x => x.challanMasterId,
                        principalTable: "TndrChallanMaster",
                        principalColumn: "challanMasterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrFinalChallanDetails_InvProduct_productId",
                        column: x => x.productId,
                        principalTable: "InvProduct",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrFinalChallanDetails_InvProductWiseSpecification_productWiseSpecificationId",
                        column: x => x.productWiseSpecificationId,
                        principalTable: "InvProductWiseSpecification",
                        principalColumn: "productWiseSpecificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrFinalChallanDetails_TndrQuotationMaster_quotationMasterId",
                        column: x => x.quotationMasterId,
                        principalTable: "TndrQuotationMaster",
                        principalColumn: "quotationMasterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalSalesInvoiceDetails_finalChallanDetailsId",
                table: "SalSalesInvoiceDetails",
                column: "finalChallanDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SalSalesInvoice_challanMasterId",
                table: "SalSalesInvoice",
                column: "challanMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrFinalChallanDetails_barcodeId",
                table: "TndrFinalChallanDetails",
                column: "barcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrFinalChallanDetails_challanDetailsId",
                table: "TndrFinalChallanDetails",
                column: "challanDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrFinalChallanDetails_challanMasterId",
                table: "TndrFinalChallanDetails",
                column: "challanMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrFinalChallanDetails_productId",
                table: "TndrFinalChallanDetails",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrFinalChallanDetails_productWiseSpecificationId",
                table: "TndrFinalChallanDetails",
                column: "productWiseSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrFinalChallanDetails_quotationMasterId",
                table: "TndrFinalChallanDetails",
                column: "quotationMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalSalesInvoice_TndrChallanMaster_challanMasterId",
                table: "SalSalesInvoice",
                column: "challanMasterId",
                principalTable: "TndrChallanMaster",
                principalColumn: "challanMasterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalSalesInvoiceDetails_TndrFinalChallanDetails_finalChallanDetailsId",
                table: "SalSalesInvoiceDetails",
                column: "finalChallanDetailsId",
                principalTable: "TndrFinalChallanDetails",
                principalColumn: "finalChallanDetailsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalSalesInvoice_TndrChallanMaster_challanMasterId",
                table: "SalSalesInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_SalSalesInvoiceDetails_TndrFinalChallanDetails_finalChallanDetailsId",
                table: "SalSalesInvoiceDetails");

            migrationBuilder.DropTable(
                name: "TndrFinalChallanDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalSalesInvoiceDetails_finalChallanDetailsId",
                table: "SalSalesInvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalSalesInvoice_challanMasterId",
                table: "SalSalesInvoice");

            migrationBuilder.DropColumn(
                name: "isFinal",
                table: "TndrChallanMaster");

            migrationBuilder.DropColumn(
                name: "finalChallanDetailsId",
                table: "SalSalesInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "challanMasterId",
                table: "SalSalesInvoice");
        }
    }
}

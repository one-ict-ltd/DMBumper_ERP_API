using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ONEERP.Migrations
{
    public partial class addTndrBillMaster070601 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "billDetailsId",
                table: "SalSalesInvoiceDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "billMasterId",
                table: "SalSalesInvoice",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TndrBillMaster",
                columns: table => new
                {
                    billMasterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isActive = table.Column<bool>(nullable: true),
                    isDelete = table.Column<bool>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    billNo = table.Column<string>(nullable: true),
                    billDate = table.Column<DateTime>(nullable: true),
                    partyId = table.Column<int>(nullable: true),
                    storeId = table.Column<int>(nullable: true),
                    mobileNo = table.Column<string>(maxLength: 50, nullable: true),
                    alternateMobileNo = table.Column<string>(maxLength: 50, nullable: true),
                    address = table.Column<string>(nullable: true),
                    totalGross = table.Column<decimal>(nullable: true),
                    totalVat = table.Column<decimal>(nullable: true),
                    totalAit = table.Column<decimal>(nullable: true),
                    shippingCost = table.Column<decimal>(nullable: true),
                    totalDiscountAmount = table.Column<decimal>(nullable: true),
                    grandTotal = table.Column<decimal>(nullable: true),
                    planId = table.Column<int>(nullable: true),
                    refNo = table.Column<string>(nullable: true),
                    isClosed = table.Column<int>(nullable: true),
                    billStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TndrBillMaster", x => x.billMasterId);
                    table.ForeignKey(
                        name: "FK_TndrBillMaster_AccParty_partyId",
                        column: x => x.partyId,
                        principalTable: "AccParty",
                        principalColumn: "partyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TndrBillDetails",
                columns: table => new
                {
                    billDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isActive = table.Column<bool>(nullable: true),
                    isDelete = table.Column<bool>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    billMasterId = table.Column<int>(nullable: true),
                    challanDetailsId = table.Column<int>(nullable: false),
                    productId = table.Column<int>(nullable: true),
                    productWiseSpecificationId = table.Column<int>(nullable: true),
                    billQty = table.Column<decimal>(nullable: true),
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
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TndrBillDetails", x => x.billDetailsId);
                    table.ForeignKey(
                        name: "FK_TndrBillDetails_InvStockInWithBarcode_barcodeId",
                        column: x => x.barcodeId,
                        principalTable: "InvStockInWithBarcode",
                        principalColumn: "barcodeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrBillDetails_TndrBillMaster_billMasterId",
                        column: x => x.billMasterId,
                        principalTable: "TndrBillMaster",
                        principalColumn: "billMasterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrBillDetails_TndrChallanDetails_challanDetailsId",
                        column: x => x.challanDetailsId,
                        principalTable: "TndrChallanDetails",
                        principalColumn: "challanDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TndrBillDetails_InvProduct_productId",
                        column: x => x.productId,
                        principalTable: "InvProduct",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrBillDetails_InvProductWiseSpecification_productWiseSpecificationId",
                        column: x => x.productWiseSpecificationId,
                        principalTable: "InvProductWiseSpecification",
                        principalColumn: "productWiseSpecificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalSalesInvoiceDetails_billDetailsId",
                table: "SalSalesInvoiceDetails",
                column: "billDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SalSalesInvoice_billMasterId",
                table: "SalSalesInvoice",
                column: "billMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrBillDetails_barcodeId",
                table: "TndrBillDetails",
                column: "barcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrBillDetails_billMasterId",
                table: "TndrBillDetails",
                column: "billMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrBillDetails_challanDetailsId",
                table: "TndrBillDetails",
                column: "challanDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrBillDetails_productId",
                table: "TndrBillDetails",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrBillDetails_productWiseSpecificationId",
                table: "TndrBillDetails",
                column: "productWiseSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrBillMaster_partyId",
                table: "TndrBillMaster",
                column: "partyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalSalesInvoice_TndrBillMaster_billMasterId",
                table: "SalSalesInvoice",
                column: "billMasterId",
                principalTable: "TndrBillMaster",
                principalColumn: "billMasterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalSalesInvoiceDetails_TndrBillDetails_billDetailsId",
                table: "SalSalesInvoiceDetails",
                column: "billDetailsId",
                principalTable: "TndrBillDetails",
                principalColumn: "billDetailsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalSalesInvoice_TndrBillMaster_billMasterId",
                table: "SalSalesInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_SalSalesInvoiceDetails_TndrBillDetails_billDetailsId",
                table: "SalSalesInvoiceDetails");

            migrationBuilder.DropTable(
                name: "TndrBillDetails");

            migrationBuilder.DropTable(
                name: "TndrBillMaster");

            migrationBuilder.DropIndex(
                name: "IX_SalSalesInvoiceDetails_billDetailsId",
                table: "SalSalesInvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalSalesInvoice_billMasterId",
                table: "SalSalesInvoice");

            migrationBuilder.DropColumn(
                name: "billDetailsId",
                table: "SalSalesInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "billMasterId",
                table: "SalSalesInvoice");
        }
    }
}

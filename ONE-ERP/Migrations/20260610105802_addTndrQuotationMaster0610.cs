using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ONEERP.Migrations
{
    public partial class addTndrQuotationMaster0610 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TndrQuotationMaster",
                columns: table => new
                {
                    quotationMasterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isActive = table.Column<bool>(nullable: true),
                    isDelete = table.Column<bool>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    quotationNo = table.Column<string>(maxLength: 100, nullable: true),
                    quotationDate = table.Column<DateTime>(nullable: true),
                    paymentDate = table.Column<DateTime>(nullable: true),
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
                    approvalStatus = table.Column<int>(nullable: true),
                    planId = table.Column<int>(nullable: true),
                    chemistId = table.Column<int>(nullable: true),
                    refNo = table.Column<string>(maxLength: 100, nullable: true),
                    orderType = table.Column<string>(nullable: true),
                    transactionTypeId = table.Column<int>(nullable: true),
                    territoryCode = table.Column<string>(nullable: true),
                    areaCode = table.Column<string>(nullable: true),
                    regionCode = table.Column<string>(nullable: true),
                    zoneCode = table.Column<string>(nullable: true),
                    territoryOfficer = table.Column<string>(nullable: true),
                    territoryOfficerName = table.Column<string>(nullable: true),
                    areaManager = table.Column<string>(nullable: true),
                    regionManager = table.Column<string>(nullable: true),
                    zoneManager = table.Column<string>(nullable: true),
                    depotCode = table.Column<string>(nullable: true),
                    isClosed = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TndrQuotationMaster", x => x.quotationMasterId);
                    table.ForeignKey(
                        name: "FK_TndrQuotationMaster_AccParty_partyId",
                        column: x => x.partyId,
                        principalTable: "AccParty",
                        principalColumn: "partyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrQuotationMaster_CmnTransactionType_transactionTypeId",
                        column: x => x.transactionTypeId,
                        principalTable: "CmnTransactionType",
                        principalColumn: "transactionTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TndrQuotationDetails",
                columns: table => new
                {
                    quotationDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isActive = table.Column<bool>(nullable: true),
                    isDelete = table.Column<bool>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    quotationMasterId = table.Column<int>(nullable: true),
                    productId = table.Column<int>(nullable: true),
                    productWiseSpecificationId = table.Column<int>(nullable: true),
                    invoiceQty = table.Column<decimal>(nullable: true),
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
                    hasNationalBonus = table.Column<bool>(nullable: true),
                    specification = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TndrQuotationDetails", x => x.quotationDetailsId);
                    table.ForeignKey(
                        name: "FK_TndrQuotationDetails_InvStockInWithBarcode_barcodeId",
                        column: x => x.barcodeId,
                        principalTable: "InvStockInWithBarcode",
                        principalColumn: "barcodeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrQuotationDetails_InvProduct_productId",
                        column: x => x.productId,
                        principalTable: "InvProduct",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrQuotationDetails_InvProductWiseSpecification_productWiseSpecificationId",
                        column: x => x.productWiseSpecificationId,
                        principalTable: "InvProductWiseSpecification",
                        principalColumn: "productWiseSpecificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrQuotationDetails_TndrQuotationMaster_quotationMasterId",
                        column: x => x.quotationMasterId,
                        principalTable: "TndrQuotationMaster",
                        principalColumn: "quotationMasterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TndrQuotationDetails_barcodeId",
                table: "TndrQuotationDetails",
                column: "barcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrQuotationDetails_productId",
                table: "TndrQuotationDetails",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrQuotationDetails_productWiseSpecificationId",
                table: "TndrQuotationDetails",
                column: "productWiseSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrQuotationDetails_quotationMasterId",
                table: "TndrQuotationDetails",
                column: "quotationMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrQuotationMaster_partyId",
                table: "TndrQuotationMaster",
                column: "partyId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrQuotationMaster_transactionTypeId",
                table: "TndrQuotationMaster",
                column: "transactionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TndrQuotationDetails");

            migrationBuilder.DropTable(
                name: "TndrQuotationMaster");
        }
    }
}

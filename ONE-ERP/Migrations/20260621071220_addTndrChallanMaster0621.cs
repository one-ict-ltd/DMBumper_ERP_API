using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ONEERP.Migrations
{
    public partial class addTndrChallanMaster0621 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TndrChallanMaster",
                columns: table => new
                {
                    challanMasterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isActive = table.Column<bool>(nullable: true),
                    isDelete = table.Column<bool>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    challanNo = table.Column<string>(maxLength: 100, nullable: true),
                    challanDate = table.Column<DateTime>(nullable: true),
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
                    refNo = table.Column<string>(nullable: true),
                    orderType = table.Column<string>(nullable: true),
                    isClosed = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TndrChallanMaster", x => x.challanMasterId);
                    table.ForeignKey(
                        name: "FK_TndrChallanMaster_AccParty_partyId",
                        column: x => x.partyId,
                        principalTable: "AccParty",
                        principalColumn: "partyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TndrChallanDetails",
                columns: table => new
                {
                    challanDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isActive = table.Column<bool>(nullable: true),
                    isDelete = table.Column<bool>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
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
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TndrChallanDetails", x => x.challanDetailsId);
                    table.ForeignKey(
                        name: "FK_TndrChallanDetails_InvStockInWithBarcode_barcodeId",
                        column: x => x.barcodeId,
                        principalTable: "InvStockInWithBarcode",
                        principalColumn: "barcodeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrChallanDetails_TndrChallanMaster_challanMasterId",
                        column: x => x.challanMasterId,
                        principalTable: "TndrChallanMaster",
                        principalColumn: "challanMasterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrChallanDetails_InvProduct_productId",
                        column: x => x.productId,
                        principalTable: "InvProduct",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrChallanDetails_InvProductWiseSpecification_productWiseSpecificationId",
                        column: x => x.productWiseSpecificationId,
                        principalTable: "InvProductWiseSpecification",
                        principalColumn: "productWiseSpecificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TndrChallanDetails_TndrQuotationMaster_quotationMasterId",
                        column: x => x.quotationMasterId,
                        principalTable: "TndrQuotationMaster",
                        principalColumn: "quotationMasterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TndrChallanDetails_barcodeId",
                table: "TndrChallanDetails",
                column: "barcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrChallanDetails_challanMasterId",
                table: "TndrChallanDetails",
                column: "challanMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrChallanDetails_productId",
                table: "TndrChallanDetails",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrChallanDetails_productWiseSpecificationId",
                table: "TndrChallanDetails",
                column: "productWiseSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrChallanDetails_quotationMasterId",
                table: "TndrChallanDetails",
                column: "quotationMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TndrChallanMaster_partyId",
                table: "TndrChallanMaster",
                column: "partyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TndrChallanDetails");

            migrationBuilder.DropTable(
                name: "TndrChallanMaster");
        }
    }
}

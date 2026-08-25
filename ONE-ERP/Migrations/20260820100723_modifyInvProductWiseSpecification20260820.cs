using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ONEERP.Migrations
{
    public partial class modifyInvProductWiseSpecification20260820 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "partslink",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "qtyonHand",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uom",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "listPrice",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "costPrice",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "salesPrice",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "fromYear",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "toYear",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "make",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "model",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
               name: "productCategoryId",
               table: "InvProductWiseSpecification",
               nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "subCategory",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "oem",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "interchange",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "patent",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "side",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "position",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "material",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "colorOrFinish",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "certification",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "barcodeOrQR",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "productWeight",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productWeight_UOM",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productWidth",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productHeight",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productLength",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productSizeUOM",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productActive",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productTaxable",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isWebsiteActive",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isReturnable",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "warrantyDays",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastReceivedDate",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastSoldDate",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "primaryVendor",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "vendorType",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "submodelOrTrim",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bodyStyle",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "engineSize",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "warehouse",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zone",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "aisle",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rack",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shelf",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bin",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pickLocationOrZone",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bulkLocationOrZone",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "qtyReserved",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "qtyDamagedHold",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "qtyReceivingHold",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "qtyReturnIntake",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "qtyVendorReturn",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "qtyScrap",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "previousCountdays",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "spotCountDate",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "currentCountDays",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "cycleCountFrequency",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "abc_Class",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "leadTimeDays",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "safetyStock",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "minStock",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "maxStock",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "suggestedReorderQty",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "vendorName",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "defaultVendor",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "vendorPartNumber",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "cost",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "vendorUOM",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "assetAccount",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cogsAccount",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adjustmentAccount",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "scrapAccount",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "varianceAccount",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "incomeAccount",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "upc",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "partTypeID",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "batchNumber",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InvMake",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isActive = table.Column<bool>(nullable: true),
                    isDelete = table.Column<bool>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    make = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvMake", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvMakeModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isActive = table.Column<bool>(nullable: true),
                    isDelete = table.Column<bool>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    makeId = table.Column<int>(nullable: false),
                    make = table.Column<string>(nullable: true),
                    model = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvMakeModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvProductWiseSpecification_productCategoryId",
                table: "InvProductWiseSpecification",
                column: "productCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvProductWiseSpecification_InvProductCategory_productCategoryId",
                table: "InvProductWiseSpecification",
                column: "productCategoryId",
                principalTable: "InvProductCategory",
                principalColumn: "productCategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvProductWiseSpecification_InvProductCategory_productCategoryId",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropTable(
                name: "InvMake");

            migrationBuilder.DropTable(
                name: "InvMakeModel");

            migrationBuilder.DropIndex(
                name: "IX_InvProductWiseSpecification_productCategoryId",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "partslink",
                table: "InvProductWiseSpecification");
            
            migrationBuilder.DropColumn(
                name: "location",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "qtyonHand",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "uom",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "listPrice",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "costPrice",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "salesPrice",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "fromYear",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "toYear",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "make",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "model",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "productCategoryId",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "category",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "subCategory",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "oem",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "interchange",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "patent",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "side",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "position",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "material",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "colorOrFinish",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "certification",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "status",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "barcodeOrQR",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "productWeight",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "productWeight_UOM",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "productWidth",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "productHeight",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "productLength",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "productSizeUOM",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "productActive",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "productTaxable",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "isWebsiteActive",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "isReturnable",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "warrantyDays",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "lastReceivedDate",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "lastSoldDate",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "primaryVendor",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "vendorType",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "submodelOrTrim",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "bodyStyle",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "engineSize",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "warehouse",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "zone",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "aisle",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "rack",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "shelf",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "bin",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "pickLocationOrZone",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "bulkLocationOrZone",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "qtyReserved",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "qtyDamagedHold",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "qtyReceivingHold",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "qtyReturnIntake",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "qtyVendorReturn",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "qtyScrap",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "previousCountdays",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "spotCountDate",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "currentCountDays",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "cycleCountFrequency",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "abc_Class",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "leadTimeDays",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "safetyStock",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "minStock",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "maxStock",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "suggestedReorderQty",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "vendorName",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "defaultVendor",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "vendorPartNumber",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "cost",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "vendorUOM",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "assetAccount",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "cogsAccount",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "adjustmentAccount",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "scrapAccount",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "varianceAccount",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "incomeAccount",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "upc",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "partTypeID",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "batchNumber",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "InvProductWiseSpecification");
        }
    }
}

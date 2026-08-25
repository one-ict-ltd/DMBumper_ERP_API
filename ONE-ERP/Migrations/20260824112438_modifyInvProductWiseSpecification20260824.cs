using Microsoft.EntityFrameworkCore.Migrations;

namespace ONEERP.Migrations
{
    public partial class modifyInvProductWiseSpecification20260824 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "makeId",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "makeModelId",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "productSubCategoryId",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "uomId",
                table: "InvProductWiseSpecification",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvProductWiseSpecification_productSubCategoryId",
                table: "InvProductWiseSpecification",
                column: "productSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InvProductWiseSpecification_uomId",
                table: "InvProductWiseSpecification",
                column: "uomId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvProductWiseSpecification_InvProductSubCategory_productSubCategoryId",
                table: "InvProductWiseSpecification",
                column: "productSubCategoryId",
                principalTable: "InvProductSubCategory",
                principalColumn: "productSubCategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvProductWiseSpecification_InvProductUOM_uomId",
                table: "InvProductWiseSpecification",
                column: "uomId",
                principalTable: "InvProductUOM",
                principalColumn: "uomId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvProductWiseSpecification_InvProductSubCategory_productSubCategoryId",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropForeignKey(
                name: "FK_InvProductWiseSpecification_InvProductUOM_uomId",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropIndex(
                name: "IX_InvProductWiseSpecification_productSubCategoryId",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropIndex(
                name: "IX_InvProductWiseSpecification_uomId",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "makeId",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "makeModelId",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "productSubCategoryId",
                table: "InvProductWiseSpecification");

            migrationBuilder.DropColumn(
                name: "uomId",
                table: "InvProductWiseSpecification");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ONEERP.Migrations
{
    public partial class modifyInvStockReceive20260727 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "challanNo",
                table: "InvStockReceive",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lcNo",
                table: "InvStockReceive",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "purchaseOrderNo",
                table: "InvStockReceive",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "supplierName",
                table: "InvStockReceive",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "challanNo",
                table: "InvStockReceive");

            migrationBuilder.DropColumn(
                name: "lcNo",
                table: "InvStockReceive");

            migrationBuilder.DropColumn(
                name: "purchaseOrderNo",
                table: "InvStockReceive");

            migrationBuilder.DropColumn(
                name: "supplierName",
                table: "InvStockReceive");
        }
    }
}

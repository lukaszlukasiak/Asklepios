using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Data.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "StandardPrice",
                table: "MedicalServices",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "MedicalServiceDiscount",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "StandardPrice",
                table: "MedicalServices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "MedicalServiceDiscount",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");
        }
    }
}

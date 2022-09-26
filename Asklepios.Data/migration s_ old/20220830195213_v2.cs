using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRooms_Locations_LocationId",
                table: "MedicalRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRooms_Locations_LocationId1",
                table: "MedicalRooms");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRooms_LocationId1",
                table: "MedicalRooms");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "MedicalRooms");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRooms_Locations_LocationId",
                table: "MedicalRooms",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRooms_Locations_LocationId",
                table: "MedicalRooms");

            migrationBuilder.AddColumn<long>(
                name: "LocationId1",
                table: "MedicalRooms",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRooms_LocationId1",
                table: "MedicalRooms",
                column: "LocationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRooms_Locations_LocationId",
                table: "MedicalRooms",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRooms_Locations_LocationId1",
                table: "MedicalRooms",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}

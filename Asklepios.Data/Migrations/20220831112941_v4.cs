using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Data.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssuedMedicine_Prescriptions_PrescriptionId",
                table: "IssuedMedicine");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MedicalPackages_MedicalPackageId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssuedMedicine",
                table: "IssuedMedicine");

            migrationBuilder.RenameTable(
                name: "IssuedMedicine",
                newName: "IssuedMedicines");

            migrationBuilder.RenameIndex(
                name: "IX_IssuedMedicine_PrescriptionId",
                table: "IssuedMedicines",
                newName: "IX_IssuedMedicines_PrescriptionId");

            migrationBuilder.AddColumn<long>(
                name: "VisitId",
                table: "Prescriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VisitId",
                table: "MedicalTestResults",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "PrescriptionId",
                table: "IssuedMedicines",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssuedMedicines",
                table: "IssuedMedicines",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IssuedMedicines",
                columns: new[] { "Id", "MedicineName", "PackageSize", "PaymentDiscount", "PrescriptionId" },
                values: new object[,]
                {
                    { 24L, "Metformax", "60 tabletek", 30f, 11L },
                    { 25L, "Metformina", "50 tabletek", 40f, 11L },
                    { 26L, "Belosalic", "Buteleczka 100 ml", 40f, 11L },
                    { 27L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 12L },
                    { 28L, "Vivacor 25 mg", "60 tabletek", 40f, 12L },
                    { 29L, "Lakcid", "20 tabletek", 30f, 13L },
                    { 30L, "Trilac Plus", "30 tabletek", 0f, 13L },
                    { 31L, "Enterol", "30 tabletek", 40f, 13L },
                    { 32L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 14L },
                    { 33L, "Vivacor 25 mg", "60 tabletek", 40f, 14L },
                    { 34L, "Lakcid", "20 tabletek", 30f, 15L },
                    { 35L, "Trilac Plus", "30 tabletek", 0f, 15L },
                    { 36L, "Enterol", "30 tabletek", 40f, 15L },
                    { 37L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 16L },
                    { 38L, "Vivacor 25 mg", "60 tabletek", 40f, 16L },
                    { 39L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 17L },
                    { 40L, "Nebbud 2 ml", "20 ampułek", 66f, 17L },
                    { 41L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 18L },
                    { 42L, "Vivacor 25 mg", "60 tabletek", 40f, 18L },
                    { 43L, "Iberogast", "100 ml", 20f, 19L },
                    { 44L, "Espumisan 40 mg", "100 tabletek", 20f, 19L },
                    { 45L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 20L },
                    { 46L, "Vivacor 25 mg", "60 tabletek", 40f, 20L },
                    { 47L, "Lakcid", "20 tabletek", 30f, 21L },
                    { 48L, "Trilac Plus", "30 tabletek", 0f, 21L },
                    { 49L, "Enterol", "30 tabletek", 40f, 21L },
                    { 50L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 22L },
                    { 51L, "Vivacor 25 mg", "60 tabletek", 40f, 22L },
                    { 52L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 23L },
                    { 53L, "Vivacor 25 mg", "60 tabletek", 40f, 23L },
                    { 54L, "Nezyr 28 mg", "28 tabletek", 30f, 24L },
                    { 55L, "Apo-Pentox 400 SR 400 mg", "30 tabletek", 40f, 24L },
                    { 56L, "Eltroxin", "60 tabletek", 30f, 25L },
                    { 57L, "Thyrozol", "40 tabletek", 10f, 25L },
                    { 58L, "Metoprolol", "100 tabletek", 40f, 25L },
                    { 59L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 26L },
                    { 60L, "Vivacor 25 mg", "60 tabletek", 40f, 26L },
                    { 61L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 27L },
                    { 62L, "Vivacor 25 mg", "60 tabletek", 40f, 27L },
                    { 63L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 28L },
                    { 64L, "Nebbud 2 ml", "20 ampułek", 66f, 28L },
                    { 65L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 29L }
                });

            migrationBuilder.InsertData(
                table: "IssuedMedicines",
                columns: new[] { "Id", "MedicineName", "PackageSize", "PaymentDiscount", "PrescriptionId" },
                values: new object[,]
                {
                    { 66L, "Nebbud 2 ml", "20 ampułek", 66f, 29L },
                    { 67L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 30L },
                    { 68L, "Vivacor 25 mg", "60 tabletek", 40f, 30L },
                    { 69L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 31L },
                    { 70L, "Nebbud 2 ml", "20 ampułek", 66f, 31L },
                    { 71L, "Iberogast", "100 ml", 20f, 32L },
                    { 72L, "Espumisan 40 mg", "100 tabletek", 20f, 32L },
                    { 73L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 33L },
                    { 74L, "Nebbud 2 ml", "20 ampułek", 66f, 33L },
                    { 75L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 34L },
                    { 76L, "Vivacor 25 mg", "60 tabletek", 40f, 34L },
                    { 77L, "Flavamed, 30 mg", "20 sztuk", 30f, 35L },
                    { 78L, "Prospan", "150 ml", 20f, 35L },
                    { 79L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 36L },
                    { 80L, "Vivacor 25 mg", "60 tabletek", 40f, 36L },
                    { 81L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 37L },
                    { 82L, "Vivacor 25 mg", "60 tabletek", 40f, 37L },
                    { 83L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 38L },
                    { 84L, "Vivacor 25 mg", "60 tabletek", 40f, 38L },
                    { 85L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 39L },
                    { 86L, "Vivacor 25 mg", "60 tabletek", 40f, 39L },
                    { 87L, "Zuccarin", "60 tabletek", 50f, 40L },
                    { 88L, "Thionerv 600", "30 tabletek", 60f, 40L },
                    { 89L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 41L },
                    { 90L, "Vivacor 25 mg", "60 tabletek", 40f, 41L },
                    { 91L, "Zuccarin", "60 tabletek", 50f, 42L },
                    { 92L, "Thionerv 600", "30 tabletek", 60f, 42L },
                    { 93L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 43L },
                    { 94L, "Vivacor 25 mg", "60 tabletek", 40f, 43L },
                    { 95L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 44L },
                    { 96L, "Vivacor 25 mg", "60 tabletek", 40f, 44L },
                    { 97L, "Zuccarin", "60 tabletek", 50f, 45L },
                    { 98L, "Thionerv 600", "30 tabletek", 60f, 45L },
                    { 99L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 46L },
                    { 100L, "Vivacor 25 mg", "60 tabletek", 40f, 46L },
                    { 101L, "Zuccarin", "60 tabletek", 50f, 47L },
                    { 102L, "Thionerv 600", "30 tabletek", 60f, 47L },
                    { 103L, "Flavamed, 30 mg", "20 sztuk", 30f, 48L },
                    { 104L, "Prospan", "150 ml", 20f, 48L },
                    { 105L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 49L },
                    { 106L, "Vivacor 25 mg", "60 tabletek", 40f, 49L },
                    { 107L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 50L }
                });

            migrationBuilder.InsertData(
                table: "IssuedMedicines",
                columns: new[] { "Id", "MedicineName", "PackageSize", "PaymentDiscount", "PrescriptionId" },
                values: new object[,]
                {
                    { 108L, "Nebbud 2 ml", "20 ampułek", 66f, 50L },
                    { 109L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 51L },
                    { 110L, "Vivacor 25 mg", "60 tabletek", 40f, 51L },
                    { 111L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 52L },
                    { 112L, "Nebbud 2 ml", "20 ampułek", 66f, 52L },
                    { 113L, "Lakcid", "20 tabletek", 30f, 53L },
                    { 114L, "Trilac Plus", "30 tabletek", 0f, 53L },
                    { 115L, "Enterol", "30 tabletek", 40f, 53L },
                    { 116L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 54L },
                    { 117L, "Vivacor 25 mg", "60 tabletek", 40f, 54L },
                    { 118L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 55L },
                    { 119L, "Vivacor 25 mg", "60 tabletek", 40f, 55L },
                    { 120L, "Nezyr 28 mg", "28 tabletek", 30f, 56L },
                    { 121L, "Apo-Pentox 400 SR 400 mg", "30 tabletek", 40f, 56L },
                    { 122L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 57L },
                    { 123L, "Vivacor 25 mg", "60 tabletek", 40f, 57L },
                    { 124L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 58L },
                    { 125L, "Vivacor 25 mg", "60 tabletek", 40f, 58L },
                    { 126L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 59L },
                    { 127L, "Vivacor 25 mg", "60 tabletek", 40f, 59L },
                    { 128L, "Zuccarin", "60 tabletek", 50f, 60L },
                    { 129L, "Thionerv 600", "30 tabletek", 60f, 60L },
                    { 130L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 61L },
                    { 131L, "Nebbud 2 ml", "20 ampułek", 66f, 61L },
                    { 132L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 62L },
                    { 133L, "Vivacor 25 mg", "60 tabletek", 40f, 62L },
                    { 134L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 63L },
                    { 135L, "Vivacor 25 mg", "60 tabletek", 40f, 63L },
                    { 136L, "Eltroxin", "60 tabletek", 30f, 64L },
                    { 137L, "Thyrozol", "40 tabletek", 10f, 64L },
                    { 138L, "Metoprolol", "100 tabletek", 40f, 64L },
                    { 139L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 65L },
                    { 140L, "Vivacor 25 mg", "60 tabletek", 40f, 65L },
                    { 141L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 66L },
                    { 142L, "Vivacor 25 mg", "60 tabletek", 40f, 66L },
                    { 143L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 67L },
                    { 144L, "Vivacor 25 mg", "60 tabletek", 40f, 67L },
                    { 145L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 68L },
                    { 146L, "Vivacor 25 mg", "60 tabletek", 40f, 68L },
                    { 147L, "Nezyr 28 mg", "28 tabletek", 30f, 69L },
                    { 148L, "Apo-Pentox 400 SR 400 mg", "30 tabletek", 40f, 69L },
                    { 149L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 70L }
                });

            migrationBuilder.InsertData(
                table: "IssuedMedicines",
                columns: new[] { "Id", "MedicineName", "PackageSize", "PaymentDiscount", "PrescriptionId" },
                values: new object[,]
                {
                    { 150L, "Vivacor 25 mg", "60 tabletek", 40f, 70L },
                    { 151L, "Iberogast", "100 ml", 20f, 71L },
                    { 152L, "Espumisan 40 mg", "100 tabletek", 20f, 71L },
                    { 153L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 72L },
                    { 154L, "Vivacor 25 mg", "60 tabletek", 40f, 72L },
                    { 155L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 73L },
                    { 156L, "Vivacor 25 mg", "60 tabletek", 40f, 73L },
                    { 157L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 74L },
                    { 158L, "Vivacor 25 mg", "60 tabletek", 40f, 74L },
                    { 159L, "Lakcid", "20 tabletek", 30f, 75L },
                    { 160L, "Trilac Plus", "30 tabletek", 0f, 75L },
                    { 161L, "Enterol", "30 tabletek", 40f, 75L },
                    { 162L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 76L },
                    { 163L, "Vivacor 25 mg", "60 tabletek", 40f, 76L },
                    { 164L, "Zuccarin", "60 tabletek", 50f, 77L },
                    { 165L, "Thionerv 600", "30 tabletek", 60f, 77L },
                    { 166L, "Lakcid", "20 tabletek", 30f, 78L },
                    { 167L, "Trilac Plus", "30 tabletek", 0f, 78L },
                    { 168L, "Enterol", "30 tabletek", 40f, 78L },
                    { 169L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 79L },
                    { 170L, "Vivacor 25 mg", "60 tabletek", 40f, 79L },
                    { 171L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 80L },
                    { 172L, "Vivacor 25 mg", "60 tabletek", 40f, 80L },
                    { 173L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 81L },
                    { 174L, "Vivacor 25 mg", "60 tabletek", 40f, 81L },
                    { 175L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 82L },
                    { 176L, "Nebbud 2 ml", "20 ampułek", 66f, 82L },
                    { 177L, "Iberogast", "100 ml", 20f, 83L },
                    { 178L, "Espumisan 40 mg", "100 tabletek", 20f, 83L },
                    { 179L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 84L },
                    { 180L, "Vivacor 25 mg", "60 tabletek", 40f, 84L },
                    { 181L, "Iberogast", "100 ml", 20f, 85L },
                    { 182L, "Espumisan 40 mg", "100 tabletek", 20f, 85L },
                    { 183L, "Zuccarin", "60 tabletek", 50f, 86L },
                    { 184L, "Thionerv 600", "30 tabletek", 60f, 86L },
                    { 185L, "Metformax", "60 tabletek", 30f, 87L },
                    { 186L, "Metformina", "50 tabletek", 40f, 87L },
                    { 187L, "Belosalic", "Buteleczka 100 ml", 40f, 87L },
                    { 188L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 88L },
                    { 189L, "Vivacor 25 mg", "60 tabletek", 40f, 88L },
                    { 190L, "Iberogast", "100 ml", 20f, 89L },
                    { 191L, "Espumisan 40 mg", "100 tabletek", 20f, 89L }
                });

            migrationBuilder.InsertData(
                table: "IssuedMedicines",
                columns: new[] { "Id", "MedicineName", "PackageSize", "PaymentDiscount", "PrescriptionId" },
                values: new object[,]
                {
                    { 192L, "Nezyr 28 mg", "28 tabletek", 30f, 90L },
                    { 193L, "Apo-Pentox 400 SR 400 mg", "30 tabletek", 40f, 90L },
                    { 194L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 91L },
                    { 195L, "Vivacor 25 mg", "60 tabletek", 40f, 91L },
                    { 196L, "Eltroxin", "60 tabletek", 30f, 92L },
                    { 197L, "Thyrozol", "40 tabletek", 10f, 92L },
                    { 198L, "Metoprolol", "100 tabletek", 40f, 92L },
                    { 199L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 93L },
                    { 200L, "Vivacor 25 mg", "60 tabletek", 40f, 93L },
                    { 201L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 94L },
                    { 202L, "Vivacor 25 mg", "60 tabletek", 40f, 94L },
                    { 203L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 95L },
                    { 204L, "Nebbud 2 ml", "20 ampułek", 66f, 95L },
                    { 205L, "Zuccarin", "60 tabletek", 50f, 96L },
                    { 206L, "Thionerv 600", "30 tabletek", 60f, 96L },
                    { 207L, "Zuccarin", "60 tabletek", 50f, 97L },
                    { 208L, "Thionerv 600", "30 tabletek", 60f, 97L },
                    { 209L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 98L },
                    { 210L, "Vivacor 25 mg", "60 tabletek", 40f, 98L },
                    { 211L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 99L },
                    { 212L, "Vivacor 25 mg", "60 tabletek", 40f, 99L },
                    { 213L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 100L },
                    { 214L, "Vivacor 25 mg", "60 tabletek", 40f, 100L },
                    { 215L, "Nezyr 28 mg", "28 tabletek", 30f, 101L },
                    { 216L, "Apo-Pentox 400 SR 400 mg", "30 tabletek", 40f, 101L },
                    { 217L, "Zuccarin", "60 tabletek", 50f, 102L },
                    { 218L, "Thionerv 600", "30 tabletek", 60f, 102L },
                    { 219L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 103L },
                    { 220L, "Nebbud 2 ml", "20 ampułek", 66f, 103L },
                    { 221L, "Lakcid", "20 tabletek", 30f, 104L },
                    { 222L, "Trilac Plus", "30 tabletek", 0f, 104L },
                    { 223L, "Enterol", "30 tabletek", 40f, 104L },
                    { 224L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 105L },
                    { 225L, "Vivacor 25 mg", "60 tabletek", 40f, 105L },
                    { 226L, "Iberogast", "100 ml", 20f, 106L },
                    { 227L, "Espumisan 40 mg", "100 tabletek", 20f, 106L },
                    { 228L, "Nezyr 28 mg", "28 tabletek", 30f, 107L },
                    { 229L, "Apo-Pentox 400 SR 400 mg", "30 tabletek", 40f, 107L },
                    { 230L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 108L },
                    { 231L, "Vivacor 25 mg", "60 tabletek", 40f, 108L },
                    { 232L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 109L },
                    { 233L, "Vivacor 25 mg", "60 tabletek", 40f, 109L }
                });

            migrationBuilder.InsertData(
                table: "IssuedMedicines",
                columns: new[] { "Id", "MedicineName", "PackageSize", "PaymentDiscount", "PrescriptionId" },
                values: new object[,]
                {
                    { 234L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 110L },
                    { 235L, "Vivacor 25 mg", "60 tabletek", 40f, 110L },
                    { 236L, "Lakcid", "20 tabletek", 30f, 111L },
                    { 237L, "Trilac Plus", "30 tabletek", 0f, 111L },
                    { 238L, "Enterol", "30 tabletek", 40f, 111L },
                    { 239L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 112L },
                    { 240L, "Vivacor 25 mg", "60 tabletek", 40f, 112L },
                    { 241L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 113L },
                    { 242L, "Vivacor 25 mg", "60 tabletek", 40f, 113L },
                    { 243L, "Zuccarin", "60 tabletek", 50f, 114L },
                    { 244L, "Thionerv 600", "30 tabletek", 60f, 114L },
                    { 245L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 115L },
                    { 246L, "Vivacor 25 mg", "60 tabletek", 40f, 115L },
                    { 247L, "Lakcid", "20 tabletek", 30f, 116L },
                    { 248L, "Trilac Plus", "30 tabletek", 0f, 116L },
                    { 249L, "Enterol", "30 tabletek", 40f, 116L },
                    { 250L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 117L },
                    { 251L, "Vivacor 25 mg", "60 tabletek", 40f, 117L },
                    { 252L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 118L },
                    { 253L, "Vivacor 25 mg", "60 tabletek", 40f, 118L },
                    { 254L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 119L },
                    { 255L, "Nebbud 2 ml", "20 ampułek", 66f, 119L },
                    { 256L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 120L },
                    { 257L, "Vivacor 25 mg", "60 tabletek", 40f, 120L },
                    { 258L, "Lakcid", "20 tabletek", 30f, 121L },
                    { 259L, "Trilac Plus", "30 tabletek", 0f, 121L },
                    { 260L, "Enterol", "30 tabletek", 40f, 121L },
                    { 261L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 122L },
                    { 262L, "Vivacor 25 mg", "60 tabletek", 40f, 122L },
                    { 263L, "Clexane 60 mg", "10 ampułko-strzykawek", 30f, 123L },
                    { 264L, "Nebbud 2 ml", "20 ampułek", 66f, 123L },
                    { 265L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 124L },
                    { 266L, "Vivacor 25 mg", "60 tabletek", 40f, 124L },
                    { 267L, "Iberogast", "100 ml", 20f, 125L },
                    { 268L, "Espumisan 40 mg", "100 tabletek", 20f, 125L },
                    { 269L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 126L },
                    { 270L, "Vivacor 25 mg", "60 tabletek", 40f, 126L },
                    { 271L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 127L },
                    { 272L, "Vivacor 25 mg", "60 tabletek", 40f, 127L },
                    { 273L, "Betaloc ZOK 100 mg", "28 tabletek", 30f, 128L },
                    { 274L, "Vivacor 25 mg", "60 tabletek", 40f, 128L },
                    { 275L, "Flavamed, 30 mg", "20 sztuk", 30f, 129L }
                });

            migrationBuilder.InsertData(
                table: "IssuedMedicines",
                columns: new[] { "Id", "MedicineName", "PackageSize", "PaymentDiscount", "PrescriptionId" },
                values: new object[,]
                {
                    { 276L, "Prospan", "150 ml", 20f, 129L },
                    { 277L, "Iberogast", "100 ml", 20f, 130L },
                    { 278L, "Espumisan 40 mg", "100 tabletek", 20f, 130L }
                });

            migrationBuilder.InsertData(
                table: "MedicalReferrals",
                columns: new[] { "Id", "Comment", "ExpireDate", "HasBeenUsed", "IssueDate", "IssuedById", "IssuedToId", "MinorMedicalServiceId", "PrimaryMedicalServiceId", "VisitId", "VisitWhenIssuedId", "VisitWhenUsedId" },
                values: new object[,]
                {
                    { 1L, null, new DateTimeOffset(new DateTime(2022, 11, 16, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2022, 8, 16, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), 0L, 0L, null, null, null, 0L, 0L },
                    { 2L, null, new DateTimeOffset(new DateTime(2022, 11, 16, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2022, 8, 13, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), 0L, 0L, null, null, null, 0L, 0L },
                    { 3L, null, new DateTimeOffset(new DateTime(2022, 11, 16, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2022, 8, 11, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), 0L, 0L, null, null, null, 0L, 0L },
                    { 4L, null, new DateTimeOffset(new DateTime(2022, 11, 16, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2022, 8, 10, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), 0L, 0L, null, null, null, 0L, 0L },
                    { 5L, null, new DateTimeOffset(new DateTime(2022, 11, 16, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2022, 8, 9, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), 0L, 0L, null, null, null, 0L, 0L },
                    { 6L, null, new DateTimeOffset(new DateTime(2022, 11, 16, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2022, 8, 8, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), 0L, 0L, null, null, null, 0L, 0L },
                    { 7L, null, new DateTimeOffset(new DateTime(2022, 11, 16, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2022, 8, 7, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), 0L, 0L, null, null, null, 0L, 0L },
                    { 8L, null, new DateTimeOffset(new DateTime(2022, 11, 16, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2022, 8, 7, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), 0L, 0L, null, null, null, 0L, 0L },
                    { 9L, null, new DateTimeOffset(new DateTime(2022, 11, 16, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2022, 8, 7, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), 0L, 0L, null, null, null, 0L, 0L },
                    { 10L, null, new DateTimeOffset(new DateTime(2022, 11, 16, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2022, 8, 7, 13, 29, 39, 958, DateTimeKind.Unspecified).AddTicks(6628), new TimeSpan(0, 2, 0, 0, 0)), 0L, 0L, null, null, null, 0L, 0L }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "DefaultAglomeration", "Gender", "HasPolishCitizenship", "ImageFilePath", "Name", "PESEL", "PassportCode", "PassportNumber", "PhoneNumber", "Surname", "ValidationError" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(1977, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 0, true, "/img/MW/m/1.jpg", "Mariusz", "77784512598", "POL", null, "777774377", "Puto", null },
                    { 2L, new DateTimeOffset(new DateTime(1965, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 0, true, "/img/MW/m/2.jpg", "Witold", "65101046546", "POL", null, "715772743", "Głąbek", null },
                    { 3L, new DateTimeOffset(new DateTime(1987, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 7, 0, true, "/img/MW/m/3.jpg", "Henryk", "87010256123", "POL", null, "715772743", "Bąbel", null },
                    { 4L, new DateTimeOffset(new DateTime(1956, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 6, 0, true, "/img/MW/m/4.jpg", "Ferdynand", "56050834534", "POL", null, "711272743", "Małolepszy", null },
                    { 5L, new DateTimeOffset(new DateTime(1954, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 3, 0, true, "/img/MW/m/5.jpg", "Zenon", "54020246454", "POL", null, "711272743", "Krzywy", null },
                    { 6L, new DateTimeOffset(new DateTime(1965, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 0, 0, true, "/img/MW/m/6.jpg", "Tadeusz", "65111176546", "POL", null, "711272743", "Nowak", null },
                    { 7L, new DateTimeOffset(new DateTime(1978, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 0, true, "/img/MW/m/7.jpg", "Tomasz", "78945646312", "POL", null, "711272743", "Woda", null },
                    { 8L, new DateTimeOffset(new DateTime(1975, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 0, true, "/img/MW/m/8.jpg", "Łukasz", "75654654646", "POL", null, "711272743", "Czekaj", null },
                    { 9L, new DateTimeOffset(new DateTime(1961, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 0, true, "/img/MW/m/9.jpg", "Dariusz", "61321234189", "POL", null, "711272743", "Dzwonek", null },
                    { 10L, new DateTimeOffset(new DateTime(1984, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 0, true, "/img/MW/m/10.jpg", "Mateusz", "84131321654", "POL", null, "712727717", "Chodzień", null },
                    { 11L, new DateTimeOffset(new DateTime(1944, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 0, true, "/img/MW/m/11.jpg", "Leszek", "44445465456", "POL", null, "712727717", "Ancymon", null },
                    { 12L, new DateTimeOffset(new DateTime(1975, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 8, 0, true, "/img/MW/m/12.jpg", "Karol", "75321231654", "POL", null, "712727717", "Szczęsny", null },
                    { 13L, new DateTimeOffset(new DateTime(1965, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 0, true, "/img/MW/m/13.jpg", "Remigiusz", "65421321564", "POL", null, "712727717", "Czystka", null },
                    { 14L, new DateTimeOffset(new DateTime(1979, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 0, true, "/img/MW/m/14.jpg", "Robert", "79887987545", "POL", null, "712727717", "Pawłowski", null },
                    { 15L, new DateTimeOffset(new DateTime(1971, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 0, true, "/img/MW/m/15.jpg", "Szymon", "71123156456", "POL", null, "712729717", "Sosna", null },
                    { 16L, new DateTimeOffset(new DateTime(1965, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 0, true, "/img/MW/m/16.jpg", "Sergiusz", "65231546456", "POL", null, "712729717", "Ząbek", null },
                    { 17L, new DateTimeOffset(new DateTime(1964, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 0, true, "/img/MW/m/17.jpg", "Tymoteusz", "64561231564", "POL", null, "712729717", "Zez", null },
                    { 18L, new DateTimeOffset(new DateTime(1945, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 0, true, "/img/MW/m/18.jpg", "Zbigniew", "45632132456", "POL", null, "712729718", "Korzeń", null },
                    { 19L, new DateTimeOffset(new DateTime(1949, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 0, true, "/img/MW/m/19.jpg", "Zbigniew", "49987945646", "POL", null, "712729718", "Osiński", null },
                    { 20L, new DateTimeOffset(new DateTime(1965, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 0, true, "/img/MW/m/20.jpg", "Michał", "65432154656", "POL", null, "712729718", "Czosnek", null },
                    { 21L, new DateTimeOffset(new DateTime(1980, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 0, true, "/img/MW/m/21.jpg", "Tomasz", "80121316546", "POL", null, "715729718", "Truteń", null },
                    { 22L, new DateTimeOffset(new DateTime(1955, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 0, true, "/img/MW/m/22.jpg", "Bogusław", "55465456412", "POL", null, "715729718", "Śmiały", null },
                    { 23L, new DateTimeOffset(new DateTime(1954, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 0, true, "/img/MW/m/23.jpg", "Jan", "54654321314", "POL", null, "715729778", "Dutki", null },
                    { 24L, new DateTimeOffset(new DateTime(1965, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 0, true, "/img/MW/m/24.jpg", "Jarosław", "65461234564", "POL", null, "715729778", "Kurczak", null },
                    { 25L, new DateTimeOffset(new DateTime(1965, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 0, true, "/img/MW/m/25.jpg", "Grzegorz", "65487456465", "POL", null, "715729778", "Grześkowiak", null },
                    { 26L, new DateTimeOffset(new DateTime(1945, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 8, 0, true, "/img/MW/m/26.jpg", "Gerwazy", "45612315646", "POL", null, "715729778", "Zasada", null },
                    { 27L, new DateTimeOffset(new DateTime(1954, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 0, true, "/img/MW/m/27.jpg", "Czesław", "54878975646", "POL", null, "715729778", "Wilk", null },
                    { 28L, new DateTimeOffset(new DateTime(1964, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 0, true, "/img/MW/m/28.jpg", "Tadeusz", "64621321564", "POL", null, "715729777", "Gąska", null },
                    { 29L, new DateTimeOffset(new DateTime(1959, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 0, true, "/img/MW/m/29.jpg", "Waldemar", "59456123156", "POL", null, "715729777", "Kucaj", null }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "DefaultAglomeration", "Gender", "HasPolishCitizenship", "ImageFilePath", "Name", "PESEL", "PassportCode", "PassportNumber", "PhoneNumber", "Surname", "ValidationError" },
                values: new object[,]
                {
                    { 30L, new DateTimeOffset(new DateTime(1978, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 0, true, "/img/MW/m/30.jpg", "Piotr", "78946513213", "POL", null, "715729777", "Kuropatwa", null },
                    { 31L, new DateTimeOffset(new DateTime(1978, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 0, true, "/img/MW/m/31.jpg", "Paweł", "78946546549", "POL", null, "715729777", "Łąkietka", null },
                    { 32L, new DateTimeOffset(new DateTime(1945, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 0, true, "/img/MW/m/32.jpg", "Rozmus", "45641341564", "POL", null, "715729777", "Remus", null },
                    { 33L, new DateTimeOffset(new DateTime(1948, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 8, 0, true, "/img/MW/m/33.jpg", "Miłosz", "48794564321", "POL", null, "715729777", "Ciapek", null },
                    { 34L, new DateTimeOffset(new DateTime(1965, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 1, true, "/img/MW/k/2.jpg", "Czesława", "65461231564", "POL", null, "715772977", "Kret", null },
                    { 35L, new DateTimeOffset(new DateTime(1989, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 1, true, "/img/MW/k/3.jpg", "Marlena", "89456113215", "POL", null, "715772977", "Bajka", null },
                    { 36L, new DateTimeOffset(new DateTime(1954, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 1, true, "/img/MW/k/4.jpg", "Bożena", "54564632165", "POL", null, "715772977", "Arbuz", null },
                    { 37L, new DateTimeOffset(new DateTime(1980, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 1, true, "/img/MW/k/5.jpg", "Klaudia", "80156465465", "POL", null, "715772977", "Kąkol", null },
                    { 38L, new DateTimeOffset(new DateTime(1986, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 7, 1, true, "/img/MW/k/6.jpg", "Sandra", "86465456464", "POL", null, "715772977", "Sosna", null },
                    { 39L, new DateTimeOffset(new DateTime(1951, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 6, 1, true, "/img/MW/k/7.jpg", "Teodora", "51564894651", "POL", null, "715772977", "Wiśniowiecka", null },
                    { 40L, new DateTimeOffset(new DateTime(1966, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 1, true, "/img/MW/k/8.jpg", "Kornelia", "66454564654", "POL", null, "715772977", "Krasicka", null },
                    { 41L, new DateTimeOffset(new DateTime(1975, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 1, true, "/img/MW/k/9.jpg", "Marzena", "75164546546", "POL", null, "715772977", "Rudnicka", null },
                    { 42L, new DateTimeOffset(new DateTime(1961, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 8, 1, true, "/img/MW/k/10.jpg", "Beata", "61231546546", "POL", null, "715729773", "Bomba", null },
                    { 43L, new DateTimeOffset(new DateTime(1971, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 1, true, "/img/MW/k/11.jpg", "Katarzyna", "71123456476", "POL", null, "715729773", "Łasinkiewicz", null },
                    { 44L, new DateTimeOffset(new DateTime(1981, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 1, true, "/img/MW/k/12.jpg", "Weronika", "81546546546", "POL", null, "715129773", "Kurzydło", null },
                    { 45L, new DateTimeOffset(new DateTime(1978, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 1, true, "/img/MW/k/13.jpg", "Maria", "78794654616", "POL", null, "715127773", "Kurka", null },
                    { 46L, new DateTimeOffset(new DateTime(1949, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 1, true, "/img/MW/k/14.jpg", "Bronisława", "49489646146", "POL", null, "715127773", "Czesiek", null },
                    { 47L, new DateTimeOffset(new DateTime(1965, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 1, true, "/img/MW/k/15.jpg", "Aleksandra", "65487987446", "POL", null, "715127773", "Ruda", null },
                    { 48L, new DateTimeOffset(new DateTime(1978, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 1, true, "/img/MW/k/16.jpg", "Iga", "78484654654", "POL", null, "715127773", "Bodzio", null },
                    { 49L, new DateTimeOffset(new DateTime(1984, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 1, true, "/img/MW/k/17.jpg", "Agnieszka", "84879486546", "POL", null, "715127773", "Pluto", null },
                    { 50L, new DateTimeOffset(new DateTime(1985, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 1, true, "/img/MW/k/18.jpg", "Karolina", "85641541321", "POL", null, "715127773", "Majak", null },
                    { 51L, new DateTimeOffset(new DateTime(1989, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 1, true, "/img/MW/k/19.jpg", "Karina", "89456411324", "POL", null, "715127773", "Wąsacz", null },
                    { 52L, new DateTimeOffset(new DateTime(1956, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 1, true, "/img/MW/k/20.jpg", "Grażyna", "56413215649", "POL", null, "715127373", "Rudniewska", null },
                    { 53L, new DateTimeOffset(new DateTime(1984, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 1, true, "/img/MW/k/21.jpg", "Marta", "84651654964", "POL", null, "715127373", "Tracka", null },
                    { 54L, new DateTimeOffset(new DateTime(1986, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 1, true, "/img/MW/k/22.jpg", "Marta", "86231165448", "POL", null, "715127373", "Trąbicka", null },
                    { 55L, new DateTimeOffset(new DateTime(1979, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 6, 1, true, "/img/MW/k/23.jpg", "Sylwia", "79132131564", "POL", null, "715127373", "Sarna", null },
                    { 56L, new DateTimeOffset(new DateTime(1975, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 1, true, "/img/MW/k/24.jpg", "Kamila", "75123165465", "POL", null, "715127373", "Kozera", null },
                    { 57L, new DateTimeOffset(new DateTime(1954, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 1, true, "/img/MW/k/25.jpg", "Bogumiła", "54878946123", "POL", null, "715127373", "Braniewska", null },
                    { 58L, new DateTimeOffset(new DateTime(1962, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 1, true, "/img/MW/k/26.jpg", "Teresa", "62348979521", "POL", null, "715127373", "Winniczek", null },
                    { 59L, new DateTimeOffset(new DateTime(1974, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 1, true, "/img/MW/k/27.jpg", "Daria", "74561213898", "POL", null, "715127373", "Jaszczur", null },
                    { 60L, new DateTimeOffset(new DateTime(1979, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 1, true, "/img/MW/k/28.jpg", "Daria", "79123156494", "POL", null, "715177373", "Biernacka", null },
                    { 61L, new DateTimeOffset(new DateTime(1978, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 1, true, "/img/MW/k/29.jpg", "Maria", "78532154645", "POL", null, "715177373", "Balon", null },
                    { 62L, new DateTimeOffset(new DateTime(1984, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 1, true, "/img/MW/k/30.jpg", "Anna", "84561321499", "POL", null, "715177373", "Poranna", null },
                    { 63L, new DateTimeOffset(new DateTime(1988, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 1, true, "/img/MW/k/31.jpg", "Anna", "88456413215", "POL", null, "715177373", "Poletko", null },
                    { 64L, new DateTimeOffset(new DateTime(1989, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 1, true, "/img/MW/k/32.jpg", "Agata", "89561321564", "POL", null, "715177373", "Bosko", null },
                    { 65L, new DateTimeOffset(new DateTime(1978, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 4, 1, true, "/img/MW/k/33.jpg", "Agata", "78465413131", "POL", null, "715177373", "Mińska", null },
                    { 66L, new DateTimeOffset(new DateTime(1980, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 1, true, "/img/MW/k/34.jpg", "Monika", "80156467513", "POL", null, "715177373", "Szajka", null },
                    { 67L, new DateTimeOffset(new DateTime(1979, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 7, 1, true, "/img/MW/k/35.jpg", "Mariola", "79856461321", "POL", null, "715177373", "Kiepska", null },
                    { 68L, new DateTimeOffset(new DateTime(1974, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 8, 1, true, "/img/MW/k/36.jpg", "Dorota", "74413212649", "POL", null, "715177377", "Zawisza", null },
                    { 69L, new DateTimeOffset(new DateTime(1988, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, 1, false, "/img/MW/k/37.jpg", "Anastasia", "88456123134", "UKR", "AAAA87946121646", "715177377", "Radczuk", null },
                    { 70L, new DateTimeOffset(new DateTime(1979, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 6, 1, true, "/img/MW/k/38.jpg", "Karolina", "79846513215", "POL", null, "715777377", "Kulka", null },
                    { 71L, new DateTimeOffset(new DateTime(1982, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 2, 1, false, "/img/MW/k/39.jpg", "Sonia", "82154698713", "UKR", "AAAA87946121646", "615772377", "Czapska", null }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "DefaultAglomeration", "Gender", "HasPolishCitizenship", "ImageFilePath", "Name", "PESEL", "PassportCode", "PassportNumber", "PhoneNumber", "Surname", "ValidationError" },
                values: new object[,]
                {
                    { 72L, new DateTimeOffset(new DateTime(1980, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 6, 1, true, "/img/MW/k/40.jpg", "Nina", "80846513215", "POL", null, "414577375", "Rączka", null },
                    { 73L, new DateTimeOffset(new DateTime(1991, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 6, 1, true, "/img/MW/k/41.jpg", "Karina", "91846533219", "POL", null, "315787311", "Kowalska", null },
                    { 74L, new DateTimeOffset(new DateTime(1949, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 3, 1, true, "/img/persons/uk8.jpg", "Bożena", "49111816546", null, null, "715747777", "Raj", null },
                    { 75L, new DateTimeOffset(new DateTime(1976, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 0, 0, true, "/img/persons/um6.jpg", "Fryderyk", "76121864984", null, null, "715747793", "Czyż", null },
                    { 76L, new DateTimeOffset(new DateTime(1982, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 1, true, "/img/persons/uk11.jpg", "Monika", "82090913215", null, null, "715477222", "Zalewska", null },
                    { 77L, new DateTimeOffset(new DateTime(1984, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, 1, true, "/img/persons/uk6.jpg", "Daria", "84061632131", null, null, "715747777", "Raszpan", null },
                    { 78L, new DateTimeOffset(new DateTime(1987, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0, 0, true, "/img/persons/um7.jpg", "Łukasz", "87101010105", "PL", "484654asd4a5sd4", "715475577", "Łuk", null },
                    { 79L, new DateTimeOffset(new DateTime(1974, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, 1, true, "/img/persons/uk1.jpg", "Magdalena", "74051256121", null, null, "715746772", "Bomba", null },
                    { 80L, new DateTimeOffset(new DateTime(1966, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 7, 1, true, "/img/persons/uk2.jpg", "Katarzyna", "66040865456", null, null, "715741778", "Jelitko", null },
                    { 81L, new DateTimeOffset(new DateTime(1979, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 9, 0, true, "/img/persons/um3.jpg", "Krzysztof", "79080546213", null, null, "715741237", "Kitka", null },
                    { 82L, new DateTimeOffset(new DateTime(1982, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 1, 0, true, "/img/persons/um2.jpg", "Dariusz", "82012464695", null, null, "515747787", "Czapa", null },
                    { 83L, new DateTimeOffset(new DateTime(1981, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 6, 0, true, "/img/persons/um5.jpg", "Tomasz", "81060754612", null, null, "415747747", "Komar", null },
                    { 84L, new DateTimeOffset(new DateTime(1979, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 8, 0, true, "/img/persons/um4.jpg", "Arkadiusz", "79102013465", null, null, "315747737", "Patka", null },
                    { 85L, new DateTimeOffset(new DateTime(1991, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 2, 1, true, "/img/persons/uk3.jpg", "Marta", "91021245646", null, null, "215747127", "Rakieta", null },
                    { 86L, new DateTimeOffset(new DateTime(1994, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 0, 1, true, "/img/persons/uk4.jpg", "Ada", "94121321654", null, null, "715741179", "Ruda", null },
                    { 87L, new DateTimeOffset(new DateTime(1954, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 4, 1, true, "/img/persons/uk5.jpg", "Genowefa", "54061324651", null, null, "715747771", "Pigwa", null },
                    { 88L, new DateTimeOffset(new DateTime(1955, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 0, 0, true, "/img/persons/um1.jpg", "Wacław", "55031365494", null, null, "915742775", "Kopytko", null },
                    { 89L, new DateTimeOffset(new DateTime(1990, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 6, 1, true, "/img/persons/uk9.jpg", "Agnieszka", "90011515676", null, null, "500365555", "Pielak", null },
                    { 90L, new DateTimeOffset(new DateTime(1970, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 8, 1, true, "/img/persons/uk13.jpg", "Monika", "70011515678", null, null, "430385094", "Krasicka", null },
                    { 91L, new DateTimeOffset(new DateTime(1988, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 2, 1, true, "/img/persons/uk14.jpg", "Maria", "88011515600", null, null, "530365915", "Kostacińska", null },
                    { 92L, new DateTimeOffset(new DateTime(2000, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 0, 1, true, "/img/persons/uk15.jpg", "Wanda", "00311515644", null, null, "540315526", "Nowak", null },
                    { 93L, new DateTimeOffset(new DateTime(1995, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 4, 1, true, "/img/persons/uk16.jpg", "Żaneta", "95211515624", null, null, "530364595", "Zielińska", null },
                    { 94L, new DateTimeOffset(new DateTime(1966, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 5, 1, true, "/img/persons/uk17.jpg", "Renata", "66111515668", null, null, "560325595", "Molska", null },
                    { 95L, new DateTimeOffset(new DateTime(1977, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 7, 0, true, "/img/persons/um8.jpg", "Radosław", "77013015655", null, null, "501325593", "Gręda", null },
                    { 96L, new DateTimeOffset(new DateTime(1982, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 9, 0, true, "/img/persons/um9.jpg", "Robert", "82014515633", null, null, "505364573", "Sapierzyński", null },
                    { 97L, new DateTimeOffset(new DateTime(1980, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 6, 0, true, "/img/persons/um10.jpg", "Paweł", "80011513671", null, null, "507361553", "Tryfon", null },
                    { 98L, new DateTimeOffset(new DateTime(1991, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 1, 0, true, "/img/persons/um11.jpg", "Tomasz", "91011415679", null, null, "601365563", "Oniśk", null },
                    { 99L, new DateTimeOffset(new DateTime(1996, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 1, 0, true, "/img/persons/um12.jpg", "Mateusz", "96011215631", null, null, "701362551", "Skorupka", null },
                    { 100L, new DateTimeOffset(new DateTime(1972, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 2, 0, true, "/img/persons/um13.jpg", "Robert", "72011525627", null, null, "401361353", "Krzycki", null },
                    { 101L, new DateTimeOffset(new DateTime(1997, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 1, 0, true, "/img/persons/um14.jpg", "Tomasz", "97011215631", null, null, "854612314", "Janiga", null },
                    { 102L, new DateTimeOffset(new DateTime(1988, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 0, 0, true, "/img/persons/um15.jpg", "Rafał", "88011525627", null, null, "795161304", "Fabisiak", null }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "EmployerNIP", "EmployerName", "MedicalPackageId", "NFZUnitId", "PersonId", "UserId" },
                values: new object[,]
                {
                    { 1L, "845465154654", null, 1L, 1L, 78L, 78L },
                    { 2L, "7777742132152", null, 4L, 16L, 79L, 79L },
                    { 3L, "549642132152", null, 1L, 1L, 80L, 80L },
                    { 4L, "549642132152", null, 2L, 2L, 81L, 81L },
                    { 5L, "7777742132152", null, 3L, 3L, 82L, 82L },
                    { 6L, "7777742132152", null, 4L, 4L, 83L, 83L },
                    { 7L, "7777742132152", null, 2L, 5L, 84L, 84L },
                    { 8L, "7777742132152", null, 1L, 6L, 85L, 85L },
                    { 9L, "7777742132152", null, 3L, 3L, 86L, 86L },
                    { 10L, "7777742132152", null, 4L, 8L, 87L, 87L },
                    { 11L, "984891621654", "Styropmin", 3L, 9L, 88L, 88L },
                    { 12L, "54646516465", "UM Ząbki", 3L, 9L, 89L, 89L },
                    { 13L, "7777742132152", "PKP Intercity", 2L, 8L, 90L, 90L },
                    { 14L, "54646516465", "Biedronka", 2L, 3L, 91L, 91L },
                    { 15L, "7777742132152", "McKinsey Polska Sp. z o.o.", 1L, 11L, 92L, 92L },
                    { 16L, "4657964654654", "Coca Cola", 3L, 6L, 93L, 93L },
                    { 17L, "63123154654", "Apple Sp. z o.o.", 3L, 5L, 94L, 94L },
                    { 18L, "1324564895413", "Samsung Polska", 4L, 4L, 95L, 95L },
                    { 19L, "13648946542", "Dell Polska S.A.", 1L, 3L, 96L, 96L },
                    { 20L, "161315648635", "CCC Sp. z o.o.", 2L, 16L, 97L, 97L },
                    { 21L, "87841213654", "Henkel S.A.", 3L, 12L, 98L, 98L },
                    { 22L, "123165498463", "LOT S.A.", 4L, 11L, 99L, 99L },
                    { 23L, "94215748989", "Alior Bank S.A.", 1L, 13L, 100L, 100L },
                    { 24L, "89461231651", "Nestle Polska", 2L, 14L, 101L, 101L },
                    { 25L, "465465461231", "Wedel sp. z o.o.", 3L, 15L, 102L, 102L }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_IssuedMedicines_Prescriptions_PrescriptionId",
                table: "IssuedMedicines",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_MedicalPackages_MedicalPackageId",
                table: "Patients",
                column: "MedicalPackageId",
                principalTable: "MedicalPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssuedMedicines_Prescriptions_PrescriptionId",
                table: "IssuedMedicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MedicalPackages_MedicalPackageId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssuedMedicines",
                table: "IssuedMedicines");

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 126L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 127L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 128L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 129L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 130L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 131L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 132L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 133L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 134L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 135L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 136L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 137L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 138L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 139L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 140L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 141L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 142L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 143L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 144L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 145L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 146L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 147L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 148L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 149L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 150L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 151L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 152L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 153L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 154L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 155L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 156L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 157L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 158L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 159L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 160L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 161L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 162L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 163L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 164L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 165L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 166L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 167L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 168L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 169L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 170L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 171L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 172L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 173L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 174L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 175L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 176L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 177L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 178L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 179L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 180L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 181L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 182L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 183L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 184L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 185L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 186L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 187L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 188L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 189L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 190L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 191L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 192L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 193L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 194L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 195L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 196L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 197L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 198L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 199L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 200L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 201L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 202L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 203L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 204L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 205L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 206L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 207L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 208L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 209L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 210L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 211L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 212L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 213L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 214L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 215L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 216L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 217L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 218L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 219L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 220L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 221L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 222L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 223L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 224L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 225L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 226L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 227L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 228L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 229L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 230L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 231L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 232L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 233L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 234L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 235L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 236L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 237L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 238L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 239L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 240L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 241L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 242L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 243L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 244L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 245L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 246L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 247L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 248L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 249L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 250L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 251L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 252L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 253L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 254L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 255L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 256L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 257L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 258L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 259L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 260L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 261L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 262L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 263L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 264L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 265L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 266L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 267L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 268L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 269L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 270L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 271L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 272L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 273L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 274L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 275L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 276L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 277L);

            migrationBuilder.DeleteData(
                table: "IssuedMedicines",
                keyColumn: "Id",
                keyValue: 278L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 126L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 127L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 128L);

            migrationBuilder.DeleteData(
                table: "MedicalReferrals",
                keyColumn: "Id",
                keyValue: 129L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                table: "MedicalTestResults",
                keyColumn: "Id",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 126L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 127L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 128L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 129L);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 130L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "MedicalTestResults");

            migrationBuilder.RenameTable(
                name: "IssuedMedicines",
                newName: "IssuedMedicine");

            migrationBuilder.RenameIndex(
                name: "IX_IssuedMedicines_PrescriptionId",
                table: "IssuedMedicine",
                newName: "IX_IssuedMedicine_PrescriptionId");

            migrationBuilder.AlterColumn<long>(
                name: "PrescriptionId",
                table: "IssuedMedicine",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssuedMedicine",
                table: "IssuedMedicine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssuedMedicine_Prescriptions_PrescriptionId",
                table: "IssuedMedicine",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_MedicalPackages_MedicalPackageId",
                table: "Patients",
                column: "MedicalPackageId",
                principalTable: "MedicalPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

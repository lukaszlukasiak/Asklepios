using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Data.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aglomeration = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAndNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalPackages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NFZUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voivodeship = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NFZUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    HasPolishCitizenship = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DefaultAglomeration = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationError = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRooms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorNumber = table.Column<short>(type: "smallint", nullable: false),
                    MedicalRoomType = table.Column<int>(type: "int", nullable: false),
                    LocationId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRooms_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalRooms_Locations_LocationId1",
                        column: x => x.LocationId1,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: true),
                    WorkerModuleType = table.Column<int>(type: "int", nullable: true),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    MedicalWorkerId = table.Column<long>(type: "bigint", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalWorkers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProfessionalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HiredSince = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCurrentlyHired = table.Column<bool>(type: "bit", nullable: false),
                    MedicalWorkerType = table.Column<int>(type: "int", nullable: false),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalWorkers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalWorkers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    MedicalPackageId = table.Column<long>(type: "bigint", nullable: false),
                    NFZUnitId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerNIP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_MedicalPackages_MedicalPackageId",
                        column: x => x.MedicalPackageId,
                        principalTable: "MedicalPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_NFZUnits_NFZUnitId",
                        column: x => x.NFZUnitId,
                        principalTable: "NFZUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IssuedById = table.Column<long>(type: "bigint", nullable: false),
                    IssuedToId = table.Column<long>(type: "bigint", nullable: false),
                    AccessCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    IdentificationCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_MedicalWorkers_IssuedById",
                        column: x => x.IssuedById,
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_IssuedToId",
                        column: x => x.IssuedToId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitReviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralRate = table.Column<float>(type: "real", nullable: false),
                    CompetenceRate = table.Column<float>(type: "real", nullable: false),
                    AtmosphereRate = table.Column<float>(type: "real", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RevieweeId = table.Column<long>(type: "bigint", nullable: false),
                    ReviewerId = table.Column<long>(type: "bigint", nullable: false),
                    VisitId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitReviews_MedicalWorkers_RevieweeId",
                        column: x => x.RevieweeId,
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitReviews_Patients_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssuedMedicine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDiscount = table.Column<float>(type: "real", nullable: false),
                    PrescriptionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuedMedicine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssuedMedicine_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalReferrals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimaryMedicalServiceId = table.Column<long>(type: "bigint", nullable: true),
                    MinorMedicalServiceId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpireDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    HasBeenUsed = table.Column<bool>(type: "bit", nullable: false),
                    IssuedById = table.Column<long>(type: "bigint", nullable: false),
                    IssuedToId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitWhenUsedId = table.Column<long>(type: "bigint", nullable: false),
                    VisitWhenIssuedId = table.Column<long>(type: "bigint", nullable: false),
                    VisitId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalReferrals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalReferrals_MedicalWorkers_IssuedById",
                        column: x => x.IssuedById,
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalReferrals_Patients_IssuedToId",
                        column: x => x.IssuedToId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalServiceDiscount",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MedicalPackageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalServiceDiscount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalServiceDiscount_MedicalPackages_MedicalPackageId",
                        column: x => x.MedicalPackageId,
                        principalTable: "MedicalPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalServiceMedicalWorker",
                columns: table => new
                {
                    MedicalServicesId = table.Column<long>(type: "bigint", nullable: false),
                    MedicalWorkersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalServiceMedicalWorker", x => new { x.MedicalServicesId, x.MedicalWorkersId });
                    table.ForeignKey(
                        name: "FK_MedicalServiceMedicalWorker_MedicalWorkers_MedicalWorkersId",
                        column: x => x.MedicalWorkersId,
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalServices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StandardPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrimaryService = table.Column<bool>(type: "bit", nullable: false),
                    RequireRefferal = table.Column<bool>(type: "bit", nullable: false),
                    PrimaryServiceId = table.Column<long>(type: "bigint", nullable: true),
                    PrimaryMedicalServiceId = table.Column<long>(type: "bigint", nullable: true),
                    VisitCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    VisitId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalServices_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalServices_MedicalServices_PrimaryMedicalServiceId",
                        column: x => x.PrimaryMedicalServiceId,
                        principalTable: "MedicalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalServices_VisitCategories_VisitCategoryId",
                        column: x => x.VisitCategoryId,
                        principalTable: "VisitCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalTestResults",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Document = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalWorkerId = table.Column<long>(type: "bigint", nullable: false),
                    ExamDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UploadDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalTestResults_MedicalServices_MedicalServiceId",
                        column: x => x.MedicalServiceId,
                        principalTable: "MedicalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalTestResults_MedicalWorkers_MedicalWorkerId",
                        column: x => x.MedicalWorkerId,
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalTestResults_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    VisitStatus = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    MedicalWorkerId = table.Column<long>(type: "bigint", nullable: false),
                    DateTimeSince = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateTimeTill = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PrimaryServiceId = table.Column<long>(type: "bigint", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    MedicalRoomId = table.Column<long>(type: "bigint", nullable: false),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalResultId = table.Column<long>(type: "bigint", nullable: false),
                    PrescriptionId = table.Column<long>(type: "bigint", nullable: false),
                    UsedExaminationReferralId = table.Column<long>(type: "bigint", nullable: false),
                    VisitReviewId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_MedicalReferrals_UsedExaminationReferralId",
                        column: x => x.UsedExaminationReferralId,
                        principalTable: "MedicalReferrals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_MedicalRooms_MedicalRoomId",
                        column: x => x.MedicalRoomId,
                        principalTable: "MedicalRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_MedicalServices_PrimaryServiceId",
                        column: x => x.PrimaryServiceId,
                        principalTable: "MedicalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_MedicalTestResults_MedicalResultId",
                        column: x => x.MedicalResultId,
                        principalTable: "MedicalTestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_MedicalWorkers_MedicalWorkerId",
                        column: x => x.MedicalWorkerId,
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_VisitCategories_VisitCategoryId",
                        column: x => x.VisitCategoryId,
                        principalTable: "VisitCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_VisitReviews_VisitReviewId",
                        column: x => x.VisitReviewId,
                        principalTable: "VisitReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    DateTimeAdded = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    VisitId = table.Column<long>(type: "bigint", nullable: false),
                    EventObjectId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recommendation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendation_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Aglomeration", "City", "Description", "ImagePath", "Name", "PhoneNumber", "PostalCode", "StreetAndNumber" },
                values: new object[,]
                {
                    { 1L, 0, "Warszawa", "Ośrodek w centrum Warszawy ze świetnym dojazdem z każdej dzielnicy.", "/img/Locations/loc1.jpeg", "Ośrodek Warszawa Jerozolimskie", "22 780 421 433", "01-111", "Jerozolimskie 80" },
                    { 2L, 0, "Warszawa", "Ośrodek w Warszawie w dzielnicy Ochota, z bardzo dobrym dojazdem z zachodniej części Warszawy.", "/img/Locations/loc2.jpg", "Ośrodek Warszawa Ochota", "22 787 477 323", "01-211", "Grójecka 100" },
                    { 3L, 0, "Warszawa", "Ośrodek na południu Warszawy ze świetnym dojazdem z południa Warszawy oraz regionów wzdłuż M1 oraz południowych okolic Warszawy.", "/img/Locations/loc3.jpg", "Ośrodek Warszawa Ursynów", "22 777 600 313", "03-055", "KEN 20" },
                    { 4L, 0, "Warszawa", "Ośrodek na wschodzie Warszawy z dobrym dojazdem ze wschodnich dzielnic Warszawy a także wschodnich okolic Warszawy.", "/img/Locations/loc4.jpg", "Ośrodek Warszawa Targówek", "22 777 444 333", "02-222", "Malborska" },
                    { 5L, 3, "Kraków", "Ośrodek w Krakowie, w świetnie skomunikowanym Kazimierzu", "/img/Locations/loc5.jpg", "Ośrodek Kraków Pogórze", "20 300 400 111", "80-078", "Podgórska 14" },
                    { 6L, 2, "Gdańsk", "Ośrodek w centrum Gdańska na popularnej Wyspie Spichrzów", "/img/Locations/loc6.jpg", "Ośrodek Gdańsk Wyspa Spichrzów", "30 500 500 241", "45-100", "Chlebnicka 11" },
                    { 7L, 1, "Poznań", "Ośrodek położony na terenie Galerie Malta Poznań", "/img/locations/loc7.jpg", "Ośrodek Poznań Malta", "30 500 500 241", "60-102", "Maltańska 1" },
                    { 8L, 4, "Wrocław", "Placówka położona nieco na wschód od ścisłego centrum. Łatwo do niej trafić, idąc prosto od strony placu Grunwaldzkiego.", "/img/locations/loc8.jpg", "Ośrodek Wrocław Szczytnicka", "71 500 500 241", "50-031", "Szczytnicka 11" },
                    { 9L, 8, "Katowice", "Ośrodek położony w bliskiej okolicy dworca PKP oraz Placu Wolności", "/img/locations/loc9.jpg", "Ośrodek Kopalnia Katowice", "32 500 500 241", "40-750", "Młyńska 23" }
                });

            migrationBuilder.InsertData(
                table: "MedicalPackages",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, "Podstawowy pakiet dla osób szukajacych podstawowej opieki zdrowotnej. W cenie pakietu są zawarte bezpłatne konsultacje z 7 specjalizacji oraz podstawowe badania", "Podstawowy" },
                    { 2L, "Srebrny pakiet jest pakietem dla osób szukajacych rozszerzonej opieki zdrowotnej. W ramach abonamentu medycznego są darmowe konsultacje u większości specjalistów, rozszerzony pakiet badań medycznych oraz 3 wizyty rehabilitacyjnE rocznie.", "Srebrny" },
                    { 3L, "Złoty pakiet dla osób szukajacych specjalistycznej opieki, w tym opieki dentystycznej oraz rehabilitacji.", "Złoty" },
                    { 4L, "Platynowy pakiet jest pakietem dla osób szukajacych pełnej ochrony zdrowia. Wszystkie oferowane przez nas usługi są oferowane nieodpłatnie. Priorytetowa obsługa w przypadku badań/operacji niecierpiących zwłoki. ", "Platynowy" }
                });

            migrationBuilder.InsertData(
                table: "MedicalServices",
                columns: new[] { "Id", "Description", "IsPrimaryService", "LocationId", "Name", "PrimaryMedicalServiceId", "PrimaryServiceId", "RequireRefferal", "StandardPrice", "VisitCategoryId", "VisitId" },
                values: new object[,]
                {
                    { 5L, "Komputerowe pole widzenia", false, null, "Komputerowe pole widzenia", null, null, false, 200m, 0L, null },
                    { 6L, "Kolonoskopia", false, null, "Kolonoskopia", null, 16L, false, 200m, 0L, null },
                    { 7L, "Audiometria", false, null, "Audiometria", null, 53L, false, 200m, 0L, null },
                    { 8L, "Gastroskopia", false, null, "Gastroskopia", null, 16L, false, 200m, 0L, null },
                    { 9L, "Założenie gipsu", false, null, "Założenie gipsu", null, 43L, false, 200m, 0L, null },
                    { 10L, "Usunięcie paznokcia", false, null, "Usunięcie paznokcia", null, 48L, false, 100m, 0L, null },
                    { 11L, "Piaskowanie", false, null, "Piaskowanie", null, 62L, false, 100m, 0L, null },
                    { 12L, "Fluoryzacja", false, null, "Fluoryzacja", null, 62L, false, 100m, 0L, null },
                    { 13L, "Usunięcie ósemki", false, null, "Usunięcie ósemki", null, 59L, false, 100m, 0L, null },
                    { 14L, "Usunięcie zęba jednokorzeniowego", false, null, "Usunięcie zęba jednokorzeniowego", null, 59L, false, 100m, 0L, null },
                    { 15L, "Usunięcie zęba jednokorzeniowego wielokorzeniowego", false, null, "Usunięcie zęba jednokorzeniowego wielokorzeniowego", null, 59L, false, 100m, 0L, null },
                    { 16L, "Usunięcie zęba mlecznego", false, null, "Usunięcie zęba mlecznego", null, 59L, false, 100m, 0L, null },
                    { 17L, "Pantomogram zęba", false, null, "Pantomogram zęba", null, 60L, false, 100m, 0L, null },
                    { 18L, "Tomografia komputerowa CBCT", false, null, "Tomografia komputerowa CBCT", null, 60L, false, 100m, 0L, null },
                    { 19L, "Znieczulenie", false, null, "Znieczulenie", null, 57L, false, 50m, 0L, null },
                    { 20L, "Wypełnienie czasowe", false, null, "Wypełnienie czasowe", null, 57L, false, 50m, 0L, null },
                    { 21L, "Wypełnienie kompozytowe", false, null, "Wypełnienie kompozytowe", null, 57L, false, 200m, 0L, null },
                    { 22L, "Odbudowa zęba po leczeniu kanałowym", false, null, "Odbudowa zęba po leczeniu kanałowym", null, 57L, false, 400m, 0L, null },
                    { 23L, "Dewitalizacja", false, null, "Dewitalizacja", null, 57L, false, 100m, 0L, null },
                    { 24L, "Podstawowe badanie krwi", false, null, "Podstawowe badanie krwi", null, null, false, 400m, 0L, null },
                    { 25L, "Rozszerzone badanie krwi", false, null, "Rozszerzone badanie krwi", null, null, false, 400m, 0L, null },
                    { 26L, "Badanie moczu", false, null, "Badanie moczu", null, null, false, 400m, 0L, null },
                    { 27L, "Badanie kału", false, null, "Badanie kału", null, null, false, 400m, 0L, null },
                    { 28L, "Test genetyczny COVID-19", false, null, "Test genetyczny COVID-19", null, null, false, 400m, 0L, null },
                    { 29L, "Test antygenowy COVID-19", false, null, "Test antygenowy COVID-19", null, null, false, 400m, 0L, null },
                    { 32L, "Krioterapia", false, null, "Krioterapia", null, 75L, false, 100m, 0L, null },
                    { 33L, "Elektrostymulacja", false, null, "Elektrostymulacja", null, 75L, false, 100m, 0L, null },
                    { 34L, "Laseroterapia", false, null, "Laseroterapia", null, 75L, false, 200m, 0L, null },
                    { 35L, "Ultradźwięki", false, null, "Ultradźwięki", null, 75L, false, 100m, 0L, null }
                });

            migrationBuilder.InsertData(
                table: "MedicalServices",
                columns: new[] { "Id", "Description", "IsPrimaryService", "LocationId", "Name", "PrimaryMedicalServiceId", "PrimaryServiceId", "RequireRefferal", "StandardPrice", "VisitCategoryId", "VisitId" },
                values: new object[,]
                {
                    { 36L, "Magnetoterapia", false, null, "Magnetoterapia", null, 75L, false, 100m, 0L, null },
                    { 37L, "Płukanie ucha", false, null, "Płukanie ucha", null, 74L, false, 50m, 0L, null },
                    { 63L, "Korona porcelanowa", false, null, "Korona porcelanowa", null, 74L, false, 800m, 0L, null },
                    { 64L, "Licówka porcelanowa", false, null, "Licówka porcelanowa", null, 74L, false, 1600m, 0L, null },
                    { 65L, "Korona pełnoceramiczna", false, null, "Korona pełnoceramiczna", null, 74L, false, 1600m, 0L, null },
                    { 66L, "Szczepienie na odrę", false, null, "Szczepienie na odrę", null, 76L, false, 100m, 0L, null },
                    { 67L, "Szczepienie na grypę", false, null, "Szczepienie na grypę", null, 76L, false, 100m, 0L, null },
                    { 68L, "Szczepienie na COVID-19", false, null, "Szczepienie na COVID-19", null, 76L, false, 200m, 0L, null },
                    { 69L, "Szczepienie przeciwko wściekliźnie", false, null, "Szczepienie przeciwko wściekliźnie", null, 76L, false, 200m, 0L, null },
                    { 70L, "Szczepienie przeciwko tężcowi", false, null, "Szczepienie przeciwko tężcowi", null, 76L, false, 100m, 0L, null },
                    { 71L, "Szczepienie przeciwko malarii", false, null, "Szczepienie przeciwko malarii", null, 76L, false, 500m, 0L, null },
                    { 72L, "Szczepienie przeciwko cholerze", false, null, "Szczepienie przeciwko cholerze", null, 76L, false, 100m, 0L, null },
                    { 77L, "Aparat stały kryształowy", false, null, "Aparat stały kryształowy", null, 74L, false, 2500m, 0L, null },
                    { 78L, "Aparat stały metalowy", false, null, "Aparat stały metalowy", null, 74L, false, 100m, 0L, null },
                    { 79L, "Aparat ruchomy - płytka Schwarza", false, null, "Aparat ruchomy - płytka Schwarza", null, 74L, false, 100m, 0L, null },
                    { 80L, "Topografia rogówki", false, null, "Topografia rogówki", null, 74L, false, 100m, 0L, null },
                    { 81L, "Dobór soczewek kontaktowych", false, null, "Dobór soczewek kontaktowych", null, 74L, false, 150m, 0L, null },
                    { 82L, "Zdjęcie dna oka", false, null, "Zdjęcie dna oka", null, 74L, false, 50m, 0L, null },
                    { 83L, "Pachymetria", false, null, "Pachymetria", null, 74L, false, 100m, 0L, null },
                    { 84L, "Pomiar ciśnienia wewnątrzgałkowego", false, null, "Pomiar ciśnienia wewnątrzgałkowego	", null, 74L, false, 50m, 0L, null },
                    { 85L, "Zdjęcie gipsu", false, null, "Zdjęcie gipsu", null, 74L, false, 100m, 0L, null },
                    { 86L, "Szycie rany", false, null, "Szycie rany", null, 74L, false, 100m, 0L, null },
                    { 87L, "Założenie szwów", false, null, "Założenie szwów", null, 74L, false, 100m, 0L, null },
                    { 88L, "Zdjęcie szwów", false, null, "Zdjęcie szwów", null, 74L, false, 100m, 0L, null },
                    { 89L, "Zabieg usunięcia ciała obcego", false, null, "Zabieg usunięcia ciała obcego", null, 74L, false, 600m, 0L, null },
                    { 90L, "Biopsja otwarta", false, null, "Biopsja otwarta", null, 74L, false, 600m, 0L, null },
                    { 91L, "EKG spoczynkowe", false, null, "EKG spoczynkowe", null, 74L, false, 200m, 0L, null },
                    { 92L, "EKG wysiłkowe", false, null, "EKG wysiłkowe", null, 74L, false, 200m, 0L, null },
                    { 93L, "Echo serca", false, null, "Echo serca", null, 74L, false, 200m, 0L, null },
                    { 94L, "Badanie cholesterolu", false, null, "Badanie cholesterolu", null, 74L, true, 100m, 0L, null },
                    { 95L, "USG Dopplera", false, null, "USG Dopplera", null, 74L, true, 150m, 0L, null },
                    { 96L, "Anoskopia", false, null, "Anoskopia", null, 38L, true, 100m, 0L, null },
                    { 97L, "Rektoskopia", false, null, "Rektoskopia", null, 38L, true, 150m, 0L, null },
                    { 98L, "Kolanoskopia", false, null, "Kolanoskopia", null, 38L, true, 200m, 0L, null }
                });

            migrationBuilder.InsertData(
                table: "NFZUnits",
                columns: new[] { "Id", "Code", "Description", "Voivodeship" },
                values: new object[,]
                {
                    { 1L, "DLŚ", "Dolnośląski Fundusz Zdrowia", 0 },
                    { 2L, "KPM", "Kujawsko-Pomorski Fundusz Zdrowia", 0 },
                    { 3L, "LBL", "Lubelski Fundusz Zdrowia", 0 },
                    { 4L, "LBS", "Lubuski Fundusz Zdrowia", 0 },
                    { 5L, "ŁDZ", "Łódzki Fundusz Zdrowia", 0 },
                    { 6L, "MŁP", "Małopolski Fundusz Zdrowia", 0 },
                    { 7L, "MAZ", "Mazowiecki Fundusz Zdrowia", 0 },
                    { 8L, "OPO", "Opolski Fundusz Zdrowia", 0 }
                });

            migrationBuilder.InsertData(
                table: "NFZUnits",
                columns: new[] { "Id", "Code", "Description", "Voivodeship" },
                values: new object[,]
                {
                    { 9L, "PDK", "Podkarpacki Fundusz Zdrowia", 0 },
                    { 10L, "PDL", "Podlaski Fundusz Zdrowia", 0 },
                    { 11L, "POM", "Pomorski Fundusz Zdrowia", 0 },
                    { 12L, "ŚLĄ", "Śląski Fundusz Zdrowia", 0 },
                    { 13L, "ŚWI", "Świętokrzyski Fundusz Zdrowia", 0 },
                    { 14L, "WAM", "Warmińsko-Mazurski Fundusz Zdrowia", 0 },
                    { 15L, "WLP", "Wielkopolski Fundusz Zdrowia", 0 },
                    { 16L, "ZAP", "Zachodniopomorski Fundusz Zdrowia", 0 }
                });

            migrationBuilder.InsertData(
                table: "VisitCategories",
                columns: new[] { "Id", "CategoryName", "Type" },
                values: new object[,]
                {
                    { 1L, "Konsultacje stacjonarne", 0 },
                    { 2L, "E-konsultacje", 0 },
                    { 3L, "Stomatologia", 0 },
                    { 4L, "Diagnostyka obrazowa ", 0 },
                    { 5L, "Fizjoterapia", 0 },
                    { 6L, "Gabinet zabiegowy", 0 }
                });

            migrationBuilder.InsertData(
                table: "MedicalRooms",
                columns: new[] { "Id", "FloorNumber", "LocationId", "LocationId1", "MedicalRoomType", "Name" },
                values: new object[,]
                {
                    { 1L, (short)0, 1L, null, 1, "1" },
                    { 2L, (short)0, 1L, null, 8, "2" },
                    { 3L, (short)0, 1L, null, 0, "3" },
                    { 4L, (short)0, 1L, null, 0, "4" },
                    { 5L, (short)0, 1L, null, 2, "5" },
                    { 6L, (short)0, 1L, null, 3, "6" },
                    { 7L, (short)0, 1L, null, 11, "7" },
                    { 8L, (short)0, 1L, null, 7, "8" },
                    { 9L, (short)0, 1L, null, 6, "9" },
                    { 10L, (short)0, 1L, null, 9, "10" },
                    { 11L, (short)0, 1L, null, 9, "11" },
                    { 12L, (short)0, 1L, null, 10, "12" },
                    { 13L, (short)1, 2L, null, 4, "1A" },
                    { 14L, (short)1, 2L, null, 5, "1B" },
                    { 15L, (short)1, 2L, null, 0, "1C" },
                    { 16L, (short)1, 2L, null, 0, "1D" },
                    { 17L, (short)1, 2L, null, 3, "1E" },
                    { 18L, (short)2, 2L, null, 1, "2A" },
                    { 19L, (short)2, 2L, null, 11, "2B" },
                    { 20L, (short)2, 2L, null, 6, "2C" },
                    { 21L, (short)2, 2L, null, 0, "2D" },
                    { 22L, (short)3, 2L, null, 0, "3A" },
                    { 23L, (short)3, 2L, null, 5, "3B" },
                    { 24L, (short)3, 2L, null, 1, "3C" },
                    { 25L, (short)3, 2L, null, 0, "3D" },
                    { 26L, (short)4, 3L, null, 5, "41" },
                    { 27L, (short)4, 3L, null, 5, "42" },
                    { 28L, (short)4, 3L, null, 0, "43" },
                    { 29L, (short)4, 3L, null, 11, "44" },
                    { 30L, (short)4, 3L, null, 0, "45" },
                    { 31L, (short)4, 3L, null, 1, "46" },
                    { 32L, (short)4, 3L, null, 2, "47" },
                    { 33L, (short)5, 3L, null, 3, "51" },
                    { 34L, (short)5, 3L, null, 7, "52" },
                    { 35L, (short)5, 3L, null, 6, "53" },
                    { 36L, (short)5, 3L, null, 10, "54" },
                    { 37L, (short)5, 3L, null, 4, "55" },
                    { 38L, (short)5, 3L, null, 4, "56" },
                    { 39L, (short)5, 3L, null, 4, "57" },
                    { 40L, (short)5, 3L, null, 4, "58" },
                    { 41L, (short)7, 4L, null, 4, "2" },
                    { 42L, (short)7, 4L, null, 8, "3" }
                });

            migrationBuilder.InsertData(
                table: "MedicalRooms",
                columns: new[] { "Id", "FloorNumber", "LocationId", "LocationId1", "MedicalRoomType", "Name" },
                values: new object[,]
                {
                    { 43L, (short)7, 4L, null, 9, "4" },
                    { 44L, (short)7, 4L, null, 1, "5" },
                    { 45L, (short)7, 4L, null, 0, "6" },
                    { 46L, (short)7, 4L, null, 0, "7" },
                    { 47L, (short)7, 4L, null, 10, "8" },
                    { 48L, (short)7, 4L, null, 5, "9" },
                    { 49L, (short)7, 4L, null, 1, "10" },
                    { 50L, (short)7, 4L, null, 7, "11" },
                    { 51L, (short)7, 4L, null, 11, "12" },
                    { 52L, (short)7, 4L, null, 0, "13" },
                    { 53L, (short)2, 5L, null, 1, "A" },
                    { 54L, (short)2, 5L, null, 1, "B" },
                    { 55L, (short)2, 5L, null, 0, "C" },
                    { 56L, (short)2, 5L, null, 0, "D" },
                    { 57L, (short)2, 5L, null, 2, "E" },
                    { 58L, (short)2, 5L, null, 11, "F" },
                    { 59L, (short)3, 5L, null, 4, "G" },
                    { 60L, (short)3, 5L, null, 5, "H" },
                    { 61L, (short)3, 5L, null, 5, "I" },
                    { 62L, (short)3, 5L, null, 0, "J" },
                    { 63L, (short)3, 5L, null, 1, "K" },
                    { 64L, (short)3, 5L, null, 0, "L" },
                    { 65L, (short)0, 6L, null, 1, "1" },
                    { 66L, (short)0, 6L, null, 8, "2" },
                    { 67L, (short)0, 6L, null, 0, "3" },
                    { 68L, (short)0, 6L, null, 0, "4" },
                    { 69L, (short)0, 6L, null, 2, "5" },
                    { 70L, (short)0, 6L, null, 3, "6" },
                    { 71L, (short)0, 6L, null, 11, "7" },
                    { 72L, (short)0, 6L, null, 7, "8" },
                    { 73L, (short)0, 6L, null, 6, "9" },
                    { 74L, (short)0, 6L, null, 9, "10" },
                    { 75L, (short)0, 6L, null, 9, "11" },
                    { 76L, (short)0, 6L, null, 10, "12" },
                    { 77L, (short)0, 6L, null, 0, "13" },
                    { 78L, (short)0, 6L, null, 0, "14" },
                    { 79L, (short)0, 6L, null, 0, "15" },
                    { 80L, (short)0, 6L, null, 0, "16" },
                    { 81L, (short)1, 7L, null, 0, "11" },
                    { 82L, (short)2, 7L, null, 0, "23" },
                    { 83L, (short)2, 7L, null, 0, "24" },
                    { 84L, (short)2, 7L, null, 0, "25" }
                });

            migrationBuilder.InsertData(
                table: "MedicalRooms",
                columns: new[] { "Id", "FloorNumber", "LocationId", "LocationId1", "MedicalRoomType", "Name" },
                values: new object[,]
                {
                    { 85L, (short)5, 7L, null, 1, "51" },
                    { 86L, (short)5, 7L, null, 8, "52" },
                    { 87L, (short)5, 7L, null, 0, "53" },
                    { 88L, (short)5, 7L, null, 0, "54" },
                    { 89L, (short)5, 7L, null, 2, "55" },
                    { 90L, (short)5, 7L, null, 3, "56" },
                    { 91L, (short)5, 8L, null, 11, "57" },
                    { 92L, (short)5, 8L, null, 7, "58" },
                    { 93L, (short)5, 8L, null, 6, "59" },
                    { 94L, (short)6, 8L, null, 9, "60" },
                    { 95L, (short)6, 8L, null, 9, "61" },
                    { 96L, (short)6, 8L, null, 10, "62" },
                    { 97L, (short)6, 8L, null, 0, "63" },
                    { 98L, (short)6, 8L, null, 0, "64" },
                    { 99L, (short)6, 8L, null, 0, "65" },
                    { 100L, (short)6, 8L, null, 0, "66" },
                    { 101L, (short)6, 8L, null, 0, "67" },
                    { 102L, (short)6, 8L, null, 0, "68" },
                    { 103L, (short)6, 8L, null, 0, "69" },
                    { 104L, (short)6, 8L, null, 0, "69B" },
                    { 105L, (short)0, 9L, null, 1, "A" },
                    { 106L, (short)0, 9L, null, 8, "B" },
                    { 107L, (short)0, 9L, null, 0, "C" },
                    { 108L, (short)0, 9L, null, 0, "D" },
                    { 109L, (short)0, 9L, null, 2, "E" },
                    { 110L, (short)0, 9L, null, 3, "F" },
                    { 111L, (short)0, 9L, null, 11, "G" },
                    { 112L, (short)0, 9L, null, 7, "H" },
                    { 113L, (short)0, 9L, null, 6, "I" },
                    { 114L, (short)0, 9L, null, 9, "J" },
                    { 115L, (short)0, 9L, null, 9, "K" },
                    { 116L, (short)0, 9L, null, 10, "L" },
                    { 117L, (short)1, 9L, null, 0, "M" },
                    { 118L, (short)1, 9L, null, 0, "N" },
                    { 119L, (short)1, 9L, null, 0, "O" },
                    { 120L, (short)1, 9L, null, 0, "P" },
                    { 121L, (short)1, 9L, null, 0, "R" },
                    { 122L, (short)2, 9L, null, 8, "S" },
                    { 123L, (short)2, 9L, null, 9, "T" },
                    { 124L, (short)2, 9L, null, 0, "U" },
                    { 125L, (short)2, 9L, null, 12, "W" }
                });

            migrationBuilder.InsertData(
                table: "MedicalServices",
                columns: new[] { "Id", "Description", "IsPrimaryService", "LocationId", "Name", "PrimaryMedicalServiceId", "PrimaryServiceId", "RequireRefferal", "StandardPrice", "VisitCategoryId", "VisitId" },
                values: new object[] { 1L, "USG", true, null, "USG", null, null, true, 200m, 4L, null });

            migrationBuilder.InsertData(
                table: "MedicalServices",
                columns: new[] { "Id", "Description", "IsPrimaryService", "LocationId", "Name", "PrimaryMedicalServiceId", "PrimaryServiceId", "RequireRefferal", "StandardPrice", "VisitCategoryId", "VisitId" },
                values: new object[,]
                {
                    { 2L, "RTG", true, null, "RTG", null, null, true, 200m, 4L, null },
                    { 3L, "Rezonans magnetyczny", true, null, "Rezonans magnetyczny", null, null, true, 200m, 4L, null },
                    { 4L, "Konsultacja gastrologiczna", true, null, "Konsultacja gastrologiczna", null, null, false, 250m, 2L, null },
                    { 30L, "Masaż leczniczy", true, null, "Masaż leczniczy", null, null, true, 300m, 5L, null },
                    { 31L, "Zajęcia rehabilitacyjne", true, null, "Zajęcia rehabilitacyjne", null, 74L, true, 300m, 5L, null },
                    { 38L, "Konsultacja proktologiczna", true, null, "Konsultacja proktologiczna", null, 74L, false, 200m, 2L, null },
                    { 39L, "Konsultacja internistyczna", true, null, "Konsultacja internistyczna", null, 74L, false, 200m, 2L, null },
                    { 40L, "Konsultacja pediatryczna", true, null, "Konsultacja pediatryczna", null, 74L, false, 200m, 2L, null },
                    { 41L, "Konsultacja geriatryczna", true, null, "Konsultacja geriatryczna", null, 74L, false, 200m, 2L, null },
                    { 42L, "Konsultacja ginekologiczna", true, null, "Konsultacja ginekologiczna", null, 74L, false, 200m, 2L, null },
                    { 43L, "Konsultacja ortopedyczna", true, null, "Konsultacja ortopedyczna", null, 74L, false, 200m, 2L, null },
                    { 44L, "Konsultacja kardiologiczna", true, null, "Konsultacja kardiologiczna", null, 74L, false, 200m, 2L, null },
                    { 45L, "Konsultacja okulistyczna", true, null, "Konsultacja okulistyczna", null, 74L, false, 200m, 2L, null },
                    { 46L, "Konsultacja dermatologiczna", true, null, "Konsultacja dermatologiczna", null, 74L, false, 200m, 2L, null },
                    { 47L, "Konsultacja endokrynologiczna", true, null, "Konsultacja endokrynologiczna", null, 74L, false, 200m, 2L, null },
                    { 48L, "Konsultacja chirurgii ogólnej", true, null, "Konsultacja chirurgii ogólnej", null, 74L, false, 200m, 2L, null },
                    { 49L, "Konsultacja neurochirurgiczna", true, null, "Konsultacja neurochirurgiczna", null, 74L, false, 250m, 2L, null },
                    { 50L, "Konsultacja chirurgii naczyniowej", true, null, "Konsultacja chirurgii naczyniowej", null, 74L, false, 250m, 2L, null },
                    { 51L, "Konsultacja chirurgii plastycznej", true, null, "Konsultacja chirurgii plastycznej", null, 74L, false, 300m, 2L, null },
                    { 52L, "Konsultacja chirurgii onkologicznej", true, null, "Konsultacja chirurgii onkologicznej", null, 74L, false, 300m, 2L, null },
                    { 53L, "Konsultacja laryngologiczna", true, null, "Konsultacja laryngologiczna", null, 74L, false, 200m, 2L, null },
                    { 54L, "Konsultacja neurologiczna", true, null, "Konsultacja neurologiczna", null, 74L, false, 200m, 2L, null },
                    { 55L, "Konsultacja urologiczna", true, null, "Konsultacja urologiczna", null, 74L, false, 200m, 2L, null },
                    { 56L, "Konsultacja psychologiczna", true, null, "Konsultacja psychologiczna", null, 74L, false, 200m, 2L, null },
                    { 57L, "Stomatologia zachowawcza", true, null, "Stomatologia zachowawcza", null, 74L, false, 200m, 3L, null },
                    { 58L, "Ortodoncja", true, null, "Ortodoncja", null, 74L, false, 200m, 3L, null },
                    { 59L, "Chirurgia stomatologiczna", true, null, "Chirurgia stomatologiczna", null, 74L, false, 200m, 3L, null },
                    { 60L, "Rentgen stomatologiczny", true, null, "Stomatologiczna diagnostyka obrazowa", null, 74L, true, 200m, 3L, null },
                    { 61L, "Protetyka", true, null, "Protetyka", null, 74L, false, 200m, 3L, null },
                    { 62L, "Profilaktyka stomatologiczna", true, null, "Profilaktyka stomatologiczna", null, 74L, false, 200m, 3L, null },
                    { 74L, "Badanie laboratoryjne", true, null, "Badanie laboratoryjne", null, 74L, true, 100m, 6L, null },
                    { 75L, "Fizykoterapia", true, null, "Fizykoterapia", null, 74L, true, 400m, 5L, null },
                    { 76L, "Szczepienia", true, null, "Szczepienia", null, 74L, false, 100m, 6L, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssuedMedicine_PrescriptionId",
                table: "IssuedMedicine",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_IssuedById",
                table: "MedicalReferrals",
                column: "IssuedById");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_IssuedToId",
                table: "MedicalReferrals",
                column: "IssuedToId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_MinorMedicalServiceId",
                table: "MedicalReferrals",
                column: "MinorMedicalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_PrimaryMedicalServiceId",
                table: "MedicalReferrals",
                column: "PrimaryMedicalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_VisitId",
                table: "MedicalReferrals",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_VisitWhenIssuedId",
                table: "MedicalReferrals",
                column: "VisitWhenIssuedId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_VisitWhenUsedId",
                table: "MedicalReferrals",
                column: "VisitWhenUsedId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRooms_LocationId",
                table: "MedicalRooms",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRooms_LocationId1",
                table: "MedicalRooms",
                column: "LocationId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceDiscount_MedicalPackageId",
                table: "MedicalServiceDiscount",
                column: "MedicalPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceDiscount_MedicalServiceId",
                table: "MedicalServiceDiscount",
                column: "MedicalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceMedicalWorker_MedicalWorkersId",
                table: "MedicalServiceMedicalWorker",
                column: "MedicalWorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_LocationId",
                table: "MedicalServices",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_PrimaryMedicalServiceId",
                table: "MedicalServices",
                column: "PrimaryMedicalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_VisitCategoryId",
                table: "MedicalServices",
                column: "VisitCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_VisitId",
                table: "MedicalServices",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalTestResults_MedicalServiceId",
                table: "MedicalTestResults",
                column: "MedicalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalTestResults_MedicalWorkerId",
                table: "MedicalTestResults",
                column: "MedicalWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalTestResults_PatientId",
                table: "MedicalTestResults",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalWorkers_PersonId",
                table: "MedicalWorkers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalWorkers_UserId",
                table: "MedicalWorkers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_PatientId",
                table: "Notification",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_VisitId",
                table: "Notification",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MedicalPackageId",
                table: "Patients",
                column: "MedicalPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_NFZUnitId",
                table: "Patients",
                column: "NFZUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PersonId",
                table: "Patients",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_IssuedById",
                table: "Prescriptions",
                column: "IssuedById");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_IssuedToId",
                table: "Prescriptions",
                column: "IssuedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_VisitId",
                table: "Recommendation",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitReviews_RevieweeId",
                table: "VisitReviews",
                column: "RevieweeId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitReviews_ReviewerId",
                table: "VisitReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_LocationId",
                table: "Visits",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_MedicalResultId",
                table: "Visits",
                column: "MedicalResultId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_MedicalRoomId",
                table: "Visits",
                column: "MedicalRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_MedicalWorkerId",
                table: "Visits",
                column: "MedicalWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientId",
                table: "Visits",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PrescriptionId",
                table: "Visits",
                column: "PrescriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PrimaryServiceId",
                table: "Visits",
                column: "PrimaryServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_UsedExaminationReferralId",
                table: "Visits",
                column: "UsedExaminationReferralId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitCategoryId",
                table: "Visits",
                column: "VisitCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitReviewId",
                table: "Visits",
                column: "VisitReviewId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReferrals_MedicalServices_MinorMedicalServiceId",
                table: "MedicalReferrals",
                column: "MinorMedicalServiceId",
                principalTable: "MedicalServices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReferrals_MedicalServices_PrimaryMedicalServiceId",
                table: "MedicalReferrals",
                column: "PrimaryMedicalServiceId",
                principalTable: "MedicalServices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReferrals_Visits_VisitId",
                table: "MedicalReferrals",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReferrals_Visits_VisitWhenIssuedId",
                table: "MedicalReferrals",
                column: "VisitWhenIssuedId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReferrals_Visits_VisitWhenUsedId",
                table: "MedicalReferrals",
                column: "VisitWhenUsedId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServiceDiscount_MedicalServices_MedicalServiceId",
                table: "MedicalServiceDiscount",
                column: "MedicalServiceId",
                principalTable: "MedicalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServiceMedicalWorker_MedicalServices_MedicalServicesId",
                table: "MedicalServiceMedicalWorker",
                column: "MedicalServicesId",
                principalTable: "MedicalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServices_Visits_VisitId",
                table: "MedicalServices",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Prescriptions_PrescriptionId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_MedicalServices_MinorMedicalServiceId",
                table: "MedicalReferrals");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_MedicalServices_PrimaryMedicalServiceId",
                table: "MedicalReferrals");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTestResults_MedicalServices_MedicalServiceId",
                table: "MedicalTestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_MedicalServices_PrimaryServiceId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_MedicalWorkers_IssuedById",
                table: "MedicalReferrals");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTestResults_MedicalWorkers_MedicalWorkerId",
                table: "MedicalTestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitReviews_MedicalWorkers_RevieweeId",
                table: "VisitReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_MedicalWorkers_MedicalWorkerId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_Patients_IssuedToId",
                table: "MedicalReferrals");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTestResults_Patients_PatientId",
                table: "MedicalTestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitReviews_Patients_ReviewerId",
                table: "VisitReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Patients_PatientId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_Visits_VisitId",
                table: "MedicalReferrals");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_Visits_VisitWhenIssuedId",
                table: "MedicalReferrals");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_Visits_VisitWhenUsedId",
                table: "MedicalReferrals");

            migrationBuilder.DropTable(
                name: "IssuedMedicine");

            migrationBuilder.DropTable(
                name: "MedicalServiceDiscount");

            migrationBuilder.DropTable(
                name: "MedicalServiceMedicalWorker");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Recommendation");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "MedicalServices");

            migrationBuilder.DropTable(
                name: "MedicalWorkers");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "MedicalPackages");

            migrationBuilder.DropTable(
                name: "NFZUnits");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "MedicalReferrals");

            migrationBuilder.DropTable(
                name: "MedicalRooms");

            migrationBuilder.DropTable(
                name: "MedicalTestResults");

            migrationBuilder.DropTable(
                name: "VisitCategories");

            migrationBuilder.DropTable(
                name: "VisitReviews");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}

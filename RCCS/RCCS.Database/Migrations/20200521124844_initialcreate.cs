using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RCCS.Database.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citizens",
                columns: table => new
                {
                    CPR = table.Column<long>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.CPR);
                });

            migrationBuilder.CreateTable(
                name: "RespiteCareHomes",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    AvailableRespiteCareRooms = table.Column<int>(nullable: false),
                    RespiteCareRoomsTotal = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespiteCareHomes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "CitizenOverviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareNeed = table.Column<string>(nullable: true),
                    PurposeOfStay = table.Column<string>(nullable: true),
                    NumberOfReevaluations = table.Column<int>(nullable: false),
                    CitizenCPR = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenOverviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CitizenOverviews_Citizens_CitizenCPR",
                        column: x => x.CitizenCPR,
                        principalTable: "Citizens",
                        principalColumn: "CPR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Report = table.Column<string>(nullable: true),
                    ResponsibleCaretaker = table.Column<string>(nullable: true),
                    CitizenCPR = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressReports_Citizens_CitizenCPR",
                        column: x => x.CitizenCPR,
                        principalTable: "Citizens",
                        principalColumn: "CPR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relatives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Relation = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    CitizenCPR = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relatives_Citizens_CitizenCPR",
                        column: x => x.CitizenCPR,
                        principalTable: "Citizens",
                        principalColumn: "CPR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResidenceInformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    ReevaluationDate = table.Column<DateTime>(nullable: false),
                    PlannedDischargeDate = table.Column<DateTime>(nullable: false),
                    ProspectiveSituationStatusForCitizen = table.Column<string>(nullable: true),
                    CitizenCPR = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidenceInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidenceInformations_Citizens_CitizenCPR",
                        column: x => x.CitizenCPR,
                        principalTable: "Citizens",
                        principalColumn: "CPR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespiteCareRooms",
                columns: table => new
                {
                    RespiteCareRoomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    IsAvailable = table.Column<bool>(nullable: false),
                    RespiteCareHomeName = table.Column<string>(nullable: true),
                    CitizenCPR = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespiteCareRooms", x => x.RespiteCareRoomId);
                    table.ForeignKey(
                        name: "FK_RespiteCareRooms_Citizens_CitizenCPR",
                        column: x => x.CitizenCPR,
                        principalTable: "Citizens",
                        principalColumn: "CPR",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespiteCareRooms_RespiteCareHomes_RespiteCareHomeName",
                        column: x => x.RespiteCareHomeName,
                        principalTable: "RespiteCareHomes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitizenOverviews_CitizenCPR",
                table: "CitizenOverviews",
                column: "CitizenCPR",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgressReports_CitizenCPR",
                table: "ProgressReports",
                column: "CitizenCPR");

            migrationBuilder.CreateIndex(
                name: "IX_Relatives_CitizenCPR",
                table: "Relatives",
                column: "CitizenCPR");

            migrationBuilder.CreateIndex(
                name: "IX_ResidenceInformations_CitizenCPR",
                table: "ResidenceInformations",
                column: "CitizenCPR",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RespiteCareRooms_CitizenCPR",
                table: "RespiteCareRooms",
                column: "CitizenCPR",
                unique: true,
                filter: "[CitizenCPR] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RespiteCareRooms_RespiteCareHomeName",
                table: "RespiteCareRooms",
                column: "RespiteCareHomeName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitizenOverviews");

            migrationBuilder.DropTable(
                name: "ProgressReports");

            migrationBuilder.DropTable(
                name: "Relatives");

            migrationBuilder.DropTable(
                name: "ResidenceInformations");

            migrationBuilder.DropTable(
                name: "RespiteCareRooms");

            migrationBuilder.DropTable(
                name: "Citizens");

            migrationBuilder.DropTable(
                name: "RespiteCareHomes");
        }
    }
}

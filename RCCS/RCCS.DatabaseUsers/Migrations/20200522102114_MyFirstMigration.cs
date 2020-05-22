using Microsoft.EntityFrameworkCore.Migrations;

namespace RCCS.DatabaseUsers.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    EfUserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaleId = table.Column<string>(maxLength: 254, nullable: true),
                    PwHash = table.Column<string>(maxLength: 60, nullable: true),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.EfUserId);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    EfAdminId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EfUserId = table.Column<long>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 64, nullable: true),
                    LastName = table.Column<string>(maxLength: 32, nullable: true),
                    PersonaleId = table.Column<string>(maxLength: 254, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.EfAdminId);
                    table.ForeignKey(
                        name: "FK_Admins_Users_EfUserId",
                        column: x => x.EfUserId,
                        principalTable: "Users",
                        principalColumn: "EfUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coordinators",
                columns: table => new
                {
                    EfCoordinatorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EfUserId = table.Column<long>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 64, nullable: true),
                    LastName = table.Column<string>(maxLength: 32, nullable: true),
                    PersonaleId = table.Column<string>(maxLength: 254, nullable: true),
                    PhoneNo = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinators", x => x.EfCoordinatorId);
                    table.ForeignKey(
                        name: "FK_Coordinators_Users_EfUserId",
                        column: x => x.EfUserId,
                        principalTable: "Users",
                        principalColumn: "EfUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NursingStaffs",
                columns: table => new
                {
                    EfNursingStaffId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EfUserId = table.Column<long>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 64, nullable: true),
                    LastName = table.Column<string>(maxLength: 32, nullable: true),
                    PersonaleId = table.Column<string>(maxLength: 254, nullable: true),
                    PhoneNo = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingStaffs", x => x.EfNursingStaffId);
                    table.ForeignKey(
                        name: "FK_NursingStaffs_Users_EfUserId",
                        column: x => x.EfUserId,
                        principalTable: "Users",
                        principalColumn: "EfUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_EfUserId",
                table: "Admins",
                column: "EfUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_PersonaleId",
                table: "Admins",
                column: "PersonaleId",
                unique: true,
                filter: "[PersonaleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinators_EfUserId",
                table: "Coordinators",
                column: "EfUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinators_PersonaleId",
                table: "Coordinators",
                column: "PersonaleId",
                unique: true,
                filter: "[PersonaleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NursingStaffs_EfUserId",
                table: "NursingStaffs",
                column: "EfUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingStaffs_PersonaleId",
                table: "NursingStaffs",
                column: "PersonaleId",
                unique: true,
                filter: "[PersonaleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonaleId",
                table: "Users",
                column: "PersonaleId",
                unique: true,
                filter: "[PersonaleId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Coordinators");

            migrationBuilder.DropTable(
                name: "NursingStaffs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

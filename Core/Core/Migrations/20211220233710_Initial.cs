using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayOfWeeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfWeeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    DarkTheme = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    DayOfWeekId = table.Column<Guid>(nullable: false),
                    StartHour = table.Column<TimeSpan>(nullable: false),
                    FinishHour = table.Column<TimeSpan>(nullable: false),
                    AmounPaid = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_DayOfWeeks_DayOfWeekId",
                        column: x => x.DayOfWeekId,
                        principalTable: "DayOfWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    TotalPaid = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkBalances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    DayOfWeekId = table.Column<Guid>(nullable: false),
                    StartHour = table.Column<TimeSpan>(nullable: false),
                    FinishHour = table.Column<TimeSpan>(nullable: false),
                    AmounPaid = table.Column<double>(nullable: false),
                    WorkBalanceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_DayOfWeeks_DayOfWeekId",
                        column: x => x.DayOfWeekId,
                        principalTable: "DayOfWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Works_WorkBalances_WorkBalanceId",
                        column: x => x.WorkBalanceId,
                        principalTable: "WorkBalances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DayOfWeekId",
                table: "Payments",
                column: "DayOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkBalances_EmployeeId",
                table: "WorkBalances",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Works_DayOfWeekId",
                table: "Works",
                column: "DayOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_WorkBalanceId",
                table: "Works",
                column: "WorkBalanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "DayOfWeeks");

            migrationBuilder.DropTable(
                name: "WorkBalances");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}

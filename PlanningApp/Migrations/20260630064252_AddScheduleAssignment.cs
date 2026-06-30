using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanningApp.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PositionOnLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shift = table.Column<int>(type: "int", nullable: false),
                    ProductionLineId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleAssignments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduleAssignments_ProductionLines_ProductionLineId",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleAssignments_EmployeeId",
                table: "ScheduleAssignments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleAssignments_ProductionLineId",
                table: "ScheduleAssignments",
                column: "ProductionLineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleAssignments");
        }
    }
}

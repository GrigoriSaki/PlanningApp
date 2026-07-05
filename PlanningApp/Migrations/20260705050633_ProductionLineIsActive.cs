using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanningApp.Migrations
{
    /// <inheritdoc />
    public partial class ProductionLineIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductionLines",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductionLines");
        }
    }
}

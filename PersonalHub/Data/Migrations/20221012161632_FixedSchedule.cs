using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalHub.Data.Migrations
{
    public partial class FixedSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Schedules",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CategoryId",
                table: "Schedules",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Category_CategoryId",
                table: "Schedules",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Category_CategoryId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_CategoryId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Schedules",
                newName: "Year");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STCS.Web.Migrations
{
    public partial class InstructorFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InstructorDesignation",
                table: "Instructors",
                newName: "InstructorType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InstructorType",
                table: "Instructors",
                newName: "InstructorDesignation");
        }
    }
}

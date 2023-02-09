using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkLibrary.Migrations
{
    public partial class testChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskName",
                table: "Tasks",
                newName: "TaskDescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskDescription",
                table: "Tasks",
                newName: "TaskName");
        }
    }
}

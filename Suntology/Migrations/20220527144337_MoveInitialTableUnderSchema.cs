using Microsoft.EntityFrameworkCore.Migrations;

namespace Suntology.Migrations
{
    public partial class MoveInitialTableUnderSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Castes",
                table: "Castes");

            migrationBuilder.RenameTable(
                name: "Castes",
                newName: "suntology.caste");

            migrationBuilder.AddPrimaryKey(
                name: "PK_suntology.caste",
                table: "suntology.caste",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_suntology.caste",
                table: "suntology.caste");

            migrationBuilder.RenameTable(
                name: "suntology.caste",
                newName: "Castes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Castes",
                table: "Castes",
                column: "Id");
        }
    }
}

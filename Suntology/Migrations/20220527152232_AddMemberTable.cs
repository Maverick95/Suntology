using Microsoft.EntityFrameworkCore.Migrations;

namespace Suntology.Migrations
{
    public partial class AddMemberTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "suntology.caste",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "suntology.member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormerForename = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FormerSurname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AssignedGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    CasteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suntology.member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_suntology.member_suntology.caste_CasteId",
                        column: x => x.CasteId,
                        principalTable: "suntology.caste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_suntology.member_CasteId",
                table: "suntology.member",
                column: "CasteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "suntology.member");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "suntology.caste");
        }
    }
}

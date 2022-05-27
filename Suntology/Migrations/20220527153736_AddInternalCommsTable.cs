using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Suntology.Migrations
{
    public partial class AddInternalCommsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "suntology.internalcomms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InitiatorId = table.Column<int>(type: "int", nullable: false),
                    InitiateeId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suntology.internalcomms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_suntology.internalcomms_suntology.member_InitiateeId",
                        column: x => x.InitiateeId,
                        principalTable: "suntology.member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_suntology.internalcomms_suntology.member_InitiatorId",
                        column: x => x.InitiatorId,
                        principalTable: "suntology.member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_suntology.internalcomms_InitiateeId",
                table: "suntology.internalcomms",
                column: "InitiateeId");

            migrationBuilder.CreateIndex(
                name: "IX_suntology.internalcomms_InitiatorId",
                table: "suntology.internalcomms",
                column: "InitiatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "suntology.internalcomms");
        }
    }
}

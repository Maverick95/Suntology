using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using System.Reflection;

namespace Suntology.Migrations
{
    public partial class AddCreateTestDatasetStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FormerSurname",
                table: "suntology.member",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FormerForename",
                table: "suntology.member",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedGender",
                table: "suntology.member",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "suntology.member",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            var location = Assembly.GetExecutingAssembly().Location;
            var path = location.Substring(0, location.LastIndexOf('\\'));

            migrationBuilder.Sql(
                File.ReadAllText(
                    Path.Combine(
                        path,
                        "Scripts",
                        "CreateTestDataset",
                        "V1.sql"
                        )
                    )
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE [dbo.suntology.CreateTestDataset]
                ");

            migrationBuilder.AlterColumn<string>(
                name: "FormerSurname",
                table: "suntology.member",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FormerForename",
                table: "suntology.member",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedGender",
                table: "suntology.member",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "suntology.member",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

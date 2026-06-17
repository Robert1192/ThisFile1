using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ProjectWebsite10032026.Migrations
{
    [DbContext(typeof(ProjectWebsite10032026.Data.AppDbContext))]
    [Migration("20250617120000_AddUrlAndFileName")]
    public partial class AddUrlAndFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Links",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Downloads",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Downloads");
        }
    }
}

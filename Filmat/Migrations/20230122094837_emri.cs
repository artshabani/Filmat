using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmat.Migrations
{
    public partial class emri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Emri",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emri",
                table: "AspNetUsers");
        }
    }
}

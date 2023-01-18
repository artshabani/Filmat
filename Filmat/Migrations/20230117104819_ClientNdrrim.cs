using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmat.Migrations
{
    /// <inheritdoc />
    public partial class ClientNdrrim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Clients",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Clients",
                newName: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Clients",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Clients",
                newName: "email");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DelegationsMVC.Infrastructure.Migrations
{
    public partial class Update_Autditable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Delegations");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Delegations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateById",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateById",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateById",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateById",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateById",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateById",
                table: "Delegations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Delegations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

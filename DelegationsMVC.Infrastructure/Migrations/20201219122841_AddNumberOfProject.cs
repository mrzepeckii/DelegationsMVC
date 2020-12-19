using Microsoft.EntityFrameworkCore.Migrations;

namespace DelegationsMVC.Infrastructure.Migrations
{
    public partial class AddNumberOfProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Projects");
        }
    }
}

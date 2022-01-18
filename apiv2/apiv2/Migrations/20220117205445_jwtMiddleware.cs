using Microsoft.EntityFrameworkCore.Migrations;

namespace apiv2.Migrations
{
    public partial class jwtMiddleware : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Apprentices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Apprentices");
        }
    }
}

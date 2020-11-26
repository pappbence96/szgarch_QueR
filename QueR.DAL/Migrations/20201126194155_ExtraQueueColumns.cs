using Microsoft.EntityFrameworkCore.Migrations;

namespace QueR.DAL.Migrations
{
    public partial class ExtraQueueColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "Queues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Step",
                table: "Queues",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "Queues");

            migrationBuilder.DropColumn(
                name: "Step",
                table: "Queues");
        }
    }
}

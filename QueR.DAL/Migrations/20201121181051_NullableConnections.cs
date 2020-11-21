using Microsoft.EntityFrameworkCore.Migrations;

namespace QueR.DAL.Migrations
{
    public partial class NullableConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Queues_QueueId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "QueueId",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Queues_QueueId",
                table: "Tickets",
                column: "QueueId",
                principalTable: "Queues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Queues_QueueId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "QueueId",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Queues_QueueId",
                table: "Tickets",
                column: "QueueId",
                principalTable: "Queues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

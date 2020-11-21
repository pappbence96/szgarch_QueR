using Microsoft.EntityFrameworkCore.Migrations;

namespace QueR.DAL.Migrations
{
    public partial class AddedUserCompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "AdministratorId", "MailingAddress", "Name" },
                values: new object[] { 1, null, "Address of Company A", "Company A" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "AdministratorId", "MailingAddress", "Name" },
                values: new object[] { 2, null, "Address of Company B", "Company B" });

            migrationBuilder.InsertData(
                table: "QueueTypes",
                columns: new[] { "Id", "CompanyId", "IsEnabled", "Name" },
                values: new object[,]
                {
                    { 1, 1, true, "Warranty" },
                    { 2, 1, true, "Returns" },
                    { 3, 2, true, "General administration" }
                });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "Id", "Address", "CompanyId", "ManagerId", "Name" },
                values: new object[,]
                {
                    { 1, "random address", 1, null, "A Site 1" },
                    { 2, "random address", 1, null, "A Site 2" },
                    { 3, "random address", 2, null, "B Site 1" },
                    { 4, "random address", 2, null, "B Site 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "QueueTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QueueTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "QueueTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");
        }
    }
}

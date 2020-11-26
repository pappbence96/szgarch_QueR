using Microsoft.EntityFrameworkCore.Migrations;

namespace QueR.DAL.Migrations
{
    public partial class SeedUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "eec16eca-92fd-4fc8-a40e-9200e7c5e54b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "df211399-9990-4ad1-badd-98866a4607e8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "666699b3-591f-4567-b4a9-a83b4f536eb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "f0907eb2-d3d3-4575-a866-5d3dac3316f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "ad1b886d-60bf-4428-b4e1-8a73613ca7a9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "73013b06-8cac-4bbc-9fa9-88f35d312394", "AQAAAAEAACcQAAAAEGfQCff+v3H8LRsVTCfGeUnb8fugy7mez7fdZk7fFjPxovS0qkZh2GDCsfuNq16OBQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cc30d075-1486-4f3e-8c3d-ce4a4f796da8", "AQAAAAEAACcQAAAAEA5aS8EzVEwHcVueG1Mrzi9sfUKxd6MwQn5lDk3FVfQU2VvfEsnuqbU3tSw8x7QhFQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "29d16c0f-e2de-419b-80ec-bfbc00662f82", "AQAAAAEAACcQAAAAEMDfa53bJOKofWRXwgdgcnmC49oRcrqVdeAUi+70qJ+SVvzP+GEgev/WUbsThMHoKQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d4ac3c41-fb11-4ae5-a637-879ca92f1b2d", "AQAAAAEAACcQAAAAENoRH3nuzYdZ++UHBGajcUvxbgVxzSjBTaFQIRr7H80swfpja2Dzqrzdw9RaywQLlA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bbcfc071-55fe-4a93-b739-ed26d52d7a41", "AQAAAAEAACcQAAAAEPCykcZrYYY4exKzoMxj+B9mL3iRsBqFwK/MENHKIaAgPl/Se44cmxcJrE+EbjQwMw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9a9943f8-c8d3-45d9-aec9-71b71e33d2ef", "AQAAAAEAACcQAAAAEIKCl8w7DQBEJBdkl9cmKkpd02FwXFYxSfVBwB4exIBXtzD2eSWSjEOYrC26+yBA4w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7474edba-3f1a-4d09-ad23-db82c3e127a6", "AQAAAAEAACcQAAAAEPLKROaJ20Bego7gviET4WohjrvyS9TqKFd4oxFofvARdZ2OKPBOhUCTwSxcG2ZVTg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6ff01aee-ab75-4999-ad0f-edb20a289e96", "AQAAAAEAACcQAAAAEJx/LhCrjBKJKLQsDwBQYK1J9XFG0BQG4gwz7CA0GwWDDqBAO6MxUmyPfm/zquX7nA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7a021976-2333-4640-95c5-cb4203eeb497", "AQAAAAEAACcQAAAAEN6OTk9BzwRO+2yRKoLvUGtpdtpNxVqioPfYmqhLRWk/Lr/4PaXdvjGegSS2Wrulxg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9c784958-4f19-4e1c-9f1f-9dba5e65bdab", "AQAAAAEAACcQAAAAEDyVO2ldytyquMqx9joPGVdtOMOG4FdKxsge8AyqsrjQhkz+q95up3irp9dVg7z8Ww==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7d3d3cce-e71d-4174-bf26-2c14a57f421c", "AQAAAAEAACcQAAAAEJWxqTvho6wK56N8oGrN2bUuGWLXA5f6YwA21Lka/inaRlBG12icKGKT7DWvEmzeAA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b9d12ab3-0996-45ad-9509-d5e2fb808701", "AQAAAAEAACcQAAAAEMY90GhktP3ulul2H4jYU7X6nHb0LiQtih+QoV1qPfqfpgXZgmslmS1ABKSTHrVCdg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "273c8cb9-3142-4433-a1ff-7f7601d427ae", "AQAAAAEAACcQAAAAEA/Zg39TdCDbtsegTbTyl7VhwJ3zdEIMDA+FAhCOc6i1DBunfKz7FZfl/rHRG0SxKw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b5032de2-2267-4f20-84c1-434c174341b8", "AQAAAAEAACcQAAAAEPdBWW4b296TIreHeu34UL7LCb0D9Ep/C1Eom8S/j13sP6K5zi5yONiT/ld2mECkjw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "79aefd0a-3253-4d8b-8f2d-a3ac56bb92de", "AQAAAAEAACcQAAAAEBsRhWs0YXrB9Sh/khXu1yMXLc3KVitOLuUEQwgu3i5NWaENtO5s83C1a7kzmBHrIw==" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "AdministratorId", "MailingAddress", "Name" },
                values: new object[,]
                {
                    { 2, 3, "Address of Company B", "Company B" },
                    { 1, 2, "Address of Company A", "Company A" }
                });

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
                    { 1, "random address", 1, 4, "A Site 1" },
                    { 2, "random address", 1, 5, "A Site 2" },
                    { 3, "random address", 2, 6, "B Site 1" },
                    { 4, "random address", 2, 7, "B Site 2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "08448364-34a6-4f90-8f10-f0a9e7baecc7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "87873787-484a-421f-a9ac-bcb0a7aeccb0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "07f7f140-44c4-417b-9992-9f6d0acb1aa4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "73de9371-733b-466f-b38c-5a51b4001779");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "a4082c49-ff90-46f8-a514-4829b4b56c9f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b0041416-8ad9-42fc-9958-953665f4268c", "AQAAAAEAACcQAAAAEOdhiL56dm5XyoFhBVkJfRNNIj/0OR/KYMPkamHfVRlL+6LJcEVaCPkvtmprWPY2RQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "af64745b-02f5-450e-add8-1a5535761365", "AQAAAAEAACcQAAAAEKhlZDt44aE6Px4OmKx0BtlqIusaBY4mBwYCpHibkbBy0sH5v2dM6D8zLgQ+G5hpUw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8549b5ed-7576-4171-893e-44b1ab69af62", "AQAAAAEAACcQAAAAED6AmRe+U2URGEOhRgPQ6yCFtx7h9OM+tg6mAUuKWqsrjsaqdHfMgzkl2vJgj0M/Ag==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "743736dd-b1fc-4979-8433-ec8e1f984669", "AQAAAAEAACcQAAAAEBz7SSGLATFRdN9nAXKxZyRQr/c5zJoXoTfDgqk5qaoEura8fS+boYwfbN/ZZ8sC2w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "671d1f86-a0a5-4f08-8cac-35b155b25605", "AQAAAAEAACcQAAAAEEspcxj5JZ0PB7seWkPRq0Gzk4DXIqn+zYyss5m9uFG2sOkzUg2ObAoHipgda7nJ+A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "be1c487f-429b-40b0-8b50-97c12b61b837", "AQAAAAEAACcQAAAAEC2rlyBY8gMZifarLoGVFzTBNnzG0ZL2S5K0vM+m4WOxmWzhBS1+WU89eTv20Cpocg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0d6bc377-1ab5-47f6-940a-f7566f106d52", "AQAAAAEAACcQAAAAEII1V7M26bGW/cuPgNZC/yjUHYnRGJLxsiitjGGNk0LJvDFfm4lABsOUMaP7SBQmMA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b537187a-ca48-4210-b306-16d80f436300", "AQAAAAEAACcQAAAAEMbAcc7KgPgafs4uFyCyIsi9/5d9JG6uOLj5Vlgl8mw1Ve3Ywszs8qnL4qHnuieVkA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2c3d9ce6-e4af-496d-b31b-164f97fea292", "AQAAAAEAACcQAAAAEN1/Rjvk0pI7pRLxqPkhXAGt8QPQ0Tt+l//Fa4jdkAI1bO3Gli8I2Uny9jddbRWuWg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "667d2aa8-77d9-488d-8979-1b2b79805a30", "AQAAAAEAACcQAAAAEJDPr01Ryl6eI31oXoal84wvSUZFy4quTibuqGLrlXLIl8JAfzgecauRYDvuPmpwbw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bc83554f-cd86-45cb-a292-d2cf33eef8bc", "AQAAAAEAACcQAAAAEJBXSMnsFJ160asruOq9MxI22NmCWefGhiAKWZ0ZcHMKnVDnXm/0pK+kp0nPjYflYA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e597bd62-eea0-4086-9b1e-51f5cb4446e0", "AQAAAAEAACcQAAAAEMneeRls6CzOpeoyCWC5Ongb/yFUHPZd6VfWMN55KSNCMG4lhseXmeZrOAy3Hyct/A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2bfb6e02-21cc-45a5-aebb-b9b61c35c10f", "AQAAAAEAACcQAAAAEIXOEwgwfSivRdrVu3QXcb+lPWAiktoZc5Dv2morue+4VuKYjuHVuEwIb5hJ3bVW9Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c928387c-47d4-4fd8-bcb3-5932154ca179", "AQAAAAEAACcQAAAAEDOSLaYG8b5TT9S5vbM1aek6V7kHqFtBRyLcdVOrdU2dXvfZpPE4hfsw/ZBGQ7oeUQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8820d065-8a5b-4d39-bd80-0681d2df7454", "AQAAAAEAACcQAAAAEF/ltKL+Vf+kaeD5ociwNKXUUKj5sJbHJ4aMGboIYUBI0FkWt8jUdbwmiJ3Rv6WyBg==" });
        }
    }
}

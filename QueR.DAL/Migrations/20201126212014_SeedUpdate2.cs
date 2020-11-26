using Microsoft.EntityFrameworkCore.Migrations;

namespace QueR.DAL.Migrations
{
    public partial class SeedUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f5583ca1-c767-48a2-8699-4c320103cdbc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1927f661-1d07-4444-8aaf-709a88fd0aae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "63bbe611-4da6-44a2-aaff-18cd12170bbb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "946315b3-5ebc-4d33-80bf-fadddf7de12e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "7c856f2f-8d2d-47e3-9a69-d1709c6fc909");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "99c2da2e-6173-44b8-b82e-ac73cf8a715c", "AQAAAAEAACcQAAAAEHZV6cTXXnBDX907gsG0vcqaxnr7ydHpyxPaYn2ISYttx54APL5bd4pMds8Di3uWdw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dc88261a-290c-4414-aa1c-2f1c43acdc34", "AQAAAAEAACcQAAAAEK3Pn9Lb/y287e0myjoFHA3gc3zSspeL/3V2piIS0gYhRXqWpnh5JG/jAa5gjUIDtQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5bcd2bf7-6975-4a08-977a-e5d6bda3c798", "AQAAAAEAACcQAAAAEIJMcJHm57TC80MANa3hq728aQ0YpPM/nnUW0I2FFc3FxgpC4rYbqz4GNCFFI/DwRA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "a0fee814-69ca-4d87-93a3-8373e2a9c2fb", "AQAAAAEAACcQAAAAEEbnAPUkpWYgGDBM130RTZ8WktHt9N7B820YBXBPaAR+cNf8JeQ0gbJ+WoKow6CMZA==", 1 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "ab1f10dc-9f36-43ef-a26f-2c80c6d70070", "AQAAAAEAACcQAAAAEG/Ev3LEbxCFEBHkTFOVUNlKBduf5gGqBPZoG4T4m6SsPTxgEsmvQtzjVigkXrk7Ig==", 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "c8528207-2878-44a1-9daa-64b249aad260", "AQAAAAEAACcQAAAAEFxZkfpxL0JMpWGeu93kbLHqbgr7aAnJw7JcWF8KUMqlsxP/W3c4FsBfHWw7jJFK3A==", 3 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "88507dc7-e344-404c-82fd-c3dd0684c00f", "AQAAAAEAACcQAAAAEDl1jaij5P5k/fseQi6kyDcBNBcTJ+AoK+9Z2tvxb5yNzmrrzH68Afs4oZ/f7MlZug==", 4 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "fc5e3e7e-2f18-47d8-97aa-61ea79ad3512", "AQAAAAEAACcQAAAAEFOTdgRR9GyJAPCb1avuQajARtnEdkP7zremlE3iQt4Xpps3AjWAaSt79VaPVoFm6A==", 1 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "d95ca76d-e2c7-43df-84d7-c79deeb94093", "AQAAAAEAACcQAAAAEIsncEBFsfcTSJdVMbTFfcTiQtx2r8H2SLbHCnLQ4xlmw89fG46Eu/PcG5wlYO04zQ==", 1 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "d0728c1b-d1cd-4fd3-ada4-6d96229d9154", "AQAAAAEAACcQAAAAEFzEx7eGtC4evB30lZMiCkG0v8FOid3EeGFL372UHKAT9JyGf0f4jA+WjBlgHo+8BA==", 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "b39434de-467b-4f05-86ac-31cbcedef2d3", "AQAAAAEAACcQAAAAEHNkRZuy0px6l/xU5fddLh+75aIRfcnUEdoxrhI4D7o7GUO8XlsP5zNjYpbJ+RkkbA==", 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "d6eb6649-3788-472b-8070-ff07dd0a8fc2", "AQAAAAEAACcQAAAAEL89d+QYH5KI8FxIuS66fmBQQrtn1TFuSUCwNkbiAw92CUJte/21faya71PKzzbc4A==", 3 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "5d1e511b-1e35-47a3-b877-acbe13a287ca", "AQAAAAEAACcQAAAAEGOWFVIpONkOrpiIHQ6p9sB08WFCUq5TdLc9MJBEwU2BIgs3KDNZ6e564PvGNPUy9A==", 3 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "5b4609b8-cbaa-489e-9b02-7ea2ee92d2a5", "AQAAAAEAACcQAAAAEI/fvcYGYmf8XHvcjeLKDKNnUnnyZOAfjUnKXy9ji2FC35hMir+tI12LjnpoMcJN6A==", 4 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "cf9656b3-cf76-49c2-b212-96e5afff48b4", "AQAAAAEAACcQAAAAEPhgMydZx+tTup7UK6oFBloso241i3Saeur5n6FH0qs5eB21Abot4/OQPcWPPrrYaQ==", 4 });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                column: "AdministratorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2,
                column: "AdministratorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 1,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 2,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 3,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 4,
                column: "ManagerId",
                value: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ea6b98e5-e3eb-42aa-825f-d972aa246b30");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "911a9b3c-965a-4e93-95b1-f0f99047f9e9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "aa652f2c-1038-4677-ba8e-4886c012b7f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "8c7b4f31-2b51-4b26-b917-571948772d49");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "ff418bc7-fb8d-40a0-a11e-14aa84a60871");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e3006973-21df-4fdf-8d89-417a0c4e8e2d", "AQAAAAEAACcQAAAAEDuq09SlfZGnht62kLoKuRlbwMWl5+eM+hxnvAg9BK81/dRT4XicvfJH/3NeU2TPSg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2ce75edb-278e-48cb-b6b5-bf191c1a39a3", "AQAAAAEAACcQAAAAEAudA80caouD/NOxoN4LPQIq6W4+P+FtLsZ29A5Zj6MHOKiGX2rdZvVq6CS48keDRA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ca83aaf1-3778-4bf4-b146-542f5da1ba8c", "AQAAAAEAACcQAAAAEH+7ffK6Z5MeEDUiL27vapORS0pBWHgfWxDmwNapz3ecabC2nU5AoX60NtGFZ5MDww==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "5c727b9e-bbd3-4d67-afc8-444623e49683", "AQAAAAEAACcQAAAAEAzwAA8R4cAEuFfd1qaYZGAwpDwOqb44n1Pljea2BMxAMN2xpcxDBPG7ydR42m9WUg==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "6aec8e4c-f601-4bf1-960c-49784215628d", "AQAAAAEAACcQAAAAEDSJaf2M5yiJY4ddRxR+zJ6L9uPeRxHuixAe++DXFQO8V2qEKIHOMfB5CPXQ+/HOvg==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "f6d24255-6fed-4d42-b28b-11ed3018076e", "AQAAAAEAACcQAAAAEDSaeiNYlanjDxi5ShrhIsn+9rmmGyz1c0CmfUJ4CVzO90dgPS+QfZverEPX7dPJUg==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "c077a098-20d3-4027-abcd-bf2246a53a01", "AQAAAAEAACcQAAAAEDVla6Iq4svniUIy8bVpuKl5Jzy7ngSvf+wufwbuJlHFCtnFARcC+M81XTw62vHFkQ==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "1d08cee4-ba99-462d-86ab-c5d3d1162e70", "AQAAAAEAACcQAAAAEO9qOdaeqKQvkY9wsu10GQyAerWF81iJhwl57TzH4egeRD7pOrzP9y2l56EgVJYtfw==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "fa47a528-1517-45a9-a924-b3e366b0c32c", "AQAAAAEAACcQAAAAED30QYmrzGqdxS8LSk8/kesXeP+6mPkPxxFiBMf1ahBo2IkxuiByczmmmn8L+jtKLw==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "13dfb7dc-05bd-4019-9cfc-df2f7fe3f1d4", "AQAAAAEAACcQAAAAEDu78O/inb4++vUfEUhQ9XER/3cC2u45VkcXjxHsBoiZqhILOiy66LHa+A/S6QrEAA==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "435f9ccf-8661-4114-9362-486adc69104e", "AQAAAAEAACcQAAAAEDfc9azMum/JNtXhfE08eNwfjoowG39PEAoAJCNJuCxNI1AgkEXrqZvtbdz3ppQwrA==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "3831e315-e099-4efb-9b06-b007c7689174", "AQAAAAEAACcQAAAAEGll64lHtcX66jrwHNUJPoD6mz84e4KauYVbzvToWij0QAiDgkCX158k1KM5uv5F7A==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "7df6f5ff-3976-4ff5-8685-0d5df9f17d4a", "AQAAAAEAACcQAAAAEKpVZ/+IoTwKHGH1Q5TD78g/ZKoxGdZ0Z0/pU8JCUowGhiHOfqGRhbA1Xrn+VDi/Sg==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "922eb229-f0de-4cd3-8f56-a2c4cfae8f45", "AQAAAAEAACcQAAAAEIUIZLkrnCFqSafagfAZworfgDIbTjac0RQcOfam9FDaGutq1AKECYwsnVVOvd6pFw==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "WorksiteId" },
                values: new object[] { "fa80674a-abe6-4978-9044-208de0e8329b", "AQAAAAEAACcQAAAAEGsdzye9ZyO2D7Ou7CHJrd6qPetxwSOcH+yMi9yPd5sT9gtFOU4SZbUTp/zLs5qDRQ==", null });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                column: "AdministratorId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2,
                column: "AdministratorId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 1,
                column: "ManagerId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 2,
                column: "ManagerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 3,
                column: "ManagerId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: 4,
                column: "ManagerId",
                value: 7);
        }
    }
}

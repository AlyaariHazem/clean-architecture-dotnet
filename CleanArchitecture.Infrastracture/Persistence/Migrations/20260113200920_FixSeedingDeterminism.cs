using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastracture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedingDeterminism : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "007266f8-a4b4-4b9e-a8d2-3e0a6f9df5ec",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33333333-3333-3333-3333-333333333333", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "PASTE_THE_PRECOMPUTED_HASH_HERE", "22222222-2222-2222-2222-222222222222" });

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b4b786c9-8b74-446a-8045-c6f1cb38ddca");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "7cfff771-c7e2-47d9-93be-637dd7ce196c");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "cfe268b3-dc5f-4834-86e5-5ee4490f8ac2");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "9cc8e7e5-6c33-4a06-b583-6b88853caab6");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "27ea9fa6-7d68-4ec2-a328-181482d7be65");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "007266f8-a4b4-4b9e-a8d2-3e0a6f9df5ec",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "200a391a-9345-4e15-b70c-8e52981dca61", new DateTime(2026, 1, 13, 22, 54, 15, 410, DateTimeKind.Local).AddTicks(8725), "AQAAAAIAAYagAAAAEK72BTZz5VwIMtfS6QAptXp6SlzcU2hGxJwYbPlciSewiTentHE7IaOBZwUUydph9Q==", "dbb68a91-fa4b-4897-9919-07e75244423a" });

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "4c6e67e3-7e91-43c3-a2e5-f15adf9ec7cb");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c717650d-c89b-4b28-81d1-55601c2823ab");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "690e35c6-29e2-44fe-8587-3f67af23c7f2");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "526c685f-3bb6-4744-81ff-bcf8136c3856");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "21379eae-49dc-45f4-befa-632af2dd4223");
        }
    }
}

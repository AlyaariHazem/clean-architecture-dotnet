using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastracture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatedSchoolTablesAndRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "007266f8-a4b4-4b9e-a8d2-3e0a6f9df5ec",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "132aa7f7-029e-4849-9bf4-6466e8024797", new DateTime(2026, 1, 13, 22, 51, 53, 201, DateTimeKind.Local).AddTicks(244), "AQAAAAIAAYagAAAAEFBuumX6gNyJA/V/lI87N83890A9It/ayBmGmR5ME7ZNzuejD1l41y5p0WQxZFK2XA==", "af39e27f-b06d-466d-bbe1-1290bc77f7eb" });

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "44c4ef24-c866-4f56-80e5-a9a73eb8b83f");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "4a506ae3-bfd8-4a4a-b12e-7e3f34d20d14");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "1a6783b8-4dac-48b4-baf4-f1aad9bf0b44");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "972809db-8185-4da9-b45a-106e8aae5b6c");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "387973b8-448b-4def-aff6-b8cff2d77380");
        }
    }
}

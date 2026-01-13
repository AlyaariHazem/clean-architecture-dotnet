using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastracture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Managers_ManagerID",
                table: "Teachers");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "44f65d88-80ff-4436-bb32-34fa3f9d1d36");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "470e8478-e533-4247-9fbf-626e30102dd7");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "69de9b69-ac2f-4349-9aff-cade12eee8b6");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "c849cef0-80b2-4548-85b1-7cedd3b9b06e");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "9e7729f4-374c-416a-8c86-d3eb20a4805a");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Managers_ManagerID",
                table: "Teachers",
                column: "ManagerID",
                principalTable: "Managers",
                principalColumn: "ManagerID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Managers_ManagerID",
                table: "Teachers");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Managers_ManagerID",
                table: "Teachers",
                column: "ManagerID",
                principalTable: "Managers",
                principalColumn: "ManagerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

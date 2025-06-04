using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineRestaurant.Services.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class NEWROLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c958a15d-0c40-4da5-b97e-5a45b58d4b17");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "864f1156-a9b6-43ec-b385-bf026ef9c5b5", null, "Admin", "ADMIN" },
                    { "f27d5862-aa17-42eb-85bd-9c6ee8d7ad26", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "864f1156-a9b6-43ec-b385-bf026ef9c5b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f27d5862-aa17-42eb-85bd-9c6ee8d7ad26");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c958a15d-0c40-4da5-b97e-5a45b58d4b17", null, "Admin", "ADMIN" });
        }
    }
}

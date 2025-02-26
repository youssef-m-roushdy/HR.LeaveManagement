using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedingLeaveTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DefaultDays", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2025, 2, 25, 19, 24, 15, 635, DateTimeKind.Local).AddTicks(335), 10, "System", new DateTime(2025, 2, 25, 19, 24, 15, 635, DateTimeKind.Local).AddTicks(364), "Vacation" },
                    { 2, "System", new DateTime(2025, 2, 25, 19, 24, 15, 635, DateTimeKind.Local).AddTicks(368), 5, "System", new DateTime(2025, 2, 25, 19, 24, 15, 635, DateTimeKind.Local).AddTicks(369), "Sick Leave" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

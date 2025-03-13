using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployeeIdToLeaveRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestingEmployeeId",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 11, 21, 15, 11, 353, DateTimeKind.Local).AddTicks(5447), new DateTime(2025, 3, 11, 21, 15, 11, 353, DateTimeKind.Local).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 11, 21, 15, 11, 353, DateTimeKind.Local).AddTicks(5483), new DateTime(2025, 3, 11, 21, 15, 11, 353, DateTimeKind.Local).AddTicks(5485) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestingEmployeeId",
                table: "LeaveRequests");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 10, 21, 31, 49, 437, DateTimeKind.Local).AddTicks(6691), new DateTime(2025, 3, 10, 21, 31, 49, 437, DateTimeKind.Local).AddTicks(6725) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 10, 21, 31, 49, 437, DateTimeKind.Local).AddTicks(6728), new DateTime(2025, 3, 10, 21, 31, 49, 437, DateTimeKind.Local).AddTicks(6730) });
        }
    }
}

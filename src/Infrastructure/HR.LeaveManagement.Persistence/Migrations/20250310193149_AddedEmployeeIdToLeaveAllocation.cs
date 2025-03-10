using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployeeIdToLeaveAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "LeaveAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "LeaveAllocations");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 7, 22, 43, 2, 675, DateTimeKind.Local).AddTicks(3169), new DateTime(2025, 3, 7, 22, 43, 2, 675, DateTimeKind.Local).AddTicks(3205) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 7, 22, 43, 2, 675, DateTimeKind.Local).AddTicks(3209), new DateTime(2025, 3, 7, 22, 43, 2, 675, DateTimeKind.Local).AddTicks(3210) });
        }
    }
}

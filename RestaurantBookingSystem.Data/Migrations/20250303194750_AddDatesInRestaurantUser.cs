using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantBookingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDatesInRestaurantUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_RestaurantUsers_EmployeeId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Reservations",
                newName: "RestaurantUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_EmployeeId",
                table: "Reservations",
                newName: "IX_Reservations_RestaurantUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "RestaurantUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDate",
                table: "RestaurantUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_RestaurantUsers_RestaurantUserId",
                table: "Reservations",
                column: "RestaurantUserId",
                principalTable: "RestaurantUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_RestaurantUsers_RestaurantUserId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "RestaurantUsers");

            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "RestaurantUsers");

            migrationBuilder.RenameColumn(
                name: "RestaurantUserId",
                table: "Reservations",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_RestaurantUserId",
                table: "Reservations",
                newName: "IX_Reservations_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_RestaurantUsers_EmployeeId",
                table: "Reservations",
                column: "EmployeeId",
                principalTable: "RestaurantUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

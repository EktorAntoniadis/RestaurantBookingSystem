using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantBookingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReservationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_RestaurantUsers_RestaurantUserId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tables");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tables",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tables",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_RestaurantUsers_RestaurantUserId",
                table: "Reservations",
                column: "RestaurantUserId",
                principalTable: "RestaurantUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

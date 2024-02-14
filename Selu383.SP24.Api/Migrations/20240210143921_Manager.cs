using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selu383.SP24.Api.Migrations
{
    /// <inheritdoc />
    public partial class Manager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Hotel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_ManagerId",
                table: "Hotel",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_AspNetUsers_ManagerId",
                table: "Hotel",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_AspNetUsers_ManagerId",
                table: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Hotel_ManagerId",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Hotel");
        }
    }
}

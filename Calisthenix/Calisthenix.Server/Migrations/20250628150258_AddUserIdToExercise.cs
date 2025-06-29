using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisthenix.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, new byte[] { 1, 2, 3 }, new byte[] { 4, 5, 6 }, "demo" });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Category", "Description", "Difficulty", "Equipment", "ImageUrl", "Name", "UserId", "VideoUrl" },
                values: new object[] { 1, "Upper Body", "A basic push-up exercise.", "Beginner", "None", "...", "Push-up", 1, "..." });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_UserId",
                table: "Exercises",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Users_UserId",
                table: "Exercises",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Users_UserId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_UserId",
                table: "Exercises");

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Exercises");
        }
    }
}

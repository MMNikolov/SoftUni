using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisthenix.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workouts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutExercises",
                columns: table => new
                {
                    WorkoutId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercises", x => new { x.WorkoutId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutExercises");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, new byte[] { 162, 244, 22, 7, 196, 148, 177, 44, 230, 67, 83, 173, 99, 145, 185, 38, 71, 137, 76, 139, 91, 30, 180, 90, 210, 152, 194, 132, 93, 125, 25, 204, 108, 2, 239, 54, 151, 251, 12, 170, 55, 138, 178, 52, 9, 44, 211, 103, 10, 179, 43, 161, 90, 145, 135, 100, 174, 67, 232, 213, 119, 30, 53, 154 }, new byte[] { 141, 178, 163, 24, 240, 98, 72, 31, 45, 144, 91, 70, 167, 250, 234, 99, 114, 136, 203, 25, 48, 35, 36, 88, 65, 214, 122, 43, 47, 21, 156, 83, 56, 163, 248, 112, 86, 99, 181, 111, 18, 4, 94, 249, 148, 228, 104, 182, 7, 101, 209, 43, 25, 207, 86, 173, 81, 119, 29, 77, 61, 208, 67, 251, 201, 190, 75, 115, 46, 21, 40, 139, 56, 32, 147, 12, 202, 53, 52, 196, 181, 30, 248, 120, 55, 28, 12, 11, 10, 146, 1, 117, 36, 244, 241, 138, 31, 73, 71, 116, 132, 187, 33, 18, 251, 21, 135, 187, 99, 103, 101, 203, 65, 47, 198, 45, 47, 65, 106, 52 }, "demo" });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Category", "Description", "Difficulty", "Equipment", "ImageUrl", "Name", "UserId", "VideoUrl" },
                values: new object[] { 1, "Upper Body", "A basic push-up exercise.", "Beginner", "None", "...", "Push-up", 1, "..." });
        }
    }
}

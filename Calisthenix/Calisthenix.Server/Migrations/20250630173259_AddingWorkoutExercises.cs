using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisthenix.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingWorkoutExercises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExerciseId1",
                table: "WorkoutExercises",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseId1",
                table: "WorkoutExercises",
                column: "ExerciseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId1",
                table: "WorkoutExercises",
                column: "ExerciseId1",
                principalTable: "Exercises",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId1",
                table: "WorkoutExercises");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutExercises_ExerciseId1",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "ExerciseId1",
                table: "WorkoutExercises");
        }
    }
}

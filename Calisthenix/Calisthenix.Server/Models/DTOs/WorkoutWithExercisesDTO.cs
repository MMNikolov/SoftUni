namespace Calisthenix.Server.Models.DTOs
{
    public class WorkoutWithExercisesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int UserId { get; set; }

        public List<ExerciseDTO> Exercises { get; set; } = new();
    }
}

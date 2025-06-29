using System.ComponentModel.DataAnnotations;

namespace Calisthenix.Server.Models
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
    }
}

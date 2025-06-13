using System.ComponentModel.DataAnnotations;
using static Calisthenix.Server.GlobalConstants.ExerciseConstants;
namespace Calisthenix.Server.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(MinExerciseNameLength)]
        [MaxLength(MaxExerciseNameLength)]
        public string Name { get; set; } = null!; // e.g., "Push-Up", "Pull-Up", "Squat"

        [Required]
        [MinLength(MinExerciseDescriptionLength)]
        [MaxLength(MaxExerciseDescriptionLength)]
        public string Description { get; set; } = null!; // e.g., "A basic bodyweight exercise for upper body strength."

        [Required]
        [MinLength(MinExerciseCategoryLength)]
        [MaxLength(MaxExerciseCategoryLength)]
        public string Category { get; set; } = null!; // e.g., "Push", "Pull", "Legs"

        [Required]
        [MinLength(MinExerciseEquipmentLength)]
        [MaxLength(MaxExerciseEquipmentLength)]
        public string Equipment { get; set; } = null!; // e.g., "Bodyweight", "Dumbbell", "Barbell"

        [Required]
        [MinLength(MinExerciseDifficultyLength)]
        [MaxLength(MaxExerciseDifficultyLength)]
        public string Difficulty { get; set; } = null!; // e.g., "Beginner", "Intermediate", "Advanced"

        public string? VideoUrl { get; set; } // URL to a video demonstration
        public string? ImageUrl { get; set; } // URL to an image of the exercise
    }
}

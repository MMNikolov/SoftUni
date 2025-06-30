namespace Calisthenix.Server.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Calisthenix.Server.GlobalConstants.ExerciseConstants;

    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(MinExerciseNameLength)]
        [MaxLength(MaxExerciseNameLength)]
        public string Name { get; set; } = null!; 

        [Required]
        [MinLength(MinExerciseDescriptionLength)]
        [MaxLength(MaxExerciseDescriptionLength)]
        public string Description { get; set; } = null!; 

        [Required]
        [MinLength(MinExerciseCategoryLength)]
        [MaxLength(MaxExerciseCategoryLength)]
        public string Category { get; set; } = null!; 

        [Required]
        [MinLength(MinExerciseEquipmentLength)]
        [MaxLength(MaxExerciseEquipmentLength)]
        public string Equipment { get; set; } = null!; 

        [Required]
        [MinLength(MinExerciseDifficultyLength)]
        [MaxLength(MaxExerciseDifficultyLength)]
        public string Difficulty { get; set; } = null!; 

        public string? VideoUrl { get; set; } 
        public string? ImageUrl { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<WorkoutExercise>? WorkoutExercises { get; set; }
    }
}

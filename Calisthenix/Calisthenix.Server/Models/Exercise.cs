namespace Calisthenix.Server.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // e.g., "Push-Up", "Pull-Up", "Squat"
        public string Description { get; set; } = null!; // e.g., "A basic bodyweight exercise for upper body strength."
        public string Category { get; set; } = null!; // e.g., "Push", "Pull", "Legs"
        public string Equipment { get; set; } = null!; // e.g., "Bodyweight", "Dumbbell", "Barbell"
        public string Difficulty { get; set; } = null!; // e.g., "Beginner", "Intermediate", "Advanced"
        public string? VideoUrl { get; set; } // URL to a video demonstration
        public string? ImageUrl { get; set; } // URL to an image of the exercise
    }
}

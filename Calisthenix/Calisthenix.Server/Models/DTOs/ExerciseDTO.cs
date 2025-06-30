namespace Calisthenix.Server.Models.DTOs
{
    public class ExerciseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Equipment { get; set; } = null!;
        public string Difficulty { get; set; } = null!;
        public string? VideoUrl { get; set; }
        public string? ImageUrl { get; set; } 
        public string UserName { get; set; } = null!;
    }
}

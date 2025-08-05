namespace Calisthenix.Server.Data
{
    using Calisthenix.Server.Models;
    using Microsoft.EntityFrameworkCore;

    public static class DbInitializer
    {
        public static void Seed(CalisthenixDbContext context)
        {
            context.Database.EnsureCreated();
            
            if (context.Exercises.Any()) return;
            
            var exercises = new List<Exercise>
            {
                new Exercise
                {
                    Name = "Push-up",
                    Description = "A basic upper body exercise.",
                    Category = "Push",
                    Equipment = "None",
                    Difficulty = "Beginner",
                    ImageUrl = "https://img.freepik.com/free-photo/young-powerful-sportsman-training-push-ups-dark-wall_176420-537.jpg",
                    VideoUrl = "https://youtube.com/pushup-tutorial",
                    UserId = 5
                },
                new Exercise
                {
                    Name = "Pull-up",
                    Description = "A compound exercise targeting the back and biceps.",
                    Category = "Pull",
                    Equipment = "Pull-up bar",
                    Difficulty = "Intermediate",
                    ImageUrl = "https://t3.ftcdn.net/jpg/01/42/58/38/360_F_142583801_56XCH5zG6KeW288T9Xjrpjsbwu4nT6KO.jpg",
                    VideoUrl = "https://youtube.com/pullup-tutorial",
                    UserId = 5
                },
                new Exercise
                {
                    Name = "Squat",
                    Description = "A lower body movement working quads and glutes.",
                    Category = "Legs",
                    Equipment = "None",
                    Difficulty = "Beginner",
                    ImageUrl = "https://hips.hearstapps.com/hmg-prod/images/man-doing-gym-front-squat-royalty-free-image-1649261343.jpg",
                    VideoUrl = "https://youtube.com/squat-tutorial",
                    UserId = 5
                },
                new Exercise
                {
                    Name = "Plank",
                    Description = "An isometric core exercise.",
                    Category = "Core",
                    Equipment = "None",
                    Difficulty = "Beginner",
                    ImageUrl = "https://media.istockphoto.com/id/924765866/photo/every-day-routine.jpg?s=612x612&w=0&k=20&c=2a_lwvfDbzj_wan_q89ak5pS6eQy8eMs0oWaLOo2sxc=",
                    VideoUrl = "https://youtube.com/plank-tutorial",
                    UserId = 5
                },
                new Exercise
                {
                    Name = "Burpee",
                    Description = "A full-body exercise combining a squat, push-up, and jump.",
                    Category = "Core",
                    Equipment = "None",
                    Difficulty = "Intermediate",
                    ImageUrl = "https://hips.hearstapps.com/hmg-prod/images/photo-23-07-2019-20-27-08-1593507744.jpg?resize=980:*",
                    VideoUrl = "https://youtube.com/burpee-tutorial",
                    UserId = 5
                }
            };
            
            context.Exercises.AddRange(exercises);
            context.SaveChanges();
        }
    }
}

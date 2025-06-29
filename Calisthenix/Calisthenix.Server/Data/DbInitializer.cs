namespace Calisthenix.Server.Data
{
    using Calisthenix.Server.Models;
    using Microsoft.EntityFrameworkCore;

    public static class DbInitializer
    {
        public static void Seed(CalisthenixDbContext context)
        {
            //context.Database.EnsureCreated();
            //
            //if (context.Exercises.Any()) return;
            //
            //var exercises = new List<Exercise>
            //{
            //    new Exercise
            //    {
            //        Name = "Push-up",
            //        Description = "A basic upper body exercise.",
            //        Category = "Upper Body",
            //        Equipment = "None",
            //        Difficulty = "Beginner",
            //        ImageUrl = "https://example.com/pushup.jpg",
            //        VideoUrl = "https://youtube.com/pushup-tutorial",
            //        //UserId = 4
            //    },
            //    new Exercise
            //    {
            //        Name = "Pull-up",
            //        Description = "A compound exercise targeting the back and biceps.",
            //        Category = "Upper Body",
            //        Equipment = "Pull-up bar",
            //        Difficulty = "Intermediate",
            //        ImageUrl = "https://example.com/pullup.jpg",
            //        VideoUrl = "https://youtube.com/pullup-tutorial",
            //        //UserId = 4
            //    },
            //    new Exercise
            //    {
            //        Name = "Squat",
            //        Description = "A lower body movement working quads and glutes.",
            //        Category = "Lower Body",
            //        Equipment = "None",
            //        Difficulty = "Beginner",
            //        ImageUrl = "https://example.com/squat.jpg",
            //        VideoUrl = "https://youtube.com/squat-tutorial",
            //        //UserId = 4
            //    },
            //};
            //
            //context.Exercises.AddRange(exercises);
            //context.SaveChanges();
        }
    }
}

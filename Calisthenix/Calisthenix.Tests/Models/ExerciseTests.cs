using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calisthenix.Server.Models;

namespace Calisthenix.Tests.Models
{
    public class ExerciseTests
    {
        [Fact]
        public void Exercise_Properties_SetCorrectly()
        {
            var exercise = new Exercise
            {
                Id = 1,
                Name = "Push Up",
                Description = "Bodyweight push",
                Category = "Strength",
                Equipment = "None",
                Difficulty = "Beginner",
                VideoUrl = "https://vid.com",
                ImageUrl = "https://img.com",
                UserId = 9,
                User = new User { Id = 9 }
            };

            Assert.Equal("Push Up", exercise.Name);
            Assert.Equal("Bodyweight push", exercise.Description);
            Assert.Equal("Strength", exercise.Category);
            Assert.Equal("None", exercise.Equipment);
            Assert.Equal("Beginner", exercise.Difficulty);
            Assert.Equal("https://vid.com", exercise.VideoUrl);
            Assert.Equal("https://img.com", exercise.ImageUrl);
            Assert.Equal(9, exercise.UserId);
            Assert.NotNull(exercise.User);
            Assert.Null(exercise.WorkoutExercises);
            Assert.Null(exercise.Comments);
        }
    }

}

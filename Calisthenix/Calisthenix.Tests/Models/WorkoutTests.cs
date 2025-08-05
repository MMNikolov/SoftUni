using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calisthenix.Server.Models;

namespace Calisthenix.Tests.Models
{
    public class WorkoutTests
    {
        [Fact]
        public void Workout_Properties_SetCorrectly()
        {
            var workout = new Workout
            {
                Id = 1,
                Name = "Leg Day",
                UserId = 4,
                User = new User { Id = 4 }
            };

            Assert.Equal(1, workout.Id);
            Assert.Equal("Leg Day", workout.Name);
            Assert.Equal(4, workout.UserId);
            Assert.NotNull(workout.User);
            Assert.NotNull(workout.WorkoutExercises);
            Assert.Empty(workout.WorkoutExercises);
        }
    }

}

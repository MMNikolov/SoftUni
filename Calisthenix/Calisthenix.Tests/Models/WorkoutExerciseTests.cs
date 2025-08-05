using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calisthenix.Server.Models;

namespace Calisthenix.Tests.Models
{
    public class WorkoutExerciseTests
    {
        [Fact]
        public void WorkoutExercise_Properties_AssignedCorrectly()
        {
            var we = new WorkoutExercise
            {
                WorkoutId = 1,
                ExerciseId = 2,
                Workout = new Workout { Id = 1 },
                Exercise = new Exercise { Id = 2 }
            };

            Assert.Equal(1, we.WorkoutId);
            Assert.Equal(2, we.ExerciseId);
            Assert.NotNull(we.Workout);
            Assert.NotNull(we.Exercise);
        }
    }
}

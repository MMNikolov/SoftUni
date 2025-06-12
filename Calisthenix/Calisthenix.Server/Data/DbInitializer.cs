using Calisthenix.Server.Models;

namespace Calisthenix.Server.Data
{
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
                    Category = "Upper Body",
                    Equipment = "None",
                    Difficulty = "Beginner",
                    ImageUrl = "https://example.com/pushup.jpg",
                    VideoUrl = "https://youtube.com/pushup-tutorial"
                },
                new Exercise
                {
                    Name = "Pull-up",
                    Description = "A compound exercise targeting the back and biceps.",
                    Category = "Upper Body",
                    Equipment = "Pull-up bar",
                    Difficulty = "Intermediate",
                    ImageUrl = "https://example.com/pullup.jpg",
                    VideoUrl = "https://youtube.com/pullup-tutorial"
                },
                new Exercise
                {
                    Name = "Squat",
                    Description = "A lower body movement working quads and glutes.",
                    Category = "Lower Body",
                    Equipment = "None",
                    Difficulty = "Beginner",
                    ImageUrl = "https://example.com/squat.jpg",
                    VideoUrl = "https://youtube.com/squat-tutorial"
                },
                new Exercise
                {
                    Name = "Deadlift",
                    Description = "A full-body exercise focusing on the posterior chain.",
                    Category = "Full Body",
                    Equipment = "Barbell",
                    Difficulty = "Advanced",
                    ImageUrl = "https://example.com/deadlift.jpg",
                    VideoUrl = "https://youtube.com/deadlift-tutorial"
                },
                new Exercise
                {
                    Name = "Plank",
                    Description = "An isometric core exercise.",
                    Category = "Core",
                    Equipment = "None",
                    Difficulty = "Beginner",
                    ImageUrl = "https://example.com/plank.jpg",
                    VideoUrl = "https://youtube.com/plank-tutorial"
                },
                new Exercise
                {
                    Name = "Lunge",
                    Description = "A lower body exercise targeting quads and glutes.",
                    Category = "Lower Body",
                    Equipment = "None",
                    Difficulty = "Beginner",
                    ImageUrl = "https://example.com/lunge.jpg",
                    VideoUrl = "https://youtube.com/lunge-tutorial"
                },
                new Exercise
                {
                    Name = "Burpee",
                    Description = "A full-body exercise combining a squat, push-up, and jump.",
                    Category = "Full Body",
                    Equipment = "None",
                    Difficulty = "Intermediate",
                    ImageUrl = "https://example.com/burpee.jpg",
                    VideoUrl = "https://youtube.com/burpee-tutorial"
                },
                new Exercise
                {
                    Name = "Dumbbell Shoulder Press",
                    Description = "An upper body exercise targeting the shoulders.",
                    Category = "Upper Body",
                    Equipment = "Dumbbells",
                    Difficulty = "Intermediate",
                    ImageUrl = "https://example.com/shoulder-press.jpg",
                    VideoUrl = "https://youtube.com/shoulder-press-tutorial"
                },
                new Exercise
                {
                    Name = "Leg Raise",
                    Description = "A core exercise focusing on the lower abs.",
                    Category = "Core",
                    Equipment = "None",
                    Difficulty = "Intermediate",
                    ImageUrl = "https://example.com/leg-raise.jpg",
                    VideoUrl = "https://youtube.com/leg-raise-tutorial"
                },
                new Exercise
                {
                    Name = "Mountain Climber",
                    Description = "A cardio and core exercise.",
                    Category = "Full Body",
                    Equipment = "None",
                    Difficulty = "Intermediate",
                    ImageUrl = "https://example.com/mountain-climber.jpg",
                    VideoUrl = "https://youtube.com/mountain-climber-tutorial"
                },
                new Exercise
                {
                    Name = "Dips",
                    Description = "An upper body compound movement for chest and triceps.",
                    Category = "Upper Body",
                    Equipment = "Parallel Bars",
                    Difficulty = "Intermediate",
                    ImageUrl = "https://example.com/images/dips.jpg",
                    VideoUrl = "https://www.youtube.com/watch?v=2z8JmcrW-As"
                },
                new Exercise
                {
                    Name = "Glute Bridge",
                    Description = "A lower body exercise focusing on the glutes and hamstrings.",
                    Category = "Lower Body",
                    Equipment = "None",
                    Difficulty = "Beginner",
                    ImageUrl = "https://example.com/glute-bridge.jpg",
                    VideoUrl = "https://youtube.com/glute-bridge-tutorial"
                },
                new Exercise
                {
                    Name = "Russian Twist",
                    Description = "A core exercise that targets the obliques.",
                    Category = "Core",
                    Equipment = "None",
                    Difficulty = "Intermediate",
                    ImageUrl = "https://example.com/russian-twist.jpg",
                    VideoUrl = "https://youtube.com/russian-twist-tutorial"
                },
                new Exercise
                {
                    Name = "Box Jump",
                    Description = "A plyometric exercise for lower body power.",
                    Category = "Lower Body",
                    Equipment = "Box",
                    Difficulty = "Intermediate",
                    ImageUrl = "https://example.com/box-jump.jpg",
                    VideoUrl = "https://youtube.com/box-jump-tutorial"
                },
                new Exercise
                {
                    Name = "L-sit",
                    Description = "Advanced core and shoulder hold performed on the floor or bars.",
                    Category = "Core",
                    Equipment = "Parallel Bars (or floor)",
                    Difficulty = "Advanced",
                    ImageUrl = "https://example.com/images/lsit.jpg",
                    VideoUrl = "https://www.youtube.com/watch?v=1PJluAChD9c"
                },
                new Exercise
                {
                    Name = "Handstand Push-up",
                    Description = "An advanced upper body exercise performed upside down.",
                    Category = "Upper Body",
                    Equipment = "Wall (for support)",
                    Difficulty = "Advanced",
                    ImageUrl = "https://example.com/handstand-pushup.jpg",
                    VideoUrl = "https://youtube.com/handstand-pushup-tutorial"
                },
                new Exercise
                {
                    Name = "Handstand",
                    Description = "A balance-intensive move engaging shoulders, arms, and core.",
                    Category = "Upper Body",
                    Equipment = "Wall (optional)",
                    Difficulty = "Advanced",
                    ImageUrl = "https://example.com/images/handstand.jpg",
                    VideoUrl = "https://www.youtube.com/watch?v=ieHGRcFjB2g"
                },
                new Exercise
                {
                    Name = "Planche",
                    Description = "An advanced bodyweight hold requiring significant strength.",
                    Category = "Core",
                    Equipment = "None",
                    Difficulty = "Expert",
                    ImageUrl = "https://example.com/planche.jpg",
                    VideoUrl = "https://youtube.com/planche-tutorial"
                },
                new Exercise
                {
                    Name = "Muscle-up",
                    Description = "A combination of a pull-up and dip, performed on bars.",
                    Category = "Upper Body",
                    Equipment = "Pull-up Bar",
                    Difficulty = "Expert",
                    ImageUrl = "https://example.com/muscle-up.jpg",
                    VideoUrl = "https://youtube.com/muscle-up-tutorial"
                },
                new Exercise
                {
                    Name = "Front Lever",
                    Description = "An advanced bodyweight hold that requires significant core strength.",
                    Category = "Core",
                    Equipment = "Pull-up Bar",
                    Difficulty = "Expert",
                    ImageUrl = "https://example.com/front-lever.jpg",
                    VideoUrl = "https://youtube.com/front-lever-tutorial"
                },
                new Exercise
                {
                    Name = "Back Lever",
                    Description = "An advanced bodyweight hold that requires significant shoulder and core strength.",
                    Category = "Core",
                    Equipment = "Pull-up Bar",
                    Difficulty = "Expert",
                    ImageUrl = "https://example.com/back-lever.jpg",
                    VideoUrl = "https://youtube.com/back-lever-tutorial"
                },
                new Exercise
                {
                    Name = "Pistol Squat",
                    Description = "A challenging lower body exercise focusing on balance and strength.",
                    Category = "Lower Body",
                    Equipment = "None",
                    Difficulty = "Advanced",
                    ImageUrl = "https://example.com/single-leg-squat.jpg",
                    VideoUrl = "https://youtube.com/single-leg-squat-tutorial"
                },
                new Exercise
                {
                    Name = "Tuck Planche",
                    Description = "A progression towards the full planche, focusing on core and shoulder strength.",
                    Category = "Core",
                    Equipment = "None",
                    Difficulty = "Advanced",
                    ImageUrl = "https://example.com/tuck-planche.jpg",
                    VideoUrl = "https://youtube.com/tuck-planche-tutorial"
                },
                new Exercise
                {
                    Name = "V-sit",
                    Description = "An advanced core exercise that requires significant flexibility and strength.",
                    Category = "Core",
                    Equipment = "None",
                    Difficulty = "Expert",
                    ImageUrl = "https://example.com/v-sit.jpg",
                    VideoUrl = "https://youtube.com/v-sit-tutorial"
                },
                new Exercise
                {
                    Name = "Single Arm Push-up",
                    Description = "An advanced variation of the push-up that requires significant strength and balance.",
                    Category = "Upper Body",
                    Equipment = "None",
                    Difficulty = "Expert",
                    ImageUrl = "https://example.com/single-arm-pushup.jpg",
                    VideoUrl = "https://youtube.com/single-arm-pushup-tutorial"
                },
                new Exercise
                {
                    Name = "Archer Push-up",
                    Description = "A variation of the push-up that emphasizes one arm more than the other.",
                    Category = "Upper Body",
                    Equipment = "None",
                    Difficulty = "Advanced",
                    ImageUrl = "https://example.com/archer-pushup.jpg",
                    VideoUrl = "https://youtube.com/archer-pushup-tutorial"
                },
                new Exercise
                {
                    Name = "Wall Walk",
                    Description = "An advanced exercise that builds shoulder strength and stability.",
                    Category = "Upper Body",
                    Equipment = "Wall",
                    Difficulty = "Advanced",
                    ImageUrl = "https://example.com/wall-walk.jpg",
                    VideoUrl = "https://youtube.com/wall-walk-tutorial"
                },
                new Exercise
                {
                    Name = "Dragon Flag",
                    Description = "An advanced core exercise that requires significant strength and control.",
                    Category = "Core",
                    Equipment = "None",
                    Difficulty = "Expert",
                    ImageUrl = "https://example.com/dragon-flag.jpg",
                    VideoUrl = "https://youtube.com/dragon-flag-tutorial"
                },
                new Exercise
                {
                    Name = "Handstand Walk",
                    Description = "An advanced skill that requires balance and shoulder strength.",
                    Category = "Upper Body",
                    Equipment = "None",
                    Difficulty = "Expert",
                    ImageUrl = "https://example.com/handstand-walk.jpg",
                    VideoUrl = "https://youtube.com/handstand-walk-tutorial"
                },
                new Exercise
                {
                    Name = "Human Flag",
                    Description = "An advanced bodyweight exercise that requires significant strength and balance.",
                    Category = "Core",
                    Equipment = "Pole or Bar",
                    Difficulty = "Expert",
                    ImageUrl = "https://example.com/human-flag.jpg",
                    VideoUrl = "https://youtube.com/human-flag-tutorial"
                },
            };

            context.Exercises.AddRange(exercises);
            context.SaveChanges();
        }
    }
}

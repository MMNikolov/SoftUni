using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calisthenix.Server.Models;

namespace Calisthenix.Tests.Models
{
    public class CommentTests
    {
        [Fact]
        public void Comment_Properties_SetCorrectly()
        {
            var comment = new Comment
            {
                Id = 1,
                Content = "Nice",
                CreatedAt = DateTime.UtcNow,
                ExerciseId = 10,
                UserId = 5,
                Exercise = new Exercise { Id = 10 },
                User = new User { Id = 5 }
            };

            Assert.Equal(1, comment.Id);
            Assert.Equal("Nice", comment.Content);
            Assert.Equal(10, comment.ExerciseId);
            Assert.Equal(5, comment.UserId);
            Assert.NotNull(comment.Reactions);
            Assert.Empty(comment.Reactions);
        }
    }
}

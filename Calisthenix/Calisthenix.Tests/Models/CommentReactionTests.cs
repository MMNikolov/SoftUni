using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calisthenix.Server.Models;

namespace Calisthenix.Tests.Models
{
    public class CommentReactionTests
    {
        [Fact]
        public void CommentReaction_Properties_SetCorrectly()
        {
            var reaction = new CommentReaction
            {
                Id = 1,
                CommentId = 2,
                UserId = 3,
                IsThumbsUp = true,
                Comment = new Comment { Id = 2 },
                User = new User { Id = 3 }
            };

            Assert.Equal(1, reaction.Id);
            Assert.Equal(2, reaction.CommentId);
            Assert.Equal(3, reaction.UserId);
            Assert.True(reaction.IsThumbsUp);
        }
    }

}

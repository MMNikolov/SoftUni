using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calisthenix.Server.Models;

namespace Calisthenix.Tests.Models
{
    public class UserTests
    {
        [Fact]
        public void User_Properties_InitializeCorrectly()
        {
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                PasswordHash = "secure"
            };

            Assert.Equal(1, user.Id);
            Assert.Equal("testuser", user.Username);
            Assert.Equal("secure", user.PasswordHash);
            Assert.NotNull(user.Exercises);
            Assert.Empty(user.Exercises);
        }
    }

}

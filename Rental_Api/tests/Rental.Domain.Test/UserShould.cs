using System;
using System.Collections.Generic;
using System.Text;
using Rental.Domain.Entities;
using Xunit;

namespace Rental.Domain.Test
{
    public class UserShould
    {
        [InlineData(0, "","", null, null, "")]
        [InlineData(1, "", "", null, null, "")]
        [InlineData(1, "test", "", null, null, "")]
        [InlineData(1, "test", "name", null, null, "")]
        [InlineData(1, "test", "name", null, null, "role")]
        [Theory]
        public void Not_Be_Created_Because_Of_Not_Valid_Fields(int id, string userName, string name, byte[] passwordHash, byte[] passwordSalt, string role)
        {
            var userResult = User.Create(id, userName, name, passwordHash, passwordSalt, role);
            Assert.True(userResult.IsFailure);
            Assert.Contains("not valid", userResult.Error);
        }

        [InlineData(1, "userName", "Name",  "ADMIN")]
        [InlineData(0, "userName", "Name", "ADMIN")]
        [Theory]
        public void Be_Created_Because_Of_Valid_Values(int id, string userName, string name,  string role)
        {
            byte[] hash = new byte[1];
            byte[] salt = new byte[1];
            var userResult = User.Create(id, userName, name, hash, salt, role);
            Assert.True(userResult.IsSuccess);
            Assert.IsType<User>(userResult.Value);
        }
    }
}

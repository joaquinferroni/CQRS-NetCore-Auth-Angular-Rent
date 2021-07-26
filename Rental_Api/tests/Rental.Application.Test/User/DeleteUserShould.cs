using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Rental.Application.UseCases.UserSettings.DeleteUser;
using Rental.Application.UseCases.UserSettings.FindUser;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Xunit;

namespace Rental.Application.Test.User
{
    public class DeleteUserShould
    {
        private readonly Mock<IUserRepository> _repository;

        private readonly DeleteUserHandler _handler;
        public DeleteUserShould()
        {
            _repository = new Mock<IUserRepository>();
            _handler = new DeleteUserHandler(_repository.Object);

        }
        [Fact]
        public async Task Return_An_Error_Because_Of_User_Does_Not_Exists()
        {
            _repository.Setup(x => x.GetByUserName(It.IsAny<string>()))
                .Returns(() => Result.Success<Domain.Entities.User>(null));

            var result = await _handler.Handle(new DeleteUserCommand(){UserName = "test"}, CancellationToken.None);
            result.IsSuccess.Should().BeFalse();
            result.Error.Contains("There is no a user with that username");
        }

    }
}

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Rental.Application.UseCases.AuthSettings.Authenticate;
using Rental.Application.UseCases.UserSettings.CreateUser;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Xunit;

namespace Rental.Application.Test.UserTest
{
    public class CreateUserShould
    {
        private readonly Mock<IUserRepository> _repository;

        private readonly CreateUserHandler _handler;
        public CreateUserShould()
        {
            _repository = new Mock<IUserRepository>();
            _handler = new CreateUserHandler(_repository.Object);

        }
        [Fact]
        public async Task Return_An_Error_Because_Of_UserName_Already_Exists()
        {
            _repository.Setup(x => x.GetByUserName(It.IsAny<string>()))
                .Returns(() => Result.Success<User>(
                    User.Create(0,"fdsa","fdsa",new byte[2],new byte[2], "dsa" ).Value
                    ));

            var result = await _handler.Handle(new CreateUserCommand()
            {
                Name = "fsa",Role = "ADMIN",Password = "1234",UserName = "test"
            }, CancellationToken.None);
            result.IsSuccess.Should().BeFalse();
        }

    }
}

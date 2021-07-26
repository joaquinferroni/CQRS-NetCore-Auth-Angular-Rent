
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Rental.Application.UseCases.AuthSettings.Authenticate;
using Rental.Domain.Dtos;
using Rental.Domain.Entities;
using Rental.Domain.Infrastructure;
using Rental.Domain.Interfaces;
using Xunit;

namespace Rental.Application.Test.Auth
{
   
    public class AuthShould
    {
        private readonly Mock<IUserRepository> _repository;

        private readonly AuthenticateHandler _handler;
        public AuthShould()
        {
            _repository = new Mock<IUserRepository>();
            var ao = new AuthOptions(){ExpireHours = 24,Secret = "marcy9d8534b48w951b9287d492b256x" };
            _handler = new AuthenticateHandler(Options.Create(ao),_repository.Object);

        }
        [Fact]
        public async Task Return_An_Error_Because_Of_UserName_Not_Exists()
        {
            _repository.Setup(x => x.GetByUserName(It.IsAny<string>()))
                .Returns(() => Result.Success<Domain.Entities.User>(null));

            var result = await _handler.Handle(new AuthenticateCommand()
            {
                UserName = "test",
                Password = "123456"
            }, CancellationToken.None);
            result.IsSuccess.Should().BeFalse();
        }

    }
}

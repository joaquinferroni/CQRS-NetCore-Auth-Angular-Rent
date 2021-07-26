
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Rental.Application.UseCases.AuthSettings.Authenticate;
using Rental.Application.UseCases.UserSettings.CreateUser;
using Rental.Application.UseCases.UserSettings.FindUser;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Xunit;

namespace Rental.Application.Test.UserTest
{
    public class FindUserShould
    {
        private readonly Mock<IUserRepository> _repository;

        private readonly GetAllUserHandler _handler;
        public FindUserShould()
        {
            _repository = new Mock<IUserRepository>();
            _handler = new GetAllUserHandler(_repository.Object);

        }
        [Fact]
        public async Task Return_A_List_Of_2_Users()
        {
            _repository.Setup(x => x.GetAll())
                .Returns(() => Result.Success<IEnumerable<Domain.Entities.User>>(
                    new List<Domain.Entities.User>()
                    {
                        Domain.Entities.User.Create(0,"fdsa","fdsa",new byte[2],new byte[2], "dsa" ).Value,
                        Domain.Entities.User.Create(0,"fdsa","fdsa",new byte[2],new byte[2], "dsa" ).Value
                    }
                ));

            var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);
            result.IsSuccess.Should().BeTrue();
            result.Value.Count().Should().Be(2);
        }

    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Rental.Api.Infrastructure.HttpResponses;
using Rental.Domain.Dtos;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental.Application.UseCases.UserSettings.CreateUser;
using Rental.Application.UseCases.ApartmentSettings.FindUserApartment;
using Rental.Application.UseCases.UserSettings.DeleteUser;
using Rental.Application.UseCases.UserSettings.FindUser;
using Rental.Application.UseCases.UserSettings.UpdateUser;
using Rental.Domain.Entities;
using Rental.Domain.Enums;

namespace Rental.Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Put([FromRoute] int id,[FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            return (await _mediator.Send(command)).ToHttpResponse();

        } 

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
            => (await _mediator.Send(command)).ToHttpResponse();


        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Get()
            => (await _mediator.Send(new GetAllUsersQuery())).ToHttpResponse();


        [HttpGet("Apartments")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<Apartment>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetMyApartments([FromQuery] ApartmentStatusEnum? status)
            => (await _mediator.Send(new GetUserApartmentQuery()
            {
                UserName = User.Identity.Name,
                Status = status
            })).ToHttpResponse();


        [HttpDelete("{UserName}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserCommand query)
            => (await _mediator.Send(query)).ToHttpResponse();
    }
}

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental.Api.Infrastructure.HttpResponses;
using Rental.Application.UseCases.ApartmentSettings.CreateApartment;
using Rental.Application.UseCases.ApartmentSettings.DeleteApartment;
using Rental.Application.UseCases.ApartmentSettings.FindApartment;
using Rental.Application.UseCases.ApartmentSettings.UpdateApartment;
using Rental.Application.UseCases.UserSettings.CreateUser;
using Rental.Domain.Dto;
using Rental.Domain.Entities;
using Rental.Domain.Enums;

namespace Rental.Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ApartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Authorize(Roles = "REALTOR, ADMIN")]
        [ProducesResponseType(typeof(Apartment), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Post([FromBody] CreateApartmentCommand command)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "REALTOR";
            if(role == "REALTOR" || string.IsNullOrWhiteSpace(command.UserName))
                command.UserName = User.Identity.Name;
            return (await _mediator.Send(command)).ToHttpResponse();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "REALTOR, ADMIN")]
        [ProducesResponseType(typeof(Apartment), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateApartmentCommand command)
        {
            command.Id = id;
            command.Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "REALTOR";
            return (await _mediator.Send(command)).ToHttpResponse();
        }

        [HttpGet("All")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ApartmentDto), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetAll([FromQuery] ApartmentStatusEnum? status,
            [FromQuery] GetAllApartmentQuery query)
        {
            query.Status = status;
            return (await _mediator.Send(query)).ToHttpResponse();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ApartmentDto), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetAll([FromQuery]GetAllApartmentQuery query)
            => (await _mediator.Send(query)).ToHttpResponse();

        [HttpDelete("{Id}")]
        [Authorize(Roles = "REALTOR, ADMIN")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Delete([FromRoute] DeleteApartmentCommand command)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "REALTOR";
            command.Role = role;
            if (role == "REALTOR" || string.IsNullOrWhiteSpace(command.UserName))
                command.UserName = User.Identity.Name;
            return (await _mediator.Send(command)).ToHttpResponse();
        }
    }
}
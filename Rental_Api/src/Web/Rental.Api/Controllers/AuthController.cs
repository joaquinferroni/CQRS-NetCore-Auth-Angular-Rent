using System.Collections.Generic;
using System.Threading.Tasks;
using Rental.Api.Infrastructure.HttpResponses;
using Rental.Domain.Dtos;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rental.Application.UseCases.AuthSettings.Authenticate;

namespace Rental.Api.Controllers
{

    [Route("Api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;

        public AuthController(ILogger<AuthController> logger,
                                IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<AuthModelDto>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Post([FromBody] AuthenticateCommand command)
        => (await _mediator.Send(command)).ToHttpResponse();

    }
}

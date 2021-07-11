using CSharpFunctionalExtensions;
using Rental.Domain.ResultMessages;
using Microsoft.AspNetCore.Mvc;

namespace Rental.Api.Infrastructure.HttpResponses
{
    public static class FromResultsToHttpCode
    {
        public static IActionResult ToHttpResponse(this Result result) => result switch
        {
            { IsSuccess: false } e  => new BadRequestObjectResult(e),
            { IsSuccess: true } => new EmptyResult()
            //_ => throw new System.NotImplementedException(),
        };
        public static IActionResult ToHttpResponse<T>(this Result<T, ResultError> result) => result switch
        {
            { IsSuccess: false } e when e.Error is DoesNotExistError  => new NotFoundObjectResult(e.Error.Message),
            { IsSuccess: false } e when e.Error is BadInputError=> new BadRequestObjectResult(e.Error.Message),
            { IsSuccess: false } e => new BadRequestObjectResult(e.Error),
            { IsSuccess: true }  r when r.Value is null=> new EmptyResult(),
            { IsSuccess: true }  r => new OkObjectResult(r.Value)
            //_ => throw new System.NotImplementedException(),
        };
        public static IActionResult ToHttpResponse<T>(this Result<T> result) => result switch
        {
            //TODO: Solve it 
            { IsSuccess: false } e => new BadRequestObjectResult(e.Error),
            { IsSuccess: true } r => new OkObjectResult(r.Value)
            //_ => new EmptyResult(),
        };
    }
}

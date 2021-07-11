using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace Rental.Host.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private string _body;
        public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await ReadBody(context);

                await _next(context);
            }
            catch (Exception ex)
            {

                LogRequest(context, ex);
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = 500;
                await response.WriteAsync(JsonSerializer.Serialize(new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "https://httpstatuses.com/500",
                    Title = ex.Message,
                    Detail = _webHostEnvironment.EnvironmentName == "Development" ? ex.StackTrace : string.Empty,
                    Instance = response.HttpContext.Request.Path
                }));
            }
        }
        private void LogRequest(HttpContext context, Exception ex) =>
            _logger.LogError($"Http Request Global Exception{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request_Body: {_body} " +
                                   $"Exception: {FlattenException(ex)}");

        private async Task ReadBody(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = new RecyclableMemoryStreamManager().GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _body = ReadStreamInChunks(requestStream);
            context.Request.Body.Position = 0;
        }
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                    0,
                    readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
        private string FlattenException(Exception exception)
        {
            var stringBuilder = new StringBuilder();

            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
            }

            return stringBuilder.ToString();
        }
    }
}

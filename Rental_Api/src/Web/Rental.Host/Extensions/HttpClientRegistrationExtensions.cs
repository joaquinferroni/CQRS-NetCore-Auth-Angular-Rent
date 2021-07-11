using System;
using System.Net.Http;
using Rental.Api.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace Rental.Host.Extensions
{
    public static class HttpClientRegistrationExtensions
    {
        private static ILogger<Startup> _logger;
        private static HealthCheckCircuitBreakerService _healthCheckCircuitBreakerService;

        public static IServiceCollection AddHttpClientRegistration(this IServiceCollection services)
        {
            _logger = services.BuildServiceProvider(true).GetRequiredService<ILogger<Startup>>();

            _healthCheckCircuitBreakerService = services.BuildServiceProvider().GetRequiredService<HealthCheckCircuitBreakerService>();

            return services;
        }

      
    }
}

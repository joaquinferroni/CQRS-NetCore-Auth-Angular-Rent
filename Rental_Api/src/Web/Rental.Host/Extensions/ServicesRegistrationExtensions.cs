using Rental.Api.Infrastructure;
using Rental.Domain.Interfaces;
using Rental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Rental.Host.Extensions
{
    public static class ServicesRegistrationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(new HealthCheckCircuitBreakerService());
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IApartmentRepository, ApartmentRepository>();
            return services;
        }
    }

}

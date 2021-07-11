using Rental.Host.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Rental.Host.Extensions
{
   public static class MediatRRegistrationExtzensions
    {
        public static IServiceCollection AddMediatRConfigurations(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Application.Application));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            return services;
        }
    }
}
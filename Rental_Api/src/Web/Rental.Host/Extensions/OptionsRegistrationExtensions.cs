using System.Text;
using Rental.Domain.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Rental.Host.Extensions
{
    public static class OptionsRegistrationExtensions
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddOptions<ConnectionOptions>()
                    .Bind(Configuration.GetSection(ConnectionOptions.SECTION));
            services.AddOptions<LiveLimitOptions>()
                    .Bind(Configuration.GetSection(LiveLimitOptions.SECTION));
            services.AddOptions<AuthOptions>()
                .Bind(Configuration.GetSection(AuthOptions.SECTION));
            return services;
        }

        
    }
}

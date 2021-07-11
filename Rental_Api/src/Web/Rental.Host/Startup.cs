using Rental.Host.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rental.Host.Extensions;

namespace Rental.Host
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;

        }

        public void ConfigureServices(IServiceCollection services) =>
            services.AddServices()
                .AddOptions(Configuration)
                .AddAuth(Configuration)
                .AddInfrastructure(Configuration)
                .AddVersioningConfigurations()
                .AddHttpClientRegistration()
                .AddHealthChecksConfigurations(Configuration)
                .AddSwaggerConfigurations()
                .AddMediatRConfigurations();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app.UseMiddleware(env)
                .UseInfrastructure(env)
                .UseSwaggerConfigurations(env)
                .UseHealthCheckConfigurations()
                .UseCorsConfigurations()
                ;
    }
}

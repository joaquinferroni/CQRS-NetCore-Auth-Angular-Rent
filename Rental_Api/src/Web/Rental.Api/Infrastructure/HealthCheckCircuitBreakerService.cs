using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Rental.Api.Infrastructure
{
    public class HealthCheckCircuitBreakerService
    {
        private HealthCheckResult _state = HealthCheckResult.Healthy();

        public void Unhealthy() => _state = HealthCheckResult.Unhealthy();
        public void Degraded() => _state = HealthCheckResult.Degraded();
        public void Healthy() => _state = HealthCheckResult.Healthy();
        public HealthCheckResult Status() => _state;
    }
}

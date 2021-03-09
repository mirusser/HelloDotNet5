using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace HelloDotNet5
{
    public class ExternalEndpointHealthCheck : IHealthCheck
    {
        private readonly ServiceSettings settings;

        public ExternalEndpointHealthCheck(IOptions<ServiceSettings> options)
        {
            settings = options.Value;
        }


        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();

            var reply = await ping.SendPingAsync(settings.OpenWeatherHost);

            return reply.Status != IPStatus.Success ? 
                HealthCheckResult.Unhealthy() : HealthCheckResult.Healthy();
        }
    }
}

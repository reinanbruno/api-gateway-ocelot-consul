using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Models;
using Shared.Utils;
using System;

namespace Shared.Extensions
{
    public static class ConsulExtension
    {
        private static ServiceSettings serviceSettings { get; set; }

        public static IServiceCollection AddConsulExtension(this IServiceCollection services, IConfiguration configuration)
        {
            serviceSettings = configuration.GetSection("ServiceSettings").Get<ServiceSettings>();
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(serviceSettings.ServiceDiscoveryAddress);
            }));
            return services;
        }

        public static IApplicationBuilder UseConsulExtension(this IApplicationBuilder app, string environmentName)
        {
            IConsulClient consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            ILogger logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("ConsulExtension");
            IApplicationLifetime lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            //Changing the host when it is localhost configuring based on your local ip but if you prefer you can configure in
            //appsettings.Development.json modifying the "host" to your local ip and remove this line below,
            //find out which is your ip opening the cmd and run "ipconfig" and get ipv4
            if (environmentName == EnvironmentName.Development)
            {
               serviceSettings.Host = IpAddress.GetLocalIPAddress();
            }

            AgentCheckRegistration httpCheck = new AgentCheckRegistration()
            {
                HTTP = $"{serviceSettings.Schema}://{serviceSettings.Host}:{serviceSettings.Port}/health",
                Interval = TimeSpan.FromSeconds(10)
            };

            var registration = new AgentServiceRegistration()
            {
                ID = serviceSettings.Name,
                Name = serviceSettings.Name,
                Address = serviceSettings.Host,
                Port = serviceSettings.Port,
                Check = httpCheck
            };

            logger.LogInformation("Registrando serviço com Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
                {
                    logger.LogInformation("Parando serviço do Consul");
                });

            return app;
        }
    }
}

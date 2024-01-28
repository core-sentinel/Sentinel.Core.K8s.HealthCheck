using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sentinel.Core.K8s;
using Sentinel.PolicyValidator.ValidationReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Sentinel.PolicyValidator.BackgroundServices
{
    public static class ValidationBackgroundServiceFactory
    {

        public static void AddValidationBackgroundServices(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var client = sp.GetService<IKubernetesClient>();
            var aggregator = sp.GetService<ValidationRuleAggregator>();
            var hcoptions = sp.GetService<IOptions<HealthCheckServiceOptions>>();
            // new ValidationBackgroundServiceFactory(client, logger, aggregator, services, hcoptions);

            if (client == null) { throw new ArgumentNullException("IKubernetesClient not found in dependices"); }
            if (aggregator == null) { throw new ArgumentNullException("ValidationRuleAggregator not found in dependices"); }


            ILoggerFactory loggerFactory = sp.GetService<ILoggerFactory>();
            var bglogger = loggerFactory.CreateLogger<ValidationBackgroundService>();

            foreach (var validation in aggregator.MergedValidations)
            {
                var service = new ValidationBackgroundService(
                   client, services, validation.Value, bglogger, hcoptions);
                //   service.Start();

                services.AddSingleton<IHostedService>((sp) => service);
            }

        }


    }
}

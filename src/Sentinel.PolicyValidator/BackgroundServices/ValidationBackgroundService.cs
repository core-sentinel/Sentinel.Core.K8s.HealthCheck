using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sentinel.Core.BackgroundServices;
using Sentinel.Core.K8s;
using Sentinel.Core.K8s.Watchers;
using Sentinel.PolicyValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sentinel.PolicyValidator.BackgroundServices
{
    public class ValidationBackgroundService : BackgroundServiceWithHealthCheck
    {
        private readonly IKubernetesClient _client;
        private readonly ValidationModel _validationModel;
        private int number = 0;

        public ValidationBackgroundService(
            IKubernetesClient client,
            IServiceCollection services,
            ValidationModel validationModel,
             ILogger<ValidationBackgroundService> logger,
            IOptions<HealthCheckServiceOptions> hcoptions
            ) : base(logger, hcoptions, 5)
        {

            _client = client;
            _validationModel = validationModel;
        }

        protected override async Task Execute(CancellationToken stoppingToken)
        {
            var group = _validationModel.K8sResource.Group;
            var plural = _validationModel.K8sResource.Plural;
            var version = _validationModel.K8sResource.Version;
            _logger.LogInformation("ValidationBackgroundService init group: {group} plural: {plural}  version: {version}", group, plural, version);
            JTokenResourceWatcher watcher = new JTokenResourceWatcher(_client, _logger, group, plural, version);
            await watcher.Start();


            watcher.WatchEvents.Subscribe(
              (x) =>
              {
                  var name = x.Resource.SelectToken("$.metadata.name").ToString();
                  _logger.LogInformation(@"class: {class} ,Event: {Event}, Resource: {name}, order: {number} at {date}", this.GetType().Name, x.Event, name, number++.ToString(), DateTime.Now.ToString());
              }
          );
        }
    }
}

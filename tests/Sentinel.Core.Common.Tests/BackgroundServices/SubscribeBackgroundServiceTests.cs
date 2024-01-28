//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Diagnostics.HealthChecks;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Abstractions;
//using Microsoft.Extensions.Options;
//using Xunit;
//using FluentAssertions;
//using Xunit.Abstractions;
//using Sentinel.Core.BackgroundServices;

//namespace Sentinel.Common.Tests.BackgroundServices
//{
//    public class SubscribeBackgroundServiceTests
//    {

//        private BackgroundServiceWithHealthChecker _sut;
//        private readonly ITestOutputHelper _output;


//        public SubscribeBackgroundServiceTests(ITestOutputHelper output)
//        {

//            ILogger<BackgroundServiceWithHealthCheck> logger = new NullLogger<BackgroundServiceWithHealthCheck>();
//            _output = output;
//            HealthCheckServiceOptions opts = new HealthCheckServiceOptions();
//            var ops = Options.Create(opts);
//            _sut = new BackgroundServiceWithHealthChecker(logger, ops, 1);
//        }

//        [Fact]
//        public void ExecuteAsync_ShouldBeCreated()
//        {

//            Assert.NotNull(_sut);
//        }

//        [Fact]
//        public async Task ExecuteAsync_ShouldBe_ExecuteAsync()
//        {
//            await _sut.BaseExecuteAsync(new CancellationToken());
//            // sut.LastRestart.Should().Be(DateTime.MinValue);
//        }

//        [Fact]
//        public void ExecuteAsync_ShouldBeDisposed()
//        {
//            _sut.Dispose();
//        }




//        [Fact]
//        public void ExecuteAsync_Should_ReportUnhealthy()
//        {
//            var message = "message";
//            _sut.ReportUnhealthy(message);
//        }

//        [Fact]
//        public void ExecuteAsync_Should_ReportDegraded()
//        {
//            var message = "message";
//            _sut.ReportDegraded(message);
//        }

//        [Fact]
//        public void ExecuteAsync_Should_ReportHealthy()
//        {
//            var message = "message";
//            _sut.ReportUnhealthy(message);
//        }
//    }

//    internal class BackgroundServiceWithHealthChecker : BackgroundServiceWithHealthCheck
//    {
//        public BackgroundServiceWithHealthChecker(
//            ILogger<BackgroundServiceWithHealthCheck> logger,
//            IOptions<HealthCheckServiceOptions> hcoptions,
//            int HeathCheckTimeoutTotalMinutes = 5) : base(logger, hcoptions, HeathCheckTimeoutTotalMinutes)
//        {
//        }

//        protected override Task Execute(CancellationToken stoppingToken)
//        {
//            return Task.CompletedTask;
//        }

//        public Task BaseExecuteAsync(CancellationToken stoppingToken)
//        {
//            return base.ExecuteAsync(stoppingToken);
//        }


//        public void BaseHealthCheckFailIfQueueNotUsed(object? state)
//        {
//            base.HealthCheckFailIfQueueNotUsed(state);
//        }
//    }
//}
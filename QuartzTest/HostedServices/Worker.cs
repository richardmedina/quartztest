using Quartz;

namespace QuartzTest.HostedServices
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IHostApplicationLifetime appLifeTime;
        private readonly IScheduler scheduler;

        public Worker(ILogger<Worker> logger, IHostApplicationLifetime appLifeTime, IScheduler scheduler)
        {
            this.appLifeTime = appLifeTime;
            this.logger = logger;
            this.scheduler = scheduler;
        }
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            appLifeTime.ApplicationStarted.Register(OnStarted);
            appLifeTime.ApplicationStopping.Register(OnStopping);
            appLifeTime.ApplicationStopped.Register(OnStopped);

            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            logger.LogInformation("****** Stopping worker..");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.CompletedTask;
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(3000);
                logger.LogInformation("****** Executing Worker...");
            }

            logger.LogInformation("****** Terminating Worker...");
        }

        private void OnStarted()
        {
            logger.LogInformation("######################## OnStarting...");
        }

        private void OnStopping()
        {
            logger.LogInformation("######################## OnStopping...");
        }

        private void OnStopped()
        {
            logger.LogInformation("######################## OnStopped...");
            logger.LogInformation("Shutting down scheduler...");
            scheduler.Shutdown();
            logger.LogInformation("Shutting down scheduler: DONE");
        }

    }
}

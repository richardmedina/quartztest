using Quartz;

namespace QuartzTest.Jobs
{
    public class IocJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.CompletedTask;
            Console.WriteLine("Running IocJob...");
        }
    }
}

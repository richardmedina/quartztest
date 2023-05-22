using Quartz;
using QuartzTest.JobParameters;
using System.Diagnostics;

namespace QuartzTest.Jobs
{
    public class SimpleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.CompletedTask;

            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string username = dataMap.GetString("username") ?? "";
            string password = dataMap.GetString("password") ?? "";
            string other = dataMap.GetString("other") ?? "";
            SimpleJobParameter jobParameters = (SimpleJobParameter) dataMap.Get("simplejobparameters");

            string triggerParam = context.MergedJobDataMap.GetString("triggerparam") ?? "";
            string triggerParamUsername = context.MergedJobDataMap.GetString("username") ?? "";

            Console.WriteLine($"********** ********** Simple job executed at ${DateTime.Now.ToString()} ** " + 
                $"With data username='{username}', password='{password}', other='{other}'" +
                $"With SimpleJobParameters ={jobParameters}");
            
            Console.WriteLine("TriggerParameter: " + triggerParam);
            Console.WriteLine("triggerParamUsername: " + triggerParamUsername);
        }
    }
}

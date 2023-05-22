using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace QuartzTest.JobFactories
{
    public class IocJobFactory : SimpleJobFactory
    {

        IServiceProvider _provider;

        public IocJobFactory(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }

        override public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return (IJob) _provider.GetService(bundle.JobDetail.JobType);
            }
            catch (Exception e)
            {
                throw new SchedulerException($"Error intantianting job {bundle.JobDetail.Key} from IoC container", e);
            }
        }
    }
}

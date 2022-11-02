using Quartz;
using Quartz.Spi;

namespace TechnicalTest.API.Quartz;

public class MyJobFactory : IJobFactory
{
    private readonly IServiceProvider m_service;

    public MyJobFactory(IServiceProvider serviceProvider)
    {
        m_service = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
       var jobDetail = bundle.JobDetail;
       return (IJob)m_service.GetService(jobDetail.JobType);
    }

    public void ReturnJob(IJob job)
    {

    }
}
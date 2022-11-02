using Quartz;
using Quartz.Spi;

namespace TechnicalTest.API.Quartz;

public class MySchedular : IHostedService
{
    private IScheduler Scheduler { get; set; }

    private readonly IJobFactory m_jobFactory;
    private readonly JobMetaData m_jobMetaData;
    private readonly ISchedulerFactory m_schedulerFactory;


    public MySchedular(ISchedulerFactory schedulerFactory, JobMetaData jobMetaData, IJobFactory jobFactory)
    {
        this.m_schedulerFactory = schedulerFactory;
        this.m_jobMetaData = jobMetaData;
        this.m_jobFactory = jobFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Create a new scheduler
        Scheduler = await m_schedulerFactory.GetScheduler();
        Scheduler.JobFactory = m_jobFactory;

        // Create a new job
        IJobDetail jobDetail = CreateJob(m_jobMetaData);

        // Create a trigger that will fire the job every 5 seconds
        ITrigger trigger = CreateTrigger(m_jobMetaData);

        //Schedule the job using the job and trigger
        await Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);

        // Start the scheduler
        await Scheduler.Start(cancellationToken);
    }

    private ITrigger CreateTrigger(JobMetaData jobMetaData1)
    {
        return TriggerBuilder.Create()
            .WithIdentity(m_jobMetaData.JobId.ToString()).WithCronSchedule(m_jobMetaData.CronExpression)
            .WithDescription(m_jobMetaData.JobName)
            .Build();
    }

    private IJobDetail CreateJob(JobMetaData jobMetaData)
    {

        return JobBuilder.Create(jobMetaData.JobType)
            .WithIdentity(jobMetaData.JobId.ToString()).WithDescription(jobMetaData.JobName)
            .Build();

    }


    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Scheduler.Shutdown(cancellationToken);
    }


}
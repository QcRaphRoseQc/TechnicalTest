namespace TechnicalTest.API.Quartz;

public class JobMetaData
{
    public Guid JobId { get; set; }

    public Type JobType { get; set; }

    public string JobName { get; set; }

    public string CronExpression { get; set; }

    public JobMetaData(Guid id, Type jobType , string jobName, string cronExpression)
    {
        JobId = id;
        JobType = jobType;
        JobName = jobName;
        CronExpression = cronExpression;
    }
}
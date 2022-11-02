using Microsoft.AspNetCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Serilog;
using Serilog.Events;
using TechnicalTest.API;
using TechnicalTest.API.Data;
using TechnicalTest.API.Quartz;
using TechnicalTest.API.TechnicalTest.API;


String cronExpression = "0 0 2 1/1 * ? *";
String cronExpression10sec = "0/10 * * 1/1 * ? *";
//every 10 seconds = "0/10 * * 1/1 * ? *"
//String cronExpression = "0/10 * * 1/1 * ? *";
String jobName = "EmailJob";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
try
{
    Log.Information("Starting web host");
    var host = CreateHostBuilder(args).Build();
    host.MigrateDbContext<EventDbContext>((_, __) => { });
    host.Run();
    return 0;
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureServices(services =>
        {
            services.AddSingleton<IJobFactory, MyJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<EmailJob>();
            services.AddSingleton(new JobMetaData(Guid.NewGuid(),typeof(EmailJob), jobName, cronExpression));
            services.AddHostedService<MySchedular>();
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
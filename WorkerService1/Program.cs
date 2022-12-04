using MyTestWokerService;
using WorkerService1;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        //注入服务类
        services.AddSingleton<WriteLogFile>();
        services.AddSingleton<Log4NetUtil>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

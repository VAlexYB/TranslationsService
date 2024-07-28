using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TranslationService.Console;
using TranslationService.Interfaces;
using TranslationService.Services;

var host = CreateHostBuilder(args).Build();
await host.Services.GetRequiredService<App>().RunAsync();


static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {

        services.AddSingleton<App>();
        services.AddHttpClient<StubTranslateClient>();


        services.AddSingleton<ITranslateClient, StubTranslateClient>();
        services.AddSingleton<ICacheProvider>(provider =>
        {
            var redisConnectionString = context.Configuration.GetValue<string>("Redis:ConnectionString");
            return new RedisCacheProvider(redisConnectionString);
        });
        services.AddSingleton<ITranslationService, TranslationService.Services.TranslationService>();
    });
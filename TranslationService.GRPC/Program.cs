using TranslationService.GRPC.Services;
using TranslationService.Core.Interfaces;
using TranslationService.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddGrpc();

services.AddSingleton<ITranslateClient, StubTranslateClient>();
services.AddSingleton<ICacheProvider>(provider =>
{
    var redisConnectionString = builder.Configuration.GetValue<string>("Redis:ConnectionString");
    return new RedisCacheProvider(redisConnectionString);
});
services.AddSingleton<ITranslationService, TranslationService.Services.TranslationService>();

var app = builder.Build();

app.MapGrpcService<TranslationServiceImpl>();

app.Run();

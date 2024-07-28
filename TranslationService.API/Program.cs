using TranslationService.Interfaces;
using TranslationService.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddSingleton<ITranslateClient, StubTranslateClient>();
services.AddSingleton<ICacheProvider>(provider =>
{
    var redisConnectionString = builder.Configuration.GetValue<string>("Redis:ConnectionString");
    return new RedisCacheProvider(redisConnectionString);
});
services.AddSingleton<ITranslationService, TranslationService.Services.TranslationService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/translate", async (TranslationRequest request, ITranslationService translationService) =>
{
    var translations = await translationService.TranslateAsync(request.Texts, request.FromLanguage, request.ToLanguage);
    return Results.Ok(translations);
});

app.MapGet("/getServiceInfo", async (ITranslationService translationService) =>
{
    var serviceInfo = await translationService.GetServiceInfoAsync();
    return Results.Ok(serviceInfo);
});

app.Run();


public record TranslationRequest(string[] Texts, string FromLanguage, string ToLanguage);
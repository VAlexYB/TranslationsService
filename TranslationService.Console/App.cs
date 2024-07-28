 using System;
using TranslationService.Interfaces;
namespace TranslationService.Console
{
    public class App
    {
        private readonly ITranslationService _translationService;

        public App(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        public async Task RunAsync()
        {
            var serviceInfo = await _translationService.GetServiceInfoAsync();
            await System.Console.Out.WriteLineAsync($"Использовано API : {serviceInfo.ExternalService}\n" +
                    $"Кэшироваине проиcходит через {serviceInfo.CacheType}\nОбъем кэша: {serviceInfo.CacheVolume}");
            while (true)
            {
                await System.Console.Out.WriteLineAsync("Введите текст для перевода (или 'exit' для выхода):");
                var input = await System.Console.In.ReadLineAsync();

                if (input?.ToLower() == "exit") break;

                await System.Console.Out.WriteLineAsync("Введите исходный язык (например, 'en' для английского):");
                var fromLanguage = await System.Console.In.ReadLineAsync();

                await System.Console.Out.WriteLineAsync("Введите целевой язык (например, 'ru' для русского):");
                var toLanguage = await System.Console.In.ReadLineAsync();

                var words = input.Split(new[] { ' ', ',', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                var translations = await _translationService.TranslateAsync(words, fromLanguage, toLanguage);

                await System.Console.Out.WriteLineAsync("Перевод:");
                for(int i = 0; i < words.Length; i++)
                {
                    await System.Console.Out.WriteLineAsync($"{words[i]}->{translations[i]}");
                }
                await System.Console.Out.WriteLineAsync();
            }
        }
    }
}

namespace TranslationService.Client.Http
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var httpClient = new HttpClient{ BaseAddress = new Uri("https://localhost:7241/") };
            var translationClient = new TranslationHttpClient(httpClient);

            var texts = new[] { "Hello", "World" };
            var fromLanguage = "en";
            var toLanguage = "ru";

            try
            {
                var translateResponse = await translationClient.TranslateAsync(texts, fromLanguage, toLanguage);
                foreach (var translation in translateResponse)
                {
                    Console.WriteLine(translation);
                }

                var serviceInfoResponse = await translationClient.GetServiceInfoAsync();

                await Console.Out.WriteLineAsync($"Использовано API : {serviceInfoResponse.ExternalService}\n" +
                    $"Кэшироваине проиcходит через {serviceInfoResponse.CacheType}\nОбъем кэша: {serviceInfoResponse.CacheVolume}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error calling Http service: {ex.Message}");
            }
        }
    }
}
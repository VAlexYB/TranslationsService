using Grpc.Core;
using Grpc.Net.Client;
using GRPCClient;


namespace TranslationService.Client.gRPC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var translationClient = new TranslationGRPCClient("https://localhost:7263");

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
            catch (RpcException ex)
            {
                Console.WriteLine($"Error calling gRPC service: {ex.Status.Detail}");
            }
        }
    }
}

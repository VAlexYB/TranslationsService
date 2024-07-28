using Grpc.Core;
using Grpc.Net.Client;
using GRPCClient;


namespace TranslationService.Client.gRPC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7263");
            var client = new Translation.TranslationClient(channel);

            var request = new TranslateRequest
            {
                FromLanguage = "en",
                ToLanguage = "ru"
            };
            request.Texts.Add("Hello");
            request.Texts.Add("World");

            try
            {
                var translateResponse = await client.TranslateAsync(request);
                foreach (var translation in translateResponse.Translations)
                {
                    Console.WriteLine(translation);
                }

                var serviceInfoResponse = await client.GetServiceInfoAsync(new Google.Protobuf.WellKnownTypes.Empty());

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

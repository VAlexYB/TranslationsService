using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using TranslationService.Core;
using TranslationService.Core.Interfaces;

namespace GRPCClient
{
    public class TranslationGRPCClient : ITranslationService
    {
        private readonly Translation.TranslationClient _client;

        public TranslationGRPCClient(string grpcServerUrl)
        {
            var channel = GrpcChannel.ForAddress(grpcServerUrl);
            _client = new Translation.TranslationClient(channel);
        }
        public async Task<ServiceInfo> GetServiceInfoAsync()
        {
            var response = await _client.GetServiceInfoAsync(new Empty());
            return new ServiceInfo
            {
                ExternalService = response.ExternalService,
                CacheType = response.CacheType,
                CacheVolume = response.CacheVolume
            };
        }

        public async Task<string[]> TranslateAsync(string[] texts, string fromLanguage, string toLanguage)
        {
            var request = new TranslateRequest
            {
                FromLanguage = fromLanguage,
                ToLanguage = toLanguage
            };
            request.Texts.AddRange(texts);

            var response = await _client.TranslateAsync(request);
            return response.Translations.ToArray();
        }
    }
}

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TranslationService.Core.Interfaces;
using static TranslationService.GRPC.Translation;

namespace TranslationService.GRPC.Services
{
    public class TranslationServiceImpl : TranslationBase
    {
        private readonly ITranslationService _translationService;
        
        public TranslationServiceImpl(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        public override async Task<TranslateResponse> Translate(TranslateRequest request, ServerCallContext context)
        {
            var translations = await _translationService.TranslateAsync(request.Texts.ToArray(), request.FromLanguage, request.ToLanguage);
            var response = new TranslateResponse();
            response.Translations.AddRange(translations);
            return response;
        }

        public override async Task<ServiceInfoResponse> GetServiceInfo(Empty request, ServerCallContext context)
        {
            var serviceInfo = await _translationService.GetServiceInfoAsync();
            var response = new ServiceInfoResponse
            {
                ExternalService = serviceInfo.ExternalService,
                CacheType = serviceInfo.CacheType,
                CacheVolume = serviceInfo.CacheVolume
            };
            return response;
        }
    }
}

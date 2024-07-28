using TranslationService.Interfaces;

namespace TranslationService.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly ITranslateClient _googleTranslateClient;
        private readonly ICacheProvider _cacheProvider;

        public TranslationService(ITranslateClient googleTranslateClient, ICacheProvider cacheProvider)
        {
            _googleTranslateClient = googleTranslateClient;
            _cacheProvider = cacheProvider;
        }

        public async Task<ServiceInfo> GetServiceInfoAsync()
        {
            var cacheVolume = await _cacheProvider.GetCacheVolumeAsync();
            var serviceInfo = new ServiceInfo
            {
                ExternalService = "Stub Translate Api",
                CacheType = "Redis",
                CacheVolume = $"{cacheVolume} symbols"
            };
            return serviceInfo;
        }

        public async Task<string[]> TranslateAsync(string[] texts, string fromLanguage, string toLanguage)
        {
            var translations = new string[texts.Length];

            for(int i = 0; i < texts.Length; i++)
            {
                var cacheKey = $"{fromLanguage}->{toLanguage}-{texts[i]}";
                var cashedTranslation = await _cacheProvider.GetCachedTranslationAsync(cacheKey);

                if (!string.IsNullOrEmpty(cashedTranslation))
                {
                    translations[i] = cashedTranslation;
                }
                else
                {
                    var translation = await _googleTranslateClient.TranslateOneAsync(texts[i], fromLanguage, toLanguage);
                    translations[i] = translation;
                    await _cacheProvider.SetCachedTranslationAsync(cacheKey, translation);
                }
            }

            return translations;
        }
    }
}

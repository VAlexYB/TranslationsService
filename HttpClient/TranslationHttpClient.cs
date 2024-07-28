using System.Net.Http.Json;

namespace TranslationService.Client.Http
{
    public class TranslationHttpClient
    {
        private readonly HttpClient _httpClient;

        public TranslationHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string[]> TranslateAsync(string[] texts, string fromLanguage, string toLanguage)
        {
            var request = new TranslationRequest(texts, fromLanguage, toLanguage);
            var response = await _httpClient.PostAsJsonAsync("/translate", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<string[]>();
        }

        public async Task<ServiceInfoResponse> GetServiceInfoAsync()
        {
            var response = await _httpClient.GetAsync("/getServiceInfo");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ServiceInfoResponse>();
        }
    }

    public record TranslationRequest(string[] Texts, string FromLanguage, string ToLanguage);
    public record ServiceInfoResponse(string ExternalService, string CacheType, string CacheVolume);
}

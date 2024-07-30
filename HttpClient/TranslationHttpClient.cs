using System.Net.Http.Json;
using TranslationService.Core;
using TranslationService.Core.Interfaces;

namespace TranslationService.Client.Http
{
    public class TranslationHttpClient : ITranslationService
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

        public async Task<ServiceInfo> GetServiceInfoAsync()
        {
            var response = await _httpClient.GetAsync("/getServiceInfo");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ServiceInfo>();
        }
    }

    public record TranslationRequest(string[] Texts, string FromLanguage, string ToLanguage);
}

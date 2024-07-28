using Newtonsoft.Json.Linq;
using TranslationService.Interfaces;

namespace TranslationService.Services
{
    public class StubTranslateClient : ITranslateClient
    {
        public async Task<string> TranslateOneAsync(string text, string fromLanguage, string toLanguage)
        {
            var translationStub = $"{text}-{toLanguage}";
            return translationStub;
        }
    }
}

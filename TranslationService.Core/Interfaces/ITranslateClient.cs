namespace TranslationService.Core.Interfaces
{
    public interface ITranslateClient
    {
        public Task<string> TranslateOneAsync(string text, string fromLanguage, string toLanguage);
    }
}

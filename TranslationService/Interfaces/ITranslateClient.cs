namespace TranslationService.Interfaces
{
    public interface ITranslateClient
    {
        public Task<string> TranslateOneAsync(string text, string fromLanguage, string toLanguage);
    }
}

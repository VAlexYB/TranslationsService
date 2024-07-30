namespace TranslationService.Core.Interfaces
{
    public interface ITranslationService
    {
        Task<string[]> TranslateAsync(string[] texts, string fromLanguage, string toLanguage);
        Task<ServiceInfo> GetServiceInfoAsync();
    }
}

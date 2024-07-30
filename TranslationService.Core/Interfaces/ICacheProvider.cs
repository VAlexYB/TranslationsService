namespace TranslationService.Core.Interfaces
{
    public interface ICacheProvider
    {
        Task<string> GetCachedTranslationAsync(string key);
        Task SetCachedTranslationAsync(string key, string translation);
        Task<long> GetCacheVolumeAsync();
    }
}

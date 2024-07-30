using StackExchange.Redis;
using System.Data;
using TranslationService.Core.Interfaces;


namespace TranslationService.Services
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly IDatabase _database;
        private const string CacheVolumeKey = "cache_volume";

        public RedisCacheProvider(string connectionString) 
        {
            var redis = ConnectionMultiplexer.Connect(connectionString);
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCachedTranslationAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task SetCachedTranslationAsync(string key, string translation)
        {
            var prevValue = await _database.StringGetAsync(key);
            if (prevValue.HasValue)
            {
                await UpdateCacheVolumeAsync(prevValue.ToString().Length, -1);               
            }
            await _database.StringSetAsync(key, translation);
            await UpdateCacheVolumeAsync(translation.ToString().Length, 1);
        }

        public async Task<long> GetCacheVolumeAsync()
        {
            var cacheVolume = await _database.StringGetAsync(CacheVolumeKey);

            if(long.TryParse(cacheVolume, out var volume)) 
            {
                return volume;
            }
            return 0;   
        }

        private async Task UpdateCacheVolumeAsync(long length, int multiplier)
        {
            await _database.StringIncrementAsync(CacheVolumeKey, length * multiplier);
        }
    }
}

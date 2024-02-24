using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SV_CodingCase.Domain.Models;
using SV_CodingCase.Domain.Models.Configuration;
using System.Text.Json;

namespace SV_CodingCase.Domain.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly ApplicationOptions _options;

        public DataRepository(HttpClient httpClient, IMemoryCache memoryCache, IOptions<ApplicationOptions> options)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
            _options = options.Value;
        }

        public async Task<DataFile> GetData()
        {
            var action = async () =>
                {
                    var response = await _httpClient.GetAsync(_options.DataSource);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<DataFile>(responseBody);
                };
            var result = await CheckCacheAsync("data", action);

            return result;
        }
        private async Task<T> CheckCacheAsync<T>(string key, Func<Task<T>> action) where T : class
        {
            if (_memoryCache.TryGetValue(key, out object cachedValue))
            {
                if (cachedValue is T cachedResult)
                {
                    return cachedResult;
                }
            }

            T result = await action();
            _memoryCache.Set(key, result, DateTimeOffset.Now.AddSeconds(_options.CachingExpirationTimeInSeconds));
            return result;
        }
    }
}

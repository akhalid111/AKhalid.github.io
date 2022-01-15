using ChuckNorris.Libraries.Cache;
using ChuckNorris.Libraries.Contract;
using ChuckNorris.Libraries.Proxy;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ChukNorris.Client
{
    public class JokesManager
    {
        ChuckNorrisProxy _chuckNorrisProxy;
        String _randomJokesRoute = "jokes/random";
        CacheManager _cacheManager;
        public JokesManager()
        {
            var baseURL = ConfigurationManager.AppSettings["API_URL"] ?? String.Empty;
            if (string.IsNullOrWhiteSpace(baseURL))
                return;
            _chuckNorrisProxy = new ChuckNorrisProxy(new Uri(baseURL));
            _cacheManager = CacheManager.Instance();
        }
        public async Task<String> DisplayJokes(String keyValue)
        {
            if (String.IsNullOrWhiteSpace(keyValue))
                return String.Empty;

            switch (keyValue.ToLowerInvariant())
            {
                case "j": return await GetJokeFromAPI();
                case "p": return GetPreviousJoke();
                case "n": return GetNextJoke();
                default: return "Please enter a valid option j , p, n or exit ";
            }
        }
        private async Task<String> GetJokeFromAPI()
        {
            try
            {
                var result = await _chuckNorrisProxy.GetFromAPI<Joke>(_randomJokesRoute, null);

                if (result != null && !String.IsNullOrWhiteSpace(result.Value))
                    _cacheManager.AddObjectToCache(result);
                return result.Value;
            }
            catch (Exception ex)
            {
                //log exception 
                //display proper message 
                return $"Error Occured : detail  {ex.Message}";
            }

        }
        private string GetNextJoke()
        {
            var nextJoke = _cacheManager.GetNextObject<Joke>();
            if (nextJoke == null)
                return "No Joke Found or Reached end of the list ";
            return nextJoke.Value;

        }
        private string GetPreviousJoke()
        {
            var previousJoke = _cacheManager.GetPreviousObject<Joke>();
            if (previousJoke == null)
                return "No Joke Found or Reached start of the list ";
            return previousJoke.Value;

        }

    }
}
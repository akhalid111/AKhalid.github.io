using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorris.Libraries.Proxy
{
    public class ChuckNorrisProxy : ApiClientBase
    {
        public ChuckNorrisProxy(Uri baseAddress) : base(baseAddress)
        {
        }

        /// <summary>
        /// Initialise HttpClient object with Url, authentication header and any further request headers.
        /// Use with API calls accepting HttpClient method parameter allowing HttpClient reuse
        /// </summary>
        /// <param name="client"></param>
        /// <param name="requestHeaders"></param>
        /// <returns></returns>
        public async Task InitHttpClientAsyncForChuckNorris(HttpClient client, List<KeyValuePair<String, String>> requestHeaders = null)
        {
             InitHttpClient(client, requestHeaders);
        }

        public void InitHttpClient(HttpClient client, List<KeyValuePair<String, String>> requestHeaders = null)
        {
            base.InitHttpClient(client, requestHeaders);

        }

        /// <summary>
        /// GET request to API creating HttpClient instance per call
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="route"></param>
        /// <param name="requestHeaders"></param>
        /// <returns></returns>
        public async Task<T> GetFromAPI<T>(String route, List<KeyValuePair<String, String>> requestHeaders = null) where T : class
        {
            try
            {
                using (var client = new HttpClient())
                {
                    await InitHttpClientAsyncForChuckNorris(client, requestHeaders);

                    return await GetFromAPI<T>(client, route);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="route"></param>
        /// <param name="requestHeaders"></param>
        /// <returns></returns>
        public async Task<String> GetFromAPI(String route, List<KeyValuePair<String, String>> requestHeaders = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    await InitHttpClientAsyncForChuckNorris(client, requestHeaders);

                    return await GetFromAPI(client, route);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

    }
}

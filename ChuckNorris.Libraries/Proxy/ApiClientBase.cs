using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorris.Libraries.Proxy
{
    //Only Implement Get at the moment
    //TO DO- PUT/POST/DELETE
    public class ApiClientBase : IDisposable
    {
        public Uri BaseUrl { get; set; }

        public ApiClientBase(Uri baseAddress)
        {
            BaseUrl = AppendTrailingBackslashIfRequired(baseAddress);
        }

        public ApiClientBase()
        {
        }

        static Uri AppendTrailingBackslashIfRequired(Uri uri)
        {
            if (uri != null)
            {
                return uri.ToString().EndsWith("/") ? uri : new Uri($"{uri}/");
            }
            else
                return uri;
        }

        /// <summary>
        /// Initialise HttpClient object with Url and any further request headers.
        /// Use with API calls accepting HttpClient method parameter allowing HttpClient reuse
        /// </summary>
        /// <param name="client"></param>
        /// <param name="requestHeaders"></param>
        /// <returns></returns>
        public virtual async Task InitHttpClientAsync(HttpClient client, List<KeyValuePair<String, String>> requestHeaders = null)
        {
            InitHttpClient(client, requestHeaders);
        }

        public virtual void InitHttpClient(HttpClient client, List<KeyValuePair<String, String>> requestHeaders = null)
        {
            client.BaseAddress = BaseUrl;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            if (requestHeaders != null)
            {
                foreach (var kvp in requestHeaders)
                {
                    client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                }
            }
        }



        /// <summary>
        /// GET request to API allowing reuse of HttpClient object.  Initialisation of HttpClient can be performed by calling InitHttpClient
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public async Task<T> GetFromAPI<T>(HttpClient httpClient, String route) where T : class
        {
            HttpResponseMessage response = await httpClient.GetAsync(route).ConfigureAwait(false);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var parsedResult = JsonConvert.DeserializeObject<T>(result);
                    return parsedResult as T;
                }
                else
                    return default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public async Task<String> GetFromAPI(HttpClient httpClient, String route)
        {
            HttpResponseMessage response = await httpClient.GetAsync(route).ConfigureAwait(false);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                    return String.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private HttpContent Serialize(object data) => new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        public void Dispose()
        {
            BaseUrl = null;
        }
    }
}

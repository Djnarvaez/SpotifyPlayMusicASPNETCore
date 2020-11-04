using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebPlayMusic.Repository.IRepository;

namespace WebPlayMusic.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory httpClientFactory;

        public Repository(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<T> GetMusicAsync(string url, string value, string type)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + $"{value}/{type}");

            var httpClient = httpClientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            return null;
        }
    }
}

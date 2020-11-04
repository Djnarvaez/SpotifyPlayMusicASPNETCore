using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PlayMusic.Models.DTO;
using PlayMusic.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static PlayMusic.Models.DTO.SpotifyModelsDTO;

namespace PlayMusic.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public Repository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<T> SearchArtist(string url, string value)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url + value);
            string accessToken = await GetTokeAsync();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var httpClient = httpClientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            return null;
        }

        public async Task<string> GetTokeAsync()
        {

            byte[] bytes = Encoding.GetEncoding("iso-8859-1")
                                   .GetBytes($"{configuration["Spotify:ClientID"]}:{configuration["Spotify:ClientSecret"]}");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, configuration["Spotify:APIAuthorizeUrl"]);

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials"
            };

            request.Content = new FormUrlEncodedContent(form);

            var httpClient = httpClientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                SpotifyTokenDTO spotifyTokenDTO = JsonConvert.DeserializeObject<SpotifyTokenDTO>(content);
                return spotifyTokenDTO.access_token;
            }

            return null;
        }
    }
}

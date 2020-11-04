using Microsoft.Extensions.Configuration;
using PlayMusic.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static PlayMusic.Models.DTO.SpotifyModelsDTO;

namespace PlayMusic.Repository
{
    public class SpotifyResult:Repository<Result>, ISpotifyResult
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public SpotifyResult(IHttpClientFactory httpClientFactory, IConfiguration configuration):base(httpClientFactory, configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }
    }
}

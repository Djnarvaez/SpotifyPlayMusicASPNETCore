using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebPlayMusic.Models;
using WebPlayMusic.Repository.IRepository;

namespace WebPlayMusic.Repository
{
    public class SpotifyResultRepository:Repository<SpotifyResult>, ISpotifyResultRepository
    {
        private readonly IHttpClientFactory httpClientFactory;

        public SpotifyResultRepository(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
    }
}

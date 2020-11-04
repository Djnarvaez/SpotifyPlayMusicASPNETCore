using PlayMusic.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PlayMusic.Models.DTO.SpotifyModelsDTO;

namespace PlayMusic.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> SearchArtist(string url, string value);
        Task<string> GetTokeAsync();
    }
}

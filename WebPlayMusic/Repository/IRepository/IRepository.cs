using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPlayMusic.Repository.IRepository
{
    public interface IRepository<T> where T :class
    {
        Task<T> GetMusicAsync(string url, string value, string type);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PlayMusic.Context;
using PlayMusic.Models.DTO;
using PlayMusic.Repository;
using PlayMusic.Repository.IRepository;
using static PlayMusic.Models.DTO.SpotifyModelsDTO;
using Models = PlayMusic.Models;

namespace PlayMusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayController : ControllerBase
    {
        private readonly ISpotifyResult spotifyResult;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;

        public PlayController
        (
            ISpotifyResult spotifyResult,
            IMapper mapper,
            IConfiguration configuration,
            ApplicationDbContext context
        )

        {
            this.spotifyResult = spotifyResult;
            this.mapper = mapper;
            this.configuration = configuration;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {

            //string barJobId = BackgroundJob.Enqueue(() => Init());
            //barJobId = BackgroundJob.ContinueJobWith(barJobId, () => Job1());
            //barJobId = BackgroundJob.ContinueJobWith(barJobId, () => Job2());

            var playLists = context.PlayLists.ToList();
            playLists.ForEach(x =>
            {
                x.Album = context.album.FirstOrDefault(y => y.PlaylistsId.Equals(x.Id));
                x.Album.Images = context.Images.Where(x => x.AlbumId == x.AlbumId).ToList();
                x.Album.Artists = context.Artists.Where(x => x.AlbumId == x.AlbumId).ToList();

            });

            return Ok(playLists);
        }
        [HttpGet("{value}/{type}")]
        public async Task<Result> SearchArtist(string value, string type)
        {
            return await spotifyResult.SearchArtist(configuration["Spotify:APIBaseUrl"], $"?q={value}&type={type}");
        }

        [HttpPost]
        public IActionResult CreatePlayList([FromBody] Item item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var playlist = new Models.PlayLists { PreviewUrl = item.preview_url, Status = "EnCola" };
            context.PlayLists.Add(playlist);
            context.SaveChanges();

            var album = new Models.Album { Name = item.album.name, PlaylistsId = playlist.Id };
            context.album.Add(album);
            context.SaveChanges();

            foreach (var artist in item.album.artists)
            {
                context.Artists.Add(new Models.Artists { Name = artist.name, AlbumId = album.Id });
            }


            foreach (var image in item.album.images)
            {
                context.Images.Add(new Models.Images { Width = image.width, Height = image.height, Url = image.url, AlbumId = album.Id });
            }
            context.SaveChanges();

            return Ok(new { message = "La inserción se realizo de forma correcta..." });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayMusic.Models.DTO
{
    public class SpotifyModelsDTO
    {
        public class ExternalUrls
        {
            public string spotify { get; set; }
        }

        public class Images
        {
            public int height { get; set; }
            public string url { get; set; }
            public int width { get; set; }
        }

        public class Album
        {
            public string name { get; set; }
            public List<Images> images { get; set; }
            public List<Artists> artists { get; set; }
        }
        public class Artists
        {
            public string name { get; set; }
        }
        public class Item
        {
            public ExternalUrls external_urls { get; set; }
            public string name { get; set; }
            public string preview_url { get; set; }
            public Album album { get; set; }
        }

        public class Tracks
        {
            public string href { get; set; }
            public List<Item> items { get; set; }
        }

        public class Result
        {
            public Tracks tracks { get; set; }
        }
    }
}

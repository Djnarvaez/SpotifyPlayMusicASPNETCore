using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPlayMusic.Models
{
    public class SpotifyResult
    {
        public class ExternalUrls
        {
            public string spotify { get; set; }
        }
        public class ImageSP
        {
            public int width { get; set; }
            public string url { get; set; }
            public int height { get; set; }
        }

        public class Item
        {
            public ExternalUrls external_urls { get; set; }
            public List<string> genres { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public List<ImageSP> images { get; set; }
            public string name { get; set; }
            public int popularity { get; set; }
            public string type { get; set; }
            public string uri { get; set; }
        }

        public class Artists
        {
            public string href { get; set; }
            public List<Item> items { get; set; }
        }

        public class Result
        {
            public Artists artists { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayMusic.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Artists> Artists { get; set; }
        public virtual List<Images> Images { get; set; }
        public int PlaylistsId { get; set; }
    }
}

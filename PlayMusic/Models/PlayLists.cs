using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayMusic.Models
{
    public class PlayLists
    {
        [Key]
        public int Id { get; set; }
        public string PreviewUrl { get; set; }
        public string Status { get; set; }
        public virtual Album Album { get; set; }
    }
}

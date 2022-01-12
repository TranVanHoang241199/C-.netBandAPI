using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Models
{
    public class BandForCreatingDto
    {
        public String Name { get; set; }
        public DateTime Founded { get; set; }
        public String MainGenre { get; set; }
        public ICollection<AlbumForCreatingDto> albums { get; set; } = new List<AlbumForCreatingDto>();
    }
}

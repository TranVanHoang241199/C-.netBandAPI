using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Models
{
    public class AlbumsDto
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public Guid BandId { get; set; }
    }
}

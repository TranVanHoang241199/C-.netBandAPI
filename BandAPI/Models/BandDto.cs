using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Models
{
    public class BandDto
    {
        public Guid Id { get; set; }
        public String Name { get; set; }

        public String FoundedYearsAgo { get; set; }
        public String MainGenre { get; set; }
    }
}

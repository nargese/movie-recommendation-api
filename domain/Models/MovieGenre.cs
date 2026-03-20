using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models
{
    public class MovieGenre
    {
        public Guid IdMovie { get; set; }
        public Movie Movie { get; set; }

        public Guid IdGenre { get; set; }
        public Genre Genre { get; set; }
    }
}

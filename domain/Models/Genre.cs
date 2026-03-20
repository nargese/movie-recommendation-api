using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models
{
    public class Genre
    {
        public Guid IdGenre { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<MovieGenre> MovieGenres { get; set; } 
    }

}

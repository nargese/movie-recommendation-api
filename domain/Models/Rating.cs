using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models
{
    public class Rating
    {
        public Guid IdRating { get; set; }
        public Guid FK_Compte { get; set; }
        public Compte? Compte { get; set; } = null!;

        public Guid FK_Movie { get; set; }
        public Movie? Movie { get; set; } = null!;

        public int RatingValue { get; set; } // entre 1 et 5
        public DateTime CreatedAt { get; set; }
    }

}

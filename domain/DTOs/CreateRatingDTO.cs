using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.DTOs
{
    public class CreateRatingDTO
    {
        public Guid IdRating { get; set; }
        public Guid FK_Compte { get; set; }
        public Guid FK_Movie { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

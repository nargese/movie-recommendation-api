using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.DTOs
{
    public class RatingDTO
    {
        public Guid IdRating { get; set; }
        public Guid FK_Compte { get; set; }
        public string Nom { get; set; } = null!;
        public Guid FK_Movie { get; set; }
        public string Title { get; set; } = null!;
        public int RatingValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.DTOs
{
    public class CreateMovieDTO
    {
        public Guid IdMovie { get; set; }   // Local DB key (always)
        public int? MovieApiId { get; set; }  // TMDB id if from API, null if user-added

        // Common fields
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string PosterPath { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;

        // User-created fields (null when from TMDB)
        public string? Description { get; set; }
        public int? ReleaseYear { get; set; }
        public string? PosterUrl { get; set; }
        public float AverageRating { get; set; }

        // Meta
        public DateTime CreatedAt { get; set; }

        // User info (only for user-created movies)
        public Guid? FK_Compte { get; set; }
        // ✅ Only IDs here
        public List<Guid> GenreIds { get; set; }
    }
}

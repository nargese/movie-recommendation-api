using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models
{
    public class Movie
    {
        // Local DB Primary Key
        public Guid IdMovie { get; set; }

        // External API reference (null if user-created)
        public int? MovieApiId { get; set; }

        // Common fields (exist in both cases)
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string PosterPath { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;

        // User-created movie extra fields
        public string? Description { get; set; }
        public int? ReleaseYear { get; set; }
        public string? PosterUrl { get; set; }
        public float AverageRating { get; set; } = 0;

        // Technical metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // User relation (only applies for user-added movies)
        public Guid? FK_Compte { get; set; }
        public virtual Compte? Compte { get; set; }
        public string? Nom { get; set; } // example: map from Compte.Username

        // Relations (both sources can have them, but more useful for user movies)
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<MovieGenre> MovieGenres { get; set; }

    }


}

using domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.DTOs
{
    public class LikeDTO
    {
        public Guid IdLike { get; set; }
        public Guid FK_Compte { get; set; }
        public string? Nom { get; set; }
        public Guid FK_Movie { get; set; }
        public string? Title { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

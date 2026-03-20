using domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace domain.Models
{
    public class Compte
    {
        [Key]
        public Guid IdCompte { get; set; }

        [Required]
        [StringLength(8)]
        public string CIN { get; set; }

        [Required]
        public string Nom { get; set; }

        public string Email { get; set; }

        [Required]
        public string Motdepasse { get; set; }

        public bool access { get; set; }

        // Relation avec Role
        public Guid FK_Role { get; set; }
        public virtual Role? Role { get; set; }
        public string RoleName { get; set; }

        // Relation One-to-Many avec Movie
        public virtual ICollection<Movie> Movies { get; set; } 

        // Relations sociales
        public virtual ICollection<Rating> Ratings { get; set; } 
        public virtual ICollection<Like> Likes { get; set; } 
        public virtual ICollection<Comment> Comments { get; set; } 
    
    }
}
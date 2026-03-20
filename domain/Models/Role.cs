using domain.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models
{
    public class Role
    {
        [Key]
        public Guid IdRole { get; set; }
        public String RoleName { get; set; }
        public virtual ICollection<Compte> Comptes { get; set; }

    }
}

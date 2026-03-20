using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.DTOs
{
    public class CompteDTO
    {
        [Required]
        public Guid IdCompte { get; set; }

        [Required]
        [StringLength(8)]
        public string CIN { get; set; }

        public string Nom { get; set; }

        public string Email { get; set; }

        public string Motdepasse { get; set; }

        public bool access { get; set; }

        public Guid FK_Role { get; set; }
        public string RoleName { get; set; }

    }
}

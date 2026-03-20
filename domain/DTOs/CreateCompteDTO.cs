using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.DTOs
{
    public class CreateCompteDTO
    {
        [Required]
        [StringLength(8)]
        public string CIN { get; set; }

        public string Nom { get; set; }
        public string Email { get; set; }
        public string Motdepasse { get; set; }
        public bool Access { get; set; }

        [Required]
        public Guid FK_Role { get; set; }
    }

}

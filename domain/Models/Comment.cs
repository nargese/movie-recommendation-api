using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models
{
    public class Comment
    {
        public Guid IdComment { get; set; }
        public Guid FK_Compte { get; set; }
        public Compte? Compte { get; set; } = null!;

        public Guid FK_Movie { get; set; }
        public Movie? Movie { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

}

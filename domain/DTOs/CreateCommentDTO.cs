using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.DTOs
{
    public class CreateCommentDTO
    {
        public Guid IdComment { get; set; }
        public Guid FK_Movie { get; set; }
        public Guid FK_Compte { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

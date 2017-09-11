using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int FileID { get; set; }

        public File File { get; set; }

    }
}

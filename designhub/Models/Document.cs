using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Models
{
    public class Document
    {
        [Key]
        public int DocumentID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime DateCreated { get; set; }

        [Required]
        public string DocumentPath { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int DocumentGroupID { get; set; }

        public DocumentGroup DocumentGroup { get; set; }

    }
}

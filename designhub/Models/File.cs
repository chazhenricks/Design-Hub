using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Models
{
    public class File
    {
        [Key]
        public int FileID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime DateCreated { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int FileGroupID { get; set; }

        public FileGroup FileGroup { get; set; }

    }
}

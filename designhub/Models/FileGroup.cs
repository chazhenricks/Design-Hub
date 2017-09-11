using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Models
{
    public class FileGroup
    {
       
        [Key]
        public int FileGroupID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ProjectFileGroup> ProjectFileGroup { get; set; }
    }
}

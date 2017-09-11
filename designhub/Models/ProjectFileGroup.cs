using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Models
{
    public class ProjectFileGroup
    {
        [Key]
        public int ProjectFileGroupID { get; set; }

        [Required]
        public int ProjectID { get; set; }

        public Project Project { get; set; }

        [Required]
        public int FileGroupID { get; set; }

        public FileGroup FileGroup { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime DateCreated { get; set; }

    }
}

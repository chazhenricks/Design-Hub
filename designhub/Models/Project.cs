using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Models
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        [Required]
        public string Name { get; set; }

        
        [DataType(DataType.Date)]
        [Display(Name = "Last Updated")]
        public DateTime DateCreated { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ProjectDocumentGroup> ProjectDocumentGroup { get; set; }
    }
}

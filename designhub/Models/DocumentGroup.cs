using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Models
{
    public class DocumentGroup
    {
       
        [Key]
        public int DocumentGroupID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]


        public virtual ICollection<ProjectDocumentGroup> ProjectDocumentGroup { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Models.DocumentViewModels
{
    public class DocumentDetailView
    {
        public Document Document { get; set; }

        public List<Comment> Comments { get; set; }
    }
}

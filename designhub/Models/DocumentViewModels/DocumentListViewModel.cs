using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Models.DocumentViewModels
{
    public class DocumentListViewModel
    {
        public List<Document> Documents { get; set; }
        public DocumentGroup DocumentGroup { get; set; }
        public string FileName { get; set; }
    }
}

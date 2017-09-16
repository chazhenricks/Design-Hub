using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Models.DocumentViewModels
{
    public class CreateDocumentViewModel
    {
        public Document Document { get; set; }

        public int DocumentGroupID { get; set; }


        public string Name { get; set; }

        public IFormFile NewDocument { get; set; }



    }
}

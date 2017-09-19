﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Models.DocumentGroupViewModels
{
    public class DocumentGroupsList
    {
        public List<DocumentGroup> DocumentGroups { get; set; }
        public List<Document> Documents { get; set; }
        public Project Project { get; set; }
    }
}

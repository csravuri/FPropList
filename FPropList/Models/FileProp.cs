using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPropList.Models
{
    public class FileProp
    {
        public string Name { get; set; }
        public bool IsFile { get; set; }
        public string FullPath { get; set; }
        public string Size { get; set; }
        public string ModifiedDate { get; set; }

    }
}

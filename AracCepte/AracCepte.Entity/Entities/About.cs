using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.Entity.Entities
{
    public class About
    {
        public int AboutId { get; set; } // about ID
        public string Description { get; set; } // about description
        public string ImageUrl1 { get; set; } // about image URLs
        public string ImageUrl2 { get; set; }
        public string Item1 { get; set; } //about Items
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Item4 { get; set; }
    }
}

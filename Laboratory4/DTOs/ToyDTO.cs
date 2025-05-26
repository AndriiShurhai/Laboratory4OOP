using Laboratory4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory4.DTOs
{
    public class ToyDTO
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public Classification Classification { get; set; }
    }
}

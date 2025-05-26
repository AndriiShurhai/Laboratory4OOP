using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory4.DTOs
{
    public class ToyBatchDTO
    {
        public ToyDTO Toy { get; set; }
        public DateTime DeliveryDate { get; set; }  
        public decimal Price { get; set; }  
        public int Quantity {  get; set; }  
    }
}

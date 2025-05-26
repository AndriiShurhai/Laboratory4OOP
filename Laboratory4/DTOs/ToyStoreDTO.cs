using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory4.DTOs
{
    public class ToyStoreDTO
    {
        public int StoreNumber { get; set; }
        public List<ToyBatchDTO> Batches { get; set; } = new List<ToyBatchDTO>();   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory4.Models
{
    public class ToyStore
    {
        private static int _storeCount;
        private readonly int _storeNumber;
        private readonly List<ToyBatch> _batches = new List<ToyBatch>(); 

        public ToyStore() => _storeNumber = ++_storeCount;

        public int StoreNumber => _storeNumber; 
        public IReadOnlyList<ToyBatch> Batches => _batches;

        public void AddBatch(ToyBatch batch)
        {
            if (batch == null) throw new ArgumentNullException(nameof(batch));
            _batches.Add(batch);
        }

        public void SellOne(ToyBatch batch)
        {
            if (!_batches.Contains(batch))
            {
                throw new InvalidOperationException("Batch not found");
            }
            if (batch.Quantity <= 0)
            {
                throw new InvalidOperationException("No items left to sell.");
            }
            batch.Quantity--;
        }

        public string ToShortString()
        {
            var total = _batches.Sum(b => b.Quantity);
            return $"Store #{StoreNumber}: Total toys = {total}";
        }
    }
}

using System;
using Laboratory4.Models;
using System.Collections.Generic;
using System.Linq;

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
            if (batch.Quantity <= 0) throw new ArgumentNullException(nameof(batch.Quantity));

            _batches.Add(batch);
        }

        public void RemoveBatch(ToyBatch batch)
        {
            if (batch == null)
            {
                throw new ArgumentNullException(nameof(batch));
            }
            _batches.Remove(batch);
        }

        public void SellOne(ToyBatch batch)
        {
            if (batch == null)
                throw new ArgumentNullException(nameof(batch));
            if (!_batches.Contains(batch))
                throw new InvalidOperationException("Batch not found.");
            if (batch.Quantity <= 0)
                throw new InvalidOperationException("No items left to sell.");
            batch.Quantity--;
        }

        public int GetTotalQuantity()
        {
            return _batches.Sum(b => b.Quantity);
        }

        public decimal GetTotalValue()
        {
            return _batches.Sum(b => b.Quantity * b.Price);
        }

        public IEnumerable<ToyBatch> GetBatchesByToy(Toy toy)
        {
            if (toy == null)
            {
                throw new ArgumentNullException(nameof(toy));
            }
            return _batches.Where(b => b.Toy == toy);
        }

        public IEnumerable<ToyBatch> GetBatchesByClassification(Classification classification)
        {
            return _batches.Where(b => b.Toy.Classification == classification);
        }
        public string ToShortString()
        {
            var total = GetTotalQuantity();
            var value = GetTotalValue();
            return $"Store #{StoreNumber}: Total toys = {total}, Total value = ${value:F2}";
        }

        public override string ToString()
        {
            var summary = ToShortString();
            var batchInfo = string.Join("\n", _batches.Select(b => $"  - {b}"));
            return $"{summary}\nBatches:\n{batchInfo}";
        }
    }
}
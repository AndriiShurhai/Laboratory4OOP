using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory4.Models
{
    public class ToyBatch
    {
        private Toy _toy;
        private DateTime _deliveryDate;
        private decimal _price;
        private int _quantity;

        public ToyBatch(Toy toy, DateTime deliveryDate, decimal price, int quantity)
        {
            Toy = toy;
            DeliveryDate= deliveryDate;
            Price = price;
            Quantity = quantity;
        }

        public Toy Toy
        {
            get => _toy;
            set => _toy = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DateTime DeliveryDate
        {
            get => _deliveryDate;
            set => _deliveryDate = value;
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be negative");
                }
                _price = value;
            }
        }
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Quantity cannot be negative");
                }
                _quantity = value;
            }
        }

        public override string ToString()
        {
            return $"{Toy.Name} [{Toy.Classification}] - Date: {DeliveryDate:d}, Price: {Price}, Quantity: {Quantity}";
        }

    }
}

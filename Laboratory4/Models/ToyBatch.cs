using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Laboratory4.Models
{
    public class ToyBatch : Helpers.ObservableObject
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
            set => SetProperty(ref _toy, value ?? throw new ArgumentNullException(nameof(value)));
        }

        public DateTime DeliveryDate
        {
            get => _deliveryDate;
            set => SetProperty(ref _deliveryDate, value);
        }

        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative, (annotation)")]
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be negative");
                }
                SetProperty(ref _price, value);
            }
        }

        [Range(1, double.MaxValue, ErrorMessage = "Quantity cannot be negative, (annotation)")]
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Quantity cannot be negative");
                }
                SetProperty(ref _quantity, value);  
            }
        }

        public override string ToString()
        {
            return $"{Toy.Name} [{Toy.Classification}] - Date: {DeliveryDate:d}, Price: {Price}, Quantity: {Quantity}";
        }

    }
}

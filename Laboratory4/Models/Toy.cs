using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory4.Models
{
    public class Toy
    {
        private string _name;
        private string _manufacturer;
        private Classification _classification;

        public Toy(string name, string manufacturer, Classification classification)
        {
            Name = name;
            Manufacturer = manufacturer;
            Classification = classification;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name cannot be empty");
                }
                _name = value;
            }
        }

        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Manufacturer cannot be empty");
                }
                _manufacturer = value;
            }
        }

        public Classification Classification
        {
            get => _classification;
            set => _classification = value;
        }

        public override string ToString()
        {
            return $"{Name} ({Manufacturer}) - {Classification}";
        }
    }
}

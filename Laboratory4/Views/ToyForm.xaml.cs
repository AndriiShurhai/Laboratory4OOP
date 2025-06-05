using Laboratory4.Helpers;
using Laboratory4.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Laboratory4.Views
{
    /// <summary>
    /// Interaction logic for ToyForm.xaml
    /// </summary>
    /// 

    public partial class ToyForm : Window
    {
        public Toy CreatedToy { get; private set; }

        private string _name;
        private string _manufacturer;
        private Classification _classification;


        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Manufacturer
        {
            get => _manufacturer;
            set => _manufacturer = value;
        }

        public Classification Classification
        {
            get => _classification;
            set => _classification = value;
        }

        public ObservableCollection<Classification> EnumValues { get; }

        public ToyForm()
        {
            InitializeComponent();
            EnumValues = new ObservableCollection<Classification>(Enum.GetValues<Classification>());
            Classification = EnumValues[0];
            DataContext = this;
        }

        private bool ValidateToy(Toy toy)
        {
            var context = new ValidationContext(toy);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(toy, context, results, true);

            if (!isValid)
            {
                string errorMessage = string.Join("\n", results.Select(r => r.ErrorMessage));
                MessageBox.Show(errorMessage, "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return isValid;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string toyName = txtName.Text?.Trim();
                string manufacturer = txtManufacturer.Text?.Trim();

                //if (string.IsNullOrWhiteSpace(toyName))
                //{
                //    MessageBox.Show("Please enter a toy name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    txtName.Focus();
                //    return;
                //}

                //if (string.IsNullOrWhiteSpace(manufacturer))
                //{
                //    MessageBox.Show("Please enter a manufacturer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    txtManufacturer.Focus();
                //    return;
                //}

                CreatedToy = new Toy(toyName, manufacturer, Classification);
                if (!ValidateToy(CreatedToy))
                {
                    return;
                }
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while creating a toy: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);  
            }
        }
    }
}
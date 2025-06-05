using Laboratory4.Helpers;
using Laboratory4.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Laboratory4.Views
{
    /// <summary>
    /// Interaction logic for BatchForm.xaml
    /// </summary>
    public partial class BatchForm : Window
    {
        public ToyBatch Batch { get; private set; }
        public ObservableCollection<Toy> Toys { get; }
        public Toy SelectedToy { get; set; }
        public DateTime DeliveryDate { get; set; } = DateTime.Today;
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public BatchForm(ObservableCollection<Toy> availableToys)
        {
            InitializeComponent();
            Toys = availableToys ?? new ObservableCollection<Toy>();
            DataContext = this;
        }
        private bool ValidateBatch(ToyBatch toyBatch)
        {
            var context = new ValidationContext(toyBatch);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(toyBatch, context, results, true);

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
                //if (SelectedToy == null)
                //{
                //    MessageBox.Show("Please select a toy.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}

                //if (Price < 0)
                //{
                //    MessageBox.Show("Price cannot be negative.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}

                //if (Quantity <= 0)
                //{
                //    MessageBox.Show("Quantity cannot be negative or zero", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}

                //if (DeliveryDate < DateTime.Today)
                //{
                //    var result = MessageBox.Show("The delivery date is in the past. Do you want to continue?", "Date warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

                //    if (result == MessageBoxResult.No)
                //    {
                //        return;
                //    }
                //}
                Batch = new ToyBatch(SelectedToy, DeliveryDate, Price, Quantity);
                if(!ValidateBatch(Batch))
                {
                    return;
                }
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while creating batch: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
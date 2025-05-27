using Laboratory4.DTOs;
using Laboratory4.Helpers;
using Laboratory4.Models;
using Laboratory4.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Laboratory4.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string _storeStatus;
        private readonly ObservableCollection<Toy> _availableToys;

        public ToyStore Store { get; }
        public ObservableCollection<ToyBatch> Batches { get; }
        public ICommand AddBatchCommand { get; }
        public ICommand AddToyCommand { get; }
        public ICommand SellOneCommand { get; }

        public string StoreStatus
        {
            get => _storeStatus;
            private set => SetProperty(ref _storeStatus, value);
        }

        public MainViewModel()
        {
            Store = new ToyStore();
            Batches = new ObservableCollection<ToyBatch>(Store.Batches);
            _availableToys = new ObservableCollection<Toy>();

            Batches.CollectionChanged += OnBatchesChanged;

            AddBatchCommand = new RelayCommand(_ => AddBatch(), _ => _availableToys.Count > 0);
            AddToyCommand = new RelayCommand(_ => AddToy());
            SellOneCommand = new RelayCommand(p => SellOne(p as ToyBatch), p => p is ToyBatch b && b.Quantity > 0);

            UpdateStoreStatus();
        }

        private void OnBatchesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateStoreStatus();

            if (e.NewItems != null)
            {
                foreach (ToyBatch batch in e.NewItems)
                {
                    batch.PropertyChanged += OnBatchPropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (ToyBatch oldBatch in e.OldItems)
                {
                    oldBatch.PropertyChanged -= OnBatchPropertyChanged;
                }
            }
        }

        private void OnBatchPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ToyBatch.Quantity))
            {
                UpdateStoreStatus();
                ((RelayCommand)SellOneCommand).RaiseCanExecuteChanged();

                var batch = sender as ToyBatch;
                if (batch?.Quantity == 0)
                {
                    Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        Batches.Remove(batch);
                        Store.RemoveBatch(batch);
                    });
                }
            }
        }

        private void UpdateStoreStatus()
        {
            var total = Batches.Sum(b => b.Quantity);
            var totalValue = Batches.Sum(b => b.Price * b.Quantity);
            StoreStatus = $"Store #{Store.StoreNumber}: Total toys = {total}, Total value = ${totalValue:F2}";
        }

        private void AddBatch()
        {
            try
            {
                var form = new BatchForm(_availableToys);
                if (form.ShowDialog() == true && form.Batch != null)
                {
                    var batch = form.Batch;
                    Store.AddBatch(batch);
                    Batches.Add(batch);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error adding batch: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddToy()
        {
            try
            {
                var form = new ToyForm();
                if (form.ShowDialog() == true && form.CreatedToy != null)
                {
                    var newToy = form.CreatedToy;

                    var existingToy = _availableToys.FirstOrDefault(t =>
                       string.Equals(t.Name, newToy.Name, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(t.Manufacturer, newToy.Manufacturer, StringComparison.OrdinalIgnoreCase));

                    if (existingToy == null)
                    {
                        _availableToys.Add(newToy);
                        ((RelayCommand)AddBatchCommand).RaiseCanExecuteChanged();
                        MessageBox.Show($"Toy '{newToy.Name}' added successfullu!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"A toy with the name '{newToy.Name}' from '{newToy.Manufacturer}' already exists.", "Dublicate toy", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show($"Error adding toy: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);    
            }
        }
        private void SellOne(ToyBatch batch)
        {
            try
            {
                if (batch == null)
                {
                    MessageBox.Show("Please select a batch to sell from.", "No selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Store.SellOne(batch);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
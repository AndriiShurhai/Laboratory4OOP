using Laboratory4.DTOs;
using Laboratory4.Helpers;
using Laboratory4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Laboratory4.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string _storeStatus;

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

            Batches.CollectionChanged += OnBatchesChanged;

            AddBatchCommand = new RelayCommand(_ => { return; });
            AddToyCommand = new RelayCommand(_ => { return; });
            SellOneCommand = new RelayCommand(p => SellOne(p as ToyBatch), p => p is ToyBatch b && b.Quantity > 0);

            UpdateStoreStatus();
        }

        private void OnBatchesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateStoreStatus();
        }

        private void UpdateStoreStatus()
        {
            var total = Batches.Sum(b => b.Quantity);
            StoreStatus = $"Store #{Store.StoreNumber}: Total toys = {total}";
        }

        private void SellOne(ToyBatch batch)
        {
            Store.SellOne(batch);
            UpdateStoreStatus(); 
            ((RelayCommand)SellOneCommand).RaiseCanExecuteChanged();
        }
    }
}
using BookStore.Models;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace BookStore.ViewModel.Employee
{
    public class RemoveProductsViewModel : ViewModelBase
    {
        private Product selectedProduct;
        private DispatcherTimer dispatcherTimer;
        private bool isSuccussfullyRemoved;
        private Visibility succesfullyRemovedNotefication;
        private Visibility failedToRemoveNotefication;

        public RelayCommand RemoveCommand { get; set; }
        public Product SelectedProduct { get => selectedProduct; set => Set(ref selectedProduct, value); }
        public ObservableCollection<Product> ProductList { get; set; }

        public Visibility SuccesfullyRemovedNotefication { get => succesfullyRemovedNotefication; set => Set(ref succesfullyRemovedNotefication, value); }
        public Visibility FailedToRemoveNotefication { get => failedToRemoveNotefication; set => Set(ref failedToRemoveNotefication, value); }

        public RemoveProductsViewModel()
        {
            RemoveCommand = new RelayCommand(RemoveProduct);
            this.ProductList = new ObservableCollection<Product>(ProductService.Instance.Products.GetAll());
            dispatcherTimer = new DispatcherTimer();
            SuccesfullyRemovedNotefication = Visibility.Collapsed;
            FailedToRemoveNotefication = Visibility.Collapsed;
            MessengerInstance.Register<bool>(this, "RefreshEmployeeView", RefreshProductsDisplay);
        }

        private void RefreshProductsDisplay(bool obj)
        {
            ProductList = new ObservableCollection<Product>(ProductService.Instance.Products.GetAll());
        }

        private void RemoveProduct()
        {
            if (SelectedProduct == null) return;
            isSuccussfullyRemoved = ProductService.Instance.Products.Delete(SelectedProduct.Id);
            ProductList.Remove(SelectedProduct);
            ShowRelevantNotefication(isSuccussfullyRemoved);
        }

        private void InitTimer()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
           if (isSuccussfullyRemoved)
            {
                SuccesfullyRemovedNotefication = Visibility.Collapsed;
            }
            else
            {
                FailedToRemoveNotefication = Visibility.Collapsed;
            }
        }
        private void ShowRelevantNotefication(bool productSuccessfullyRemoved)
        {
            if (productSuccessfullyRemoved)
            {
                SuccesfullyRemovedNotefication = Visibility.Visible;
                InitTimer();
            }
            else
            {
                FailedToRemoveNotefication = Visibility.Visible;
                InitTimer();
            }
        }
    }
}

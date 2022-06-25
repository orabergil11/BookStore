// ----------------------------------------------------------------------------------------------------- //
//                                                                                                       //
// @File      RemoveProductsViewModel.cs                                                                 //
// @Details   Responsible on removing products from stock                                                //
// @Author    Or Abergil                                                                                 //
// @Since     15/03/2022                                                                                 //
//                                                                                                       //
// ----------------------------------------------------------------------------------------------------- //

using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows;
using System;

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;

using BookStore.Models;
using BookStore.Server;

namespace BookStore.ViewModel.Employee
{
    public class RemoveProductsViewModel : ViewModelBase
    {
        // Fields
        private ObservableCollection<Product> productList;
        private Visibility succesfullyRemoved_Notification;
        private Visibility failedToRemove_Notification;
        private DispatcherTimer dispatcherTimer;
        private Product selectedProduct;

        // Notifications
        public Visibility SuccesfullyRemoved_Notification { get => succesfullyRemoved_Notification; set => Set(ref succesfullyRemoved_Notification, value); }
        public Visibility FailedToRemove_Notification     { get => failedToRemove_Notification;     set => Set(ref failedToRemove_Notification, value); }
        public Product SelectedProduct                    { get => selectedProduct;                 set => Set(ref selectedProduct, value); }
        public ObservableCollection<Product> ProductList  { get => productList;                     set => Set(ref productList, value); }

        // Commands
        public RelayCommand Remove_Command { get; set; }

        // Flags
        private bool isSuccussfullyRemoved;

        public RemoveProductsViewModel()
        {
            this.ProductList                     = new ObservableCollection<Product>(ProductService.Instance.Products.GetAll());
            this.Remove_Command                  = new RelayCommand(RemoveProduct);
            this.dispatcherTimer                 = new DispatcherTimer();
            this.SuccesfullyRemoved_Notification = Visibility.Collapsed;
            this.FailedToRemove_Notification     = Visibility.Collapsed;
            this.MessengerInstance.Register<bool>(this, "RefreshEmployeeView", RefreshProductsDisplay);
        }

        private void RefreshProductsDisplay(bool obj)
        {
            ProductList = new ObservableCollection<Product>(ProductService.Instance.Products.GetAll());
        }

        private void RemoveProduct()
        {
            if (SelectedProduct == null)
            {
                ShowRelevantNotification(isSuccussfullyRemoved);
                return;
            }

            isSuccussfullyRemoved = ProductService.Instance.Products.Delete(SelectedProduct.Id);

            if (isSuccussfullyRemoved)
            {
                ProductList.Remove(SelectedProduct);
            }
            
            ShowRelevantNotification(isSuccussfullyRemoved);
        }

        private void InitTimer()
        {
            dispatcherTimer.Tick    += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(Consts.NOTIFICATION_TIME_HOUR,
                                                    Consts.NOTIFICATION_TIME_MIN,
                                                    Consts.NOTIFICATION_TIME_SEC);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var notification = FailedToRemove_Notification;

            if (isSuccussfullyRemoved)
            {
                notification = SuccesfullyRemoved_Notification;
            }

            notification = Visibility.Collapsed;
        }

        private void ShowRelevantNotification(bool productSuccessfullyRemoved)
        {
            if (productSuccessfullyRemoved)
            {
                SuccesfullyRemoved_Notification = Visibility.Visible;
            }

            else
            {
                FailedToRemove_Notification = Visibility.Visible;
            }

            InitTimer();
        }
    }
}

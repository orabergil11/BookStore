using BookStore.Views;
using BookStore.Views.EmployeeViews;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Controls;

namespace BookStore.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        public RelayCommand AddBookMenu { get; set; }
        public RelayCommand RemoveProductsMenu { get; set; }

        public EmployeeViewModel()
        {
            AddBookMenu = new RelayCommand(GoToAddProductPage);
            RemoveProductsMenu = new RelayCommand(GoToRemoveProductsPage);
        }

        private void GoToRemoveProductsPage()
        {
            MessengerInstance.Send<UserControl>(new RemoveProductsView());
        }

        private void GoToAddProductPage()
        {
            MessengerInstance.Send<UserControl>(new ReplenishmentView());
        }
    }
}

// ----------------------------------------------------------------------------------------------------- //
//                                                                                                       //
// @File      EmployeeViewModel.cs                                                                       //
// @Details   Responsible on navigtion between different employee views                                  //
// @Author    Or Abergil                                                                                 //
// @Since     15/03/2022                                                                                 //
//                                                                                                       //
// ----------------------------------------------------------------------------------------------------- //

using BookStore.Views;
using BookStore.Views.EmployeeViews;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;

namespace BookStore.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        public RelayCommand ChooseProductsMenu { get; set; }
        public RelayCommand RemoveProductsMenu { get; set; }

        public EmployeeViewModel()
        {
            ChooseProductsMenu = new RelayCommand(GoToAddProductPage);
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

/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:BookStore"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using BookStore.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Controls;

namespace BookStore.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        public RelayCommand BuyBookCommand { get; set; }
        public RelayCommand ProductMenuCommand { get; set; }

        public CustomerViewModel()
        {
            BuyBookCommand = new RelayCommand(GoToBuyBook);
            ProductMenuCommand = new RelayCommand(GoToProductMenuCommand);
        }

        private void GoToProductMenuCommand()
        {
            MessengerInstance.Send<UserControl>(new ProductMenuView());
        }

        private void GoToBuyBook()
        {
            MessengerInstance.Send<UserControl>(new ProductMenuView());
        }
    }
}
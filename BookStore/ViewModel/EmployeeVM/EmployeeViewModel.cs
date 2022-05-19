using BookStore.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookStore.ViewModel
{
   public class EmployeeViewModel : ViewModelBase
    {
        public RelayCommand AddBookMenu { get; set; }

        public EmployeeViewModel()
        {
            AddBookMenu = new RelayCommand(GoToAddProductPage);

        }

        private void GoToAddProductPage()
        {
            MessengerInstance.Send<UserControl>(new ReplenishmentView());
        }
    }
}

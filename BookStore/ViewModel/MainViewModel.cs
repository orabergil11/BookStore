using BookStore.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Runtime.Remoting;
using System.Windows.Controls;

namespace BookStore.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserControl myUserControl;
        public UserControl MyUserControl { get => myUserControl; set => Set(ref myUserControl, value); }

        public RelayCommand EmployeeCommand { get; set; }
        public RelayCommand CustomerCommand { get; set; }
        public RelayCommand HomeCommand { get; set; }

        public MainViewModel()
        {
            MyUserControl = new HomeView(); // First Screen Is Start View
            MessengerInstance.Register<UserControl>(this, UpdateUserControl);
            EmployeeCommand = new RelayCommand(GoToEmployeePage);
            CustomerCommand = new RelayCommand(GoToCustomerPage);
            HomeCommand = new RelayCommand(GoToHomePage);
        }


        private void UpdateUserControl(UserControl userControl) => MyUserControl = userControl;

        private void GoToCustomerPage()
        {
            
            MessengerInstance.Send<UserControl>(new CustomerView());
        }

        private void GoToEmployeePage() => MessengerInstance.Send<UserControl>(new EmployeeView());
        private void GoToHomePage() => MessengerInstance.Send<UserControl>(new HomeView());
    }
}
using BookStore.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        public RelayCommand CartCommand { get; set; }

        public MainViewModel()
        {
            MyUserControl = new HomeView(); // Set the first screen to 'Homepage'.
            MessengerInstance.Register<UserControl>(this, UpdateUserControl);
            EmployeeCommand = new RelayCommand(GoToEmployeePage);
            CustomerCommand = new RelayCommand(GoToCustomerPage);
            HomeCommand = new RelayCommand(GoToHomePage);
            CartCommand = new RelayCommand(GoToCartPage);
        }
        private void UpdateUserControl(UserControl userControl) => MyUserControl = userControl;
        private void GoToCartPage()
        {
            MessengerInstance.Send<UserControl>(new CartView());
            MessengerInstance.Send<bool>(true,"RefreshCartView");
        }
        private void GoToCustomerPage()
        {
            MessengerInstance.Send<UserControl>(new ProductMenuView());
            MessengerInstance.Send<bool>(true, "RefreshProductMenuView");
        }

        private void GoToEmployeePage()
        {
            MessengerInstance.Send<UserControl>(new EmployeeView());
            MessengerInstance.Send<bool>(true, "RefreshEmployeeView");
        }

        private void GoToHomePage() => MessengerInstance.Send<UserControl>(new HomeView());
    }
}
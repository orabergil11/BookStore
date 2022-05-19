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

using BookStore.ViewModel.CustomerVM;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;


namespace BookStore.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EmployeeViewModel>();
            SimpleIoc.Default.Register<ProductMenuViewModel>();
            SimpleIoc.Default.Register<ReplenishmentViewModel>();
            SimpleIoc.Default.Register<AddBookViewModel>();
            SimpleIoc.Default.Register<AddJournalViewModel>();
            SimpleIoc.Default.Register<CartViewModel>();

        }
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public EmployeeViewModel Employee => ServiceLocator.Current.GetInstance<EmployeeViewModel>();
        public ProductMenuViewModel ProductMenu => ServiceLocator.Current.GetInstance<ProductMenuViewModel>();
        public ReplenishmentViewModel Replenishment => ServiceLocator.Current.GetInstance<ReplenishmentViewModel>();
        public AddBookViewModel AddBook => ServiceLocator.Current.GetInstance<AddBookViewModel>();
        public AddJournalViewModel AddJournal => ServiceLocator.Current.GetInstance<AddJournalViewModel>();
        public CartViewModel Cart => ServiceLocator.Current.GetInstance<CartViewModel>();
    }
}
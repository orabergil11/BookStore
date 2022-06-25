// ----------------------------------------------------------------------------------------------------- //
//                                                                                                       //
// @File      RemoveProductsViewModel.cs                                                                 //
// @Details   Responsible on displaying the products types the employee can add to stock                 //
// @Author    Or Abergil                                                                                 //
// @Since     15/03/2022                                                                                 //
//                                                                                                       //
// ----------------------------------------------------------------------------------------------------- //

using System.Collections.ObjectModel;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Linq;
using System;

using BookStore.Models.Enums;
using GalaSoft.MvvmLight;

namespace BookStore.ViewModel
{
    public class ReplenishmentViewModel : ViewModelBase
    {
        public ObservableCollection<ProductTypes> ProductsTypesToAdd { get; set; }
        private ProductTypes selectedProduct;

        public ProductTypes SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                Set(ref selectedProduct, value);
                ShowRelevantView();
            }
        }

        public ReplenishmentViewModel()
        {
            this.ProductsTypesToAdd = new ObservableCollection<ProductTypes>(Enum.GetValues(typeof(ProductTypes)).Cast<ProductTypes>());
        }

        public void ShowRelevantView()
        {
            ObjectHandle handle = Activator.CreateInstance(null, "BookStore.Views.Add" + SelectedProduct.ToString() + "View");
            object RelevantPage = handle.Unwrap();
            MessengerInstance.Send<UserControl>((UserControl)RelevantPage);
        }
    }
}

using BookStore.Models.Enums;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting;
using System.Windows.Controls;

namespace BookStore.ViewModel
{
    public class ReplenishmentViewModel : ViewModelBase
    {
        public ObservableCollection<ProductTypes> ItemsToAdd { get; set; }

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
            this.ItemsToAdd = new ObservableCollection<ProductTypes>(Enum.GetValues(typeof(ProductTypes)).Cast<ProductTypes>());
        }

        public void ShowRelevantView()
        {
            ObjectHandle handle = Activator.CreateInstance(null, "BookStore.Views.Add" + SelectedProduct.ToString() + "View");
            object O = handle.Unwrap();
            MessengerInstance.Send<UserControl>((UserControl)O);
        }
    }
}

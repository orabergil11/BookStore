using BookStore.Models;
using BookStore.Server;
using BookStore.Views;
using BookStore.Views.CustomerViews;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BookStore.ViewModel.CustomerVM
{
    public class CartViewModel : ViewModelBase
    {
        private ObservableCollection<Product> currentProductsInCart;
        private Product selectedProduct;
        private double selectedProductPrice;
        private int selectedProductQuantity;
        private double totalCartValue;
        public RelayCommand BuyProductCommand { get; set; }
        public double SelectedProductPrice { get => selectedProductPrice; set => Set(ref selectedProductPrice, value); }
        public int SelectedProductQuantity { get => selectedProductQuantity; set => Set(ref selectedProductQuantity, value); }
        public double TotalCartValue { get => totalCartValue; set => Set(ref totalCartValue, value); }

        public ObservableCollection<Product> CurrentProductsInCart
        {
            get { 
                TotalCartValue = ShoppingCart.Instance.GetCartValue();
                return currentProductsInCart; }
            set
            {
                Set(ref currentProductsInCart, value);
            }
        }
        public Product SelectedProduct
        {
            get => selectedProduct; set
            {
                Set(ref selectedProduct, value);
                UpdateProductDetails();
            }
        }

        public CartViewModel()
        {
            currentProductsInCart = new ObservableCollection<Product>(ShoppingCart.Instance.ProductsInCart);
            BuyProductCommand = new RelayCommand(BuyProduct);
            MessengerInstance.Register<bool>(this,"cart", InitCart);
            MessengerInstance.Register<bool>(this, "resetCart", resetCart);
        }

        private void resetCart(bool obj)
        {
            CurrentProductsInCart.Clear();
            ShoppingCart.Instance.ProductsInCart.Clear();
            SelectedProductPrice = 0;
            SelectedProductQuantity = 0;
            TotalCartValue = 0;
        }
        private void InitCart(bool obj)
        {
            CurrentProductsInCart = new ObservableCollection<Product>(ShoppingCart.Instance.ProductsInCart);
            UpdateProductDetails();
        }

        private void UpdateProductDetails()
        {
            if (SelectedProduct == null) return;
            SelectedProductPrice = SelectedProduct.GetTotalPrice();
            SelectedProductQuantity = SelectedProduct.Quantity;
        }

        private void BuyProduct()
        {
            if (currentProductsInCart.Count == 0) return;
            ProductService.Instance.BuyProduct();
            MessengerInstance.Send<bool>(true,"resetCart");
            MessengerInstance.Send<UserControl>(new ThankYouView());
        }
    }
}

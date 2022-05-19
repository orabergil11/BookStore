using BookStore.Models;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;

namespace BookStore.ViewModel.CustomerVM
{
    public class CartViewModel : ViewModelBase
    {
        private ObservableCollection<Product> currentProductsInCart;
        private Product selectedProduct;
        private double selectedProductPrice;
        private int selectedProductQuantity;
        private string totalCartValue;
        public RelayCommand BuyProductCommand { get; set; }

        public ObservableCollection<Product> CurrentProductsInCart
        {
            get { currentProductsInCart = new ObservableCollection<Product>(ShoppingCart.Instance.ProductsInCart);
                TotalCartValue = ShoppingCart.Instance.GetCartValue();
                return currentProductsInCart; }
            set
            {
                Set(ref currentProductsInCart, value);
            }
        }
        public double SelectedProductPrice { get => selectedProductPrice; set => Set(ref selectedProductPrice, value); }
        public int SelectedProductQuantity { get => selectedProductQuantity; set => Set(ref selectedProductQuantity, value); }
        public string TotalCartValue { get => totalCartValue; set => Set(ref totalCartValue, value); }

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
        }
        private void UpdateProductDetails()
        {
            if (SelectedProduct == null) return;
            SelectedProductPrice = SelectedProduct.GetTotalPrice();
            SelectedProductQuantity = SelectedProduct.Quantity;
        }
        private void InitCart(bool obj)
        {
            currentProductsInCart = new ObservableCollection<Product>(ShoppingCart.Instance.ProductsInCart);
            UpdateProductDetails();
        }
        private void BuyProduct()
        {
            ProductService.Instance.BuyProduct();
        }
    }
}

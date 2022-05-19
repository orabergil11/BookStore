using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public class ShoppingCart
    {
        public static ShoppingCart Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCart();
                }
                return instance;
            }
        }
        public List<Product> ProductsInCart { get; set; }
        private static ShoppingCart instance = null;

        private ShoppingCart()
        {
            ProductsInCart = new List<Product>();
        }

        public double GetCartValue()
        {
            double totalValue = 0;
            for (int i = 0; i < ProductsInCart.Count; i++)
            {
                totalValue += ProductsInCart[i].GetTotalPrice();
            }
            return totalValue;
        }

        public void RemoveProductFromCart(Product productToRemove)
        {
            ProductsInCart.Remove(productToRemove);
        }
    }
}

using BookStore.DataBase;
using BookStore.Models;
using BookStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Server
{
    public class ProductService
    {
        public static ProductService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductService();
                }
                return instance;
            }
        }

        private static ProductService instance = null;

        public ProductRepository Products { get; set; }

        private ProductService()
        {
            Products = new ProductRepository();
            //Fill(); // uncomment if you want to fill database with default products
        }
        private void Fill()
        {
            var products = new Product[]
            {
                new Book ("JK.Rollin", "Hary Potter Philosopher's Stone", 10, DateTime.Now, 30,BookGenre.Action,1 ),
                new Book ("JK.Rollin", "Hary Potter Chamber of Secrets", 10, DateTime.Now, 30,BookGenre.Action,2 ),
                new Book ("JK.Rollin", "Hary Potter Prisoner of Azkaban", 10, DateTime.Now, 30,BookGenre.Action,3 ),
                new Book ("JK.Rollin", "Hary Potter Goblet of Fire", 10, DateTime.Now, 30,BookGenre.Action,3 ),
                new Book ("JK.Rollin", "Hary Potter Order of the Phoenix", 10, DateTime.Now, 30,BookGenre.Action,3 ),
                new Book ("JK.Rollin", "Hary Potter Deathly Hallows ", 10, DateTime.Now, 30,BookGenre.Action,3 ),
                new Journal ("Amos Shoken", "Haaretz", 1, 10, DateTime.Now, 50, JournalFrequency.Annually, JournalGenre.Law),
                new Journal ("Aviv Shalom", "Yedioth_Aharonot", 1, 10, DateTime.Now, 50, JournalFrequency.Annually, JournalGenre.Law),
                new Journal ("George Nap ", "National Geographic", 1, 10, DateTime.Now, 50, JournalFrequency.Annually, JournalGenre.Law),
                new Journal ("Robert Falcon Scott", "Captain Scott's Last Expedition", 1, 10, DateTime.Now, 50, JournalFrequency.Annually, JournalGenre.Nature)
            };
            using (Products.Data)
            {
                Products.Data._Products.AddRange(products);
            }
        }//setting default products for testing

        public bool AddProductsToCart(Product selectedProduct, int numOfProductsToBuy)
        {
            var productsInCart = ShoppingCart.Instance.ProductsInCart;

            if (selectedProduct == null) return false;
            if (selectedProduct.Quantity < numOfProductsToBuy || numOfProductsToBuy <= 0)
                return false;

            foreach (var product in productsInCart)
            {
                if (product.Id == selectedProduct.Id)
                {
                    product.Quantity += numOfProductsToBuy;
                    return true;
                } 
            }

            for (int i = 0; i < productsInCart.Count; i++)
            {
                if (productsInCart[i].Quantity + numOfProductsToBuy > selectedProduct.Quantity)
                    return false;
            }

            for (int i = 0; i < productsInCart.Count; i++)
            {
                if (productsInCart[i].Id == selectedProduct.Id)
                    productsInCart[i].Quantity += numOfProductsToBuy;
            }

            Product newProductInCart = (Product)selectedProduct.Clone();
            newProductInCart.Quantity = numOfProductsToBuy;
            productsInCart.Add(newProductInCart);
            return true;
        }
        public void BuyProduct()
        {
            var shoppingCart = ShoppingCart.Instance.ProductsInCart;

            foreach (var prodInCart in shoppingCart)
            {
                foreach (var prodInDb in Products.GetAll())
                {
                    if (prodInCart.Id == prodInDb.Id)
                    {
                        var quantityAfterPurchase = prodInDb.Quantity = prodInDb.Quantity - prodInCart.Quantity;

                        if (quantityAfterPurchase == 0)
                        {
                            Products.Delete(prodInDb.Id);
                        }

                        Products.Update(prodInDb, quantityAfterPurchase);
                        return;
                    }
                }
            }
        }
        public void AddBook(string autoherName, string title, int quantity, DateTime publicationDate, decimal basePrice, BookGenre bookGenre, int eddition)
        {
            var book = new Book(autoherName, title, quantity, publicationDate, basePrice, bookGenre, eddition);
            Products.Add(book);
        }
        public void AddJournal(string editorName, string name, int issueNumber, int quantuty, DateTime publicationDate,
            decimal basePrice, JournalFrequency frequency, JournalGenre genres)
        {
            var journal = new Journal(editorName, name, issueNumber, quantuty, publicationDate, basePrice, frequency, genres);
            Products.Add(journal);
        }
    }
}

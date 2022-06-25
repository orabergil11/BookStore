// ------------------------------------------------------------------------------------------------------ //
//                                                                                                        //
// @File      ProductService.cs                                                                           //
// @Details   Responsible to be the "logical" layer between the  server side and client side              //
// @Author    Or Abergil                                                                                  //
// @Since     15/03/2022                                                                                  //
//                                                                                                        //
// ------------------------------------------------------------------------------------------------------ //

using BookStore.Models.Enums;
using BookStore.DataBase;
using BookStore.Models;
using System;

namespace BookStore.Server
{
    public class ProductService
    {
        public static ProductService Instance
        {
            // 'singelton' pattern  
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
                new Book ("JK.Rollin",      "Hary Potter Philosopher's Stone",   10, DateTime.Now, 30,BookGenre.Action,1 ),
                new Book ("JK.Rollin",      "Hary Potter Chamber of Secrets",    10, DateTime.Now, 30,BookGenre.Action,2 ),
                new Book ("JK.Rollin",      "Hary Potter Prisoner of Azkaban",   10, DateTime.Now, 30,BookGenre.Action,3 ),
                new Book ("JK.Rollin",      "Hary Potter Goblet of Fire",        10, DateTime.Now, 30,BookGenre.Action,3 ),
                new Book ("JK.Rollin",      "Hary Potter Order of the Phoenix",  10, DateTime.Now, 30,BookGenre.Action,3 ),
                new Book ("JK.Rollin",      "Hary Potter Deathly Hallows ",      10, DateTime.Now, 30,BookGenre.Action,3 ),
                new Journal ("Amos Shoken", "Haaretz",                                1, 10, DateTime.Now, 50, JournalFrequency.Annually, JournalGenre.Law),
                new Journal ("Aviv Shalom", "Yedioth_Aharonot",                       1, 10, DateTime.Now, 50, JournalFrequency.Annually, JournalGenre.Law),
                new Journal ("George Nap ", "National Geographic",                    1, 10, DateTime.Now, 50, JournalFrequency.Annually, JournalGenre.Law),
                new Journal ("Robert Falcon Scott", "Captain Scott's Last Expedition",1, 10, DateTime.Now, 50, JournalFrequency.Annually, JournalGenre.Nature)
            };
            using (Products.Data)
            {
                Products.Data._Products.AddRange(products);
            }
        } //setting default products for testing

        public bool AddProductsToCart(Product selectedProduct, int numOfProductsToBuy)
        {
            var productsInCart = ShoppingCart.Instance.ProductsInCart;

            // check if input valid
            if (selectedProduct == null) return false;
            if (selectedProduct.Quantity < numOfProductsToBuy || numOfProductsToBuy <= 0)
                return false;

            // if product exist
            bool isProductInCart = false;
            Product productInCart = default; 

            foreach (var product in productsInCart)
            {
                if (product.Id == selectedProduct.Id)
                {
                    isProductInCart = true;
                    productInCart = product;
                }
            }

            if (isProductInCart)
            {
                if (productInCart.Quantity + numOfProductsToBuy > selectedProduct.Quantity)
                {
                    return false;
                }
                productInCart.Quantity += numOfProductsToBuy;
            }
            else
            {
                Product newProductInCart = (Product)selectedProduct.Clone();
                newProductInCart.Quantity = numOfProductsToBuy;
                productsInCart.Add(newProductInCart);
            }
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

        public bool AddBook(Book bookToAdd)
        {
            // details validation
            if (IsProductExists(bookToAdd.Title))
            {
                UpdateQuantityForExistingProduct(bookToAdd.Title, bookToAdd.Quantity);
            }
            else
            {
                Products.Add(bookToAdd);
            }

            return true;
        }

        public bool AddJournal(Journal journalToAdd)
        {
            // details validation
            if (IsProductExists(journalToAdd.Name))
            {
                UpdateQuantityForExistingProduct(journalToAdd.Name, journalToAdd.Quantity);
            }

            else
            {
                Products.Add(journalToAdd);
            }

             return true;
        }

        

        private void UpdateQuantityForExistingProduct(string title, int quantity)
        {
            var productInStock  = Products.Get(title);
            var UpdatedQuantity = productInStock.Quantity + quantity;
            Products.Update(productInStock, UpdatedQuantity);
        }

        private bool IsProductExists(string title)
        {
            if (Products.Get(title) == null) return false;
            else return true;
        }
    }
}

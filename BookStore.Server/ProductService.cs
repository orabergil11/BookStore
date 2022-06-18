using BookStore.DataBase;
using BookStore.Models;
using BookStore.Models.Enums;
using System;

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

        public bool AddBook(string autoherName, string title, int quantity, DateTime publicationDate, decimal basePrice, BookGenre bookGenre, int eddition)
        {
            // details validation
            if (ProductAlreadyExists(title))
            {
                UpdateQuantityForExistingProduct(title, quantity);
                return true;
            }

            if (AreValuesValid(title, autoherName, quantity, basePrice, eddition))
            {
                var book = new Book(autoherName, title, quantity, publicationDate, basePrice, bookGenre, eddition);
                Products.Add(book);
                return true;
            }
            else return false;
        }

        public bool AddJournal(string editorName, string title, int issueNumber, int quantity, DateTime publicationDate,
            decimal basePrice, JournalFrequency frequency, JournalGenre genres)
        {
            // details validation
            if (ProductAlreadyExists(title))
            {
                UpdateQuantityForExistingProduct(title, quantity);
                return true;
            }

            if (AreValuesValid(title, editorName, quantity, basePrice, issueNumber))
            {
                var journal = new Journal(editorName, title, issueNumber, quantity, publicationDate, basePrice, frequency, genres);
                Products.Add(journal);
                return true;
            }
            else return false;

        }

        private bool AreValuesValid(string title, string editorOrAuthor, int quantity, decimal basePrice, int issueOrEddition)
        {
            if (quantity <= 0) return false;
            if (basePrice <= 0) return false;
            if (issueOrEddition <= 0) return false;
            if (title == default) return false;
            if (editorOrAuthor == default) return false;
            if (editorOrAuthor.Length == 0) return false;
            else return true;
        }

        private void UpdateQuantityForExistingProduct(string title, int quantity)
        {
            var productInStock = Products.Get(title);
            var UpdatedQuantity = productInStock.Quantity + quantity;
            Products.Update(productInStock, UpdatedQuantity);
        }

        private bool ProductAlreadyExists(string title)
        {
            if (Products.Get(title) == null) return false;
            else return true;
        }
    }
}

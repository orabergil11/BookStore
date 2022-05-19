using BookStore.DataBase.Interfaces;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.DataBase
{
    public class ProductRepository : IRepository<Product>
    {
        public ProductDataContext Data { get; set; }
        public ProductRepository()
        {
            Data = new ProductDataContext();
        }

        public void Add(Product product)
        {
            Data._Products.Add(product);
            Data.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var prodToRemove = Data._Products.FirstOrDefault(s => s.Id == id);
            if (prodToRemove.Id != Guid.Empty)
            {
                Data._Products.Remove(prodToRemove);
                Data.SaveChanges();
                return true;
            }
            return false;
        }

        public void Update(Product item, int numOfBooks)
        {
            item.Quantity = numOfBooks;
            Data.SaveChanges();

            if (item.Quantity == 0)
            {
                var productRepository = new ProductRepository();
                productRepository.Delete(item.Id);
                Data.SaveChanges();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return Data._Products.ToList();
        }

        public Product Get(Guid id)
        {
            var selectedProduct = Data._Products.Where(product => product.Id == id);

            return (Product)selectedProduct;
        }
    }
}

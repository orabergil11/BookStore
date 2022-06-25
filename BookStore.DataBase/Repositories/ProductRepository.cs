// ------------------------------------------------------------------------------------------------------ //
//                                                                                                        //
// @File      ProductRepository.cs                                                                        //
// @Details   The Layer that links between the data-base and the server                                   //
// @Author    Or Abergil                                                                                  //
// @Since     15/03/2022                                                                                  //
//                                                                                                        //
// ------------------------------------------------------------------------------------------------------ //

using System.Collections.Generic;
using System.Linq;
using System;

using BookStore.DataBase.Interfaces;
using BookStore.Models;

namespace BookStore.DataBase
{
    public class ProductRepository : IRepository<Product>
    {
        public ProductDataContext Data { get; set; }
        public ProductRepository()
        {
            Data = new ProductDataContext();
        }

        //CRUD functions
        public void Add(Product product)
        {
            Data._Products.Add(product);
            Data.SaveChanges();
        }
        public Product Get(Guid id)
        {
            var selectedProduct = Data._Products.FirstOrDefault(product => product.Id == id);
            return selectedProduct;
        }

        public Product Get(string title)
        {
            var selectedProduct = Data._Products.FirstOrDefault((product => product.Description == title));
            return selectedProduct;
        }

        public IEnumerable<Product> GetAll()
        {
            return Data._Products.ToList();
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

        public bool Delete(Guid id)
        {
            var prodToRemove = Data._Products.FirstOrDefault(s => s.Id == id);

            if ((prodToRemove    == null) ||
                (prodToRemove.Id == Guid.Empty))
            {
                 return false;
            }

            Data._Products.Remove(prodToRemove);
            Data.SaveChanges();
            return true;
        }
    }
}

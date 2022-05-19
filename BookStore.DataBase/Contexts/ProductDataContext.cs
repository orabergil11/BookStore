using BookStore.Models;
using System.Data.Entity;

namespace BookStore.DataBase
{
    public  class ProductDataContext : DbContext
    {
        public virtual DbSet<Product> _Products { get; set; }
        public ProductDataContext() : base ("BookStoreDb")
        {
                
        }
    }
}

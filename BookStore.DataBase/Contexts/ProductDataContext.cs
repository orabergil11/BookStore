// ------------------------------------------------------------------------------------------------------ //
//                                                                                                        //
// @File      ProductDataContext.cs                                                                       //
// @Details   Contains the collection that connected to the Sql-Server Database                           //
// @Author    Or Abergil                                                                                  //
// @Since     15/03/2022                                                                                  //
//                                                                                                        //
// ------------------------------------------------------------------------------------------------------ //

using BookStore.Models;
using System.Data.Entity;

namespace BookStore.DataBase
{
    public  class ProductDataContext : DbContext
    {
        public virtual DbSet<Product> _Products { get; set; }
        public ProductDataContext() : base ("BookStoreDb") { }
    }
}

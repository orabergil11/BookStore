using BookStore.DataBase.Interfaces;
using BookStore.Models;

namespace BookStore.DataBase
{
    public class RepositoryFactory
    {
        public static IRepository<Product> GetProductRepository()
        {
            return FileSystemDataContext.Instance;
        }
    }
}

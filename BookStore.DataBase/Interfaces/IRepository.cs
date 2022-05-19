using BookStore.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace BookStore.DataBase.Interfaces
{
    public interface IRepository<T>
    {
        bool Delete(Guid id);

        void Update(T item, int amount);

        void Add(T id);

        T Get(Guid id);

        IEnumerable<T> GetAll();
    }
}

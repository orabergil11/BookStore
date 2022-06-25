// ------------------------------------------------------------------------------------------------------ //
//                                                                                                        //
// @File      IRepository.cs                                                                              //
// @Details   CRUD functions implemented in the repository class                                          //
// @Author    Or Abergil                                                                                  //
// @Since     15/03/2022                                                                                  //
//                                                                                                        //
// ------------------------------------------------------------------------------------------------------ //

using System.Collections.Generic;
using System;

namespace BookStore.DataBase.Interfaces
{
    public interface IRepository<T>
    {
        void Update(T item, int amount);
        IEnumerable<T> GetAll();
        bool Delete(Guid id);
        T Get(string title);
        T Get(Guid id);
        void Add(T id);
    }
}

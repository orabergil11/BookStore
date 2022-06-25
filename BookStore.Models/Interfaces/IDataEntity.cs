// ------------------------------------------------------------------------------------------------------ //
//                                                                                                        //
// @File      IDataEntity.cs                                                                              //
// @Details   a contract between all the classes that inherits from the abstract class 'product' must     //
// implement.                                                                                             //
// @Author    Or Abergil                                                                                  //
// @Since     15/03/2022                                                                                  //
//                                                                                                        //
// ------------------------------------------------------------------------------------------------------ //

using System;

namespace BookStore.Models.Interfaces
{
    public interface IDataEntity
    {
        Guid Id { get; }
    }
}

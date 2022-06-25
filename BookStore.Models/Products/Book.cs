// ------------------------------------------------------------------------------------------------------ //
//                                                                                                        //
// @File      Book.cs                                                                                     //
// @Details   represents the different propertis a book contains                                          //
// @Author    Or Abergil                                                                                  //
// @Since     15/03/2022                                                                                  //
//                                                                                                        //
// ------------------------------------------------------------------------------------------------------ //

using BookStore.Models.Enums;
using System;

namespace BookStore.Models
{
    public class Book : Product
    {
        // Properties
        public string Title
        {
            get { return base.Description; }
            set { base.Description = value; }
        }

        public string AuthorName     { get; set; }
        public int Edition           { get; set; }
        public string Synopsis       { get; set; }
        public BookGenre? BookGenre  { get; set; }

        public Book(string authorName, string title, int quantity, DateTime publicationDate,
            decimal basePrice, BookGenre genres, int eddition = 1)
            : base(title, quantity, publicationDate, basePrice)
        {
            this.AuthorName = authorName;
            this.Edition    = eddition;
            this.BookGenre  = genres;
            this.Quantity   = quantity;
        }
        public Book() { }
        public override bool IsValid()
        {
            if(!base.IsValid())          return false;
            if (this.Edition <= 0)       return false;
            if (this.BookGenre == null)  return false;
            else return true;
        }
    }
    
}

using BookStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models
{
    public class Book : Product
    {
        public string Title
        {
            get { return base.Description; }
            set { base.Description = value; }
        }
        public string AuthorName { get; set; }
        public int Edition { get; set; }
        public string Synopsis { get; set; }
        public BookGenre? BookGenre { get; set; }
        // ctor
        public Book(string authorName, string title, int quantity, DateTime publicationDate,
            decimal basePrice, BookGenre genres, int eddition = 1)
            : base(title, quantity, publicationDate, basePrice)
        {
            this.AuthorName = authorName;
            this.Edition = eddition;
            this.BookGenre = genres;
            this.Quantity = quantity;
        }
        public Book() { }
    }
}

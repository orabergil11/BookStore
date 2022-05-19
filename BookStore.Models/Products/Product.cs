using BookStore.Models.Interfaces;
using System;

namespace BookStore.Models
{
    public abstract class Product : ICloneable, IDataEntity
    {
        public Guid Id { get;  set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal BasePrice { get; set; }
        //ctor
        protected Product (string discription,int quantity, DateTime publicationDate, decimal basePrice)
            : this(Guid.NewGuid(), discription,quantity, publicationDate, basePrice) { }
        //ctor
        public Product() { }
        public Product (Guid id, string discription,int quantity, DateTime publicationDate, decimal basePrice)
        {
            this.Id = id;
            this.Description = discription;
            this.PublicationDate = publicationDate;
            this.BasePrice = basePrice;
            this.Quantity = quantity;
        }
        public object Clone() => this.MemberwiseClone();
        public double GetTotalPrice() => (double)(Quantity * BasePrice);
    }
}

// ------------------------------------------------------------------------------------------------------ //
//                                                                                                        //
// @File      Product.cs                                                                                  //
// @Details   represents the different propertis a product contains                                       //
// @Author    Or Abergil                                                                                  //
// @Since     15/03/2022                                                                                  //
//                                                                                                        //
// ------------------------------------------------------------------------------------------------------ //

using BookStore.Models.Interfaces;
using System;

namespace BookStore.Models
{
    public abstract class Product : ICloneable, IDataEntity
    {
        //Propertis
        public Guid Id                  { get;  set; }
        public string Description       { get; set; }
        public int Quantity             { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal BasePrice        { get; set; }

        protected Product (string discription,int quantity, DateTime publicationDate, decimal basePrice)
            : this(Guid.NewGuid(), discription,quantity, publicationDate, basePrice) { }

        public Product() { }
        public Product (Guid id, string discription,int quantity, DateTime publicationDate, decimal basePrice)
        {
            this.Id              = id;
            this.Description     = discription;
            this.PublicationDate = publicationDate;
            this.BasePrice       = basePrice;
            this.Quantity        = quantity;
        }

        public object Clone()         => this.MemberwiseClone();
        public double GetTotalPrice() => (double)(Quantity * BasePrice);

        public virtual bool IsValid()
        {
            if (this.Quantity <= 0)          return false;
            if (this.BasePrice <= 0)         return false;
            if (this.Description == default) return false;
            return true;
        }
    }
}

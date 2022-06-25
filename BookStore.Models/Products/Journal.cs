// ------------------------------------------------------------------------------------------------------ //
//                                                                                                        //
// @File      Journal.cs                                                                                  //
// @Details   represents the different propertis a journal contains                                       //
// @Author    Or Abergil                                                                                  //
// @Since     15/03/2022                                                                                  //
//                                                                                                        //
// ------------------------------------------------------------------------------------------------------ //

using BookStore.Models.Enums;
using System;

namespace BookStore.Models
{
    public class Journal : Product
    {
        public string Name
        {
            get { return base.Description; }
            set { base.Description = value; }
        }
        public string EditorName             { get; set; }
        public int IssueNumber               { get; set; }
        public JournalGenre? JournalGenre    { get; set; }
        public JournalFrequency Frequency    { get; set; }

        public Journal(string editorName, string name,  int issueNumber, int quantity, DateTime publicationDate,
            decimal basePrice, JournalFrequency frequency, JournalGenre genres)
            : base(name, quantity, publicationDate, basePrice)
        {
            this.EditorName   = editorName;
            this.IssueNumber  = issueNumber;
            this.Frequency    = frequency;
            this.JournalGenre = genres;
            this.Quantity     = quantity;
        }
        public Journal() { }

        public override bool IsValid()
        {
            if (!base.IsValid())            return false;
            if (this.IssueNumber <= 0)      return false;
            if (this.EditorName == default) return false;
            else return true;
        }
    }
}


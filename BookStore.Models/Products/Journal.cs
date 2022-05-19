using BookStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models
{
    public class Journal : Product
    {
        public string EditorName { get; set; }
        public string Name
        {
            get { return base.Description; }
            set { base.Description = value; }
        }
        public int IssueNumber { get; set; }
        public JournalGenre? JournalGenre { get; set; }
        public JournalFrequency Frequency { get; set; }
        public Journal(string editorName, string name,  int issueNumber, int quantity, DateTime publicationDate,
            decimal basePrice, JournalFrequency frequency, JournalGenre genres)
            : base(name, quantity, publicationDate, basePrice)
        {
            this.EditorName = editorName;
            this.IssueNumber = issueNumber;
            this.Frequency = frequency;
            this.JournalGenre = genres;
            this.Quantity = quantity;
        }
        public Journal() { }
    }
}


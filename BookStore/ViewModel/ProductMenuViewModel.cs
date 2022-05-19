using BookStore.Models;
using BookStore.Models.Enums;
using BookStore.Server;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookStore.ViewModel
{
    public class ProductMenuViewModel : ViewModelBase
    {
        // fields
        private string selectedType;
        private string selectedGenre;
        private string description;
        private string authorOrEditor;
        private string productDetails;
        private int editionOrIssueNumber;
        private int quantity;
        private decimal price;
        private Visibility bookVisibility;
        private Visibility journalVisibility;
        private DateTime publicationDate;
        private Product selectedProduct;

        //collections
        public ObservableCollection<Product> ProductDb
        {
            get { productDb = new ObservableCollection<Product>(ProductService.Instance.ListToOcProdductDb()); return productDb; }
            set
            {

                Set(ref productDb, value);
            }
        }
        private ObservableCollection<Product> showList;

        public ObservableCollection<Product> ShowList
        {
            get { return showList; }
            set
            {
                Set(ref showList, value);
            }
        }

        private ObservableCollection<Product> productDb;
        public ObservableCollection<ProductTypes> ProductType { get; set; }
        public ObservableCollection<string> ProductGenre { get; set; }
        // Prop
        public string ProductDetails { get => productDetails; set => Set(ref productDetails, value); }
        public string Description { get => description; set => Set(ref description, value); }
        public decimal Price { get => price; set => Set(ref price, value); }
        public string AuthorOrEditor { get => authorOrEditor; set => Set(ref authorOrEditor, value); }
        public int EditionOrIssueNumber { get => editionOrIssueNumber; set => Set(ref editionOrIssueNumber, value); }
        public int Quantity { get => quantity; set => Set(ref quantity, value); }
        public DateTime PublicationDate { get => publicationDate; set => Set(ref publicationDate, value); }
        public Visibility BookVisibility { get => bookVisibility; set => Set(ref bookVisibility, value); }
        public Visibility JournalVisibility { get => journalVisibility; set => Set(ref journalVisibility, value); }
        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                SelectedProduct = null;
                Set(ref selectedType, value);
                //clean the cb
                ProductGenre.Clear();
                
                JournalOrBook(true);
            }
        }

        public string SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                Set(ref selectedGenre, value);
                JournalOrBook(false);
                ShowRelevantGenres();
            }
        }

        public Product SelectedProduct
        {
            get => selectedProduct; set
            {
                Set(ref selectedProduct, value);
               
                UpdateProductDetails();
            }
        }
        // ctor
        public ProductMenuViewModel()
        {
            this.ProductType = new ObservableCollection<ProductTypes>(Enum.GetValues(typeof(ProductTypes)).Cast<ProductTypes>());
            this.ProductGenre = new ObservableCollection<string>();
            this.ProductDb = new ObservableCollection<Product>();
            this.ShowList = new ObservableCollection<Product>();
            this.ShowList = ProductDb;
            this.productDb = new ObservableCollection<Product>(ProductService.Instance.ProductDB);
            this.BookVisibility = Visibility.Collapsed;
            this.JournalVisibility = Visibility.Collapsed;
        }
        // func
        private void UpdateProductDetails()
        {
            if (SelectedProduct == null) return;
            if (SelectedProduct.GetType() == typeof(Book))
            {
                if (JournalVisibility == Visibility.Visible)
                {
                    JournalVisibility = Visibility.Collapsed;
                }
                else
                {
                    Book book = (Book)SelectedProduct;
                    Description = book.Description;
                    Price = book.BasePrice;
                    AuthorOrEditor = book.AuthorName;
                    Quantity = book.Quantity;
                    PublicationDate = book.PublicationDate;
                    EditionOrIssueNumber = book.Edition;
                }
                BookVisibility = Visibility.Visible;
            }
            else if (SelectedProduct.GetType() == typeof(Journal))
            {
                if (BookVisibility == Visibility.Visible)
                {
                    BookVisibility = Visibility.Collapsed;
                }
                else
                {
                    Journal journal = (Journal)SelectedProduct;
                    Description = journal.Description;
                    Price = journal.BasePrice;
                    AuthorOrEditor = journal.EditorName;
                    Quantity = journal.Quantity;
                    PublicationDate = journal.PublicationDate;
                }
                JournalVisibility = Visibility.Visible;
            }
        }
        private void JournalOrBook(bool ifGenresNeedToBeAdded)
        {
            if (selectedType == "Book")
            {
                if (ifGenresNeedToBeAdded == true)
                {
                    ProductService.Instance.AddBookGenreToOc(ProductGenre);
                }
                ShowList.Clear();
                foreach (var product in ProductDb)
                {
                    if (product.GetType() == typeof(Book))
                    {
                        ShowList.Add(product);
                    }
                }
            }

            else if (selectedType == "Journal")
            {
                if (ifGenresNeedToBeAdded == true)
                {
                    ProductService.Instance.AddJournalGenreToOc(ProductGenre);
                    //ProductGenre = new ObservableCollection<JournalGenre>(Enum.GetValues(typeof(JournalGenre)).Cast<JournalGenre>().ToList());

                }
                ShowList.Clear();
                foreach (var product in ProductDb)
                {
                    if (product.GetType() == typeof(Journal))
                    {
                        ShowList.Add(product);
                    }
                }
            }
        }
        private void ShowRelevantGenres()
        {
            if (SelectedType == "Book")
            {
                for (int i = 0; i < ShowList.Count; i++)
                {
                    Book b = (Book)ShowList[i];
                    if (b.Genres.Contains(convertBookGenreStringToEnum(SelectedGenre)) == false)
                    {
                        ShowList.RemoveAt(i);
                        i--;
                    }
                }
            }

            else if (SelectedType == "Journal")
            {
                for (int i = 0; i < ShowList.Count; i++)
                {
                    Journal b = (Journal)ShowList[i];
                    if (b.Genres.Contains(convertJournalGenreStringToEnum(SelectedGenre)) == false)
                    {
                        ShowList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
        private BookGenre convertBookGenreStringToEnum(string stringToConvert)
        {
            switch (stringToConvert)
            {
                case "Drama":
                    return BookGenre.Drama;
                case "Action":
                    return BookGenre.Action;
                case "Romance":
                    return BookGenre.Romance;
                case "Fiction":
                    return BookGenre.Fiction;
            }
            return BookGenre.All;
        }

        private JournalGenre convertJournalGenreStringToEnum(string stringToConvert)
        {
            switch (stringToConvert)
            {
                case "Sience":
                    return JournalGenre.Sience;
                case "Law":
                    return JournalGenre.Law;
                case "Medicine":
                    return JournalGenre.Medicine;
            }
            return JournalGenre.All;
        }
    }
}

using BookStore.Models;
using BookStore.Models.Enums;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookStore.ViewModel
{
    public class ProductMenuViewModel : ViewModelBase
    {
        // fields
        private string description;
        private string authorOrEditor;
        private string productDetails;
        private int editionOrIssueNumber;
        private int quantity;
        private int amountOfProducts;
        private decimal price;
        private ProductTypes selectedType;
        private Visibility bookVisibility;
        private Visibility journalVisibility;
        private DateTime publicationDate;
        private Enum selectedGenre;
        private Product selectedProduct;


        //collections
        public ObservableCollection<Product> ProductDb
        {
            get {  return productDb; }
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
        private ObservableCollection<BookGenre> tempBookGenre;
        private ObservableCollection<JournalGenre> tempJournalGenre;

        public ObservableCollection<ProductTypes> ProductType { get; set; }
        public ObservableCollection<dynamic> ProductGenre { get; set; }
        // Prop
        public string ProductDetails { get => productDetails; set => Set(ref productDetails, value); }
        public string Description { get => description; set => Set(ref description, value); }
        public decimal Price { get => price; set => Set(ref price, value); }
        public string AuthorOrEditor { get => authorOrEditor; set => Set(ref authorOrEditor, value); }
        public int EditionOrIssueNumber { get => editionOrIssueNumber; set => Set(ref editionOrIssueNumber, value); }
        public int Quantity { get => quantity; set => Set(ref quantity, value); }
        public int AmountOfProducts { get => amountOfProducts; set => Set(ref amountOfProducts, value); }
        public DateTime PublicationDate { get => publicationDate; set => Set(ref publicationDate, value); }
        public Visibility BookVisibility { get => bookVisibility; set => Set(ref bookVisibility, value); }
        public Visibility JournalVisibility { get => journalVisibility; set => Set(ref journalVisibility, value); }
        public RelayCommand CartCommand { get; set; }

        public ObservableCollection<BookGenre> TempBookGenre { get => tempBookGenre; set => Set(ref tempBookGenre, value); }
        public ObservableCollection<JournalGenre> TempJournalGenre { get => tempJournalGenre; set => Set(ref tempJournalGenre, value); }

        public ProductTypes SelectedType
        {
            get { return selectedType; }
            set
            {
                SelectedProduct = null;
                Set(ref selectedType, value);
                //clean the cb
                ProductGenre.Clear();
                
                FillProductsInComboBox(true);
            }
        }

        public Enum SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                Set(ref selectedGenre, value);
                FillProductsInComboBox(false);
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
            InitObservableCollections();
            this.BookVisibility = Visibility.Collapsed;
            this.JournalVisibility = Visibility.Collapsed;
            CartCommand = new RelayCommand(AddProductToCart);
            MessengerInstance.Register<bool>(this, "custView", InitList);
        }

        private void InitObservableCollections()
        {
            this.ProductType = new ObservableCollection<ProductTypes>(Enum.GetValues(typeof(ProductTypes)).Cast<ProductTypes>());
            this.ProductGenre = new ObservableCollection<dynamic>();
            this.ProductDb = new ObservableCollection<Product>();
            this.ShowList = ProductDb;
            this.productDb = new ObservableCollection<Product>(ProductService.Instance.ProductDB);
            TempBookGenre = new ObservableCollection<BookGenre>(Enum.GetValues(typeof(BookGenre)).Cast<BookGenre>());
            TempJournalGenre = new ObservableCollection<JournalGenre>(Enum.GetValues(typeof(JournalGenre)).Cast<JournalGenre>());
        }

        private void InitList(bool obj)
        {
            this.productDb = new ObservableCollection<Product>(ProductService.Instance.ProductDB);
        }

        private void AddProductToCart()
        {
                ProductService.Instance.AddProductsToCart(SelectedProduct,ShoppingCart.Instance.ProductsInCart,AmountOfProducts);
        }
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
        private void FillProductsInComboBox(bool ifGenresNeedToBeAdded)
        {
            if (selectedType == ProductTypes.Book)
            {
                if (ifGenresNeedToBeAdded == true)
                {
                    foreach (var item in Enum.GetValues(typeof(BookGenre)))
                    {
                        this.ProductGenre.Add(item);
                    }
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

            else if (selectedType == ProductTypes.Journal)
            {
                if (ifGenresNeedToBeAdded == true)
                {
                    foreach (var item in Enum.GetValues(typeof(JournalGenre)))
                    {
                        this.ProductGenre.Add(item);
                    }
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
            if (SelectedType == ProductTypes.Book)
            {
                for (int i = 0; i < ShowList.Count; i++)
                {
                    Book b = (Book)ShowList[i];
                    if (SelectedGenre == null) return;
                    if (b.Genres.Contains((BookGenre)SelectedGenre) == false)
                    {
                        ShowList.RemoveAt(i);
                        i--;
                    }
                }
            }

            else if (SelectedType == ProductTypes.Journal)
            {
                for (int i = 0; i < ShowList.Count; i++)
                {
                    Journal b = (Journal)ShowList[i];
                    if (SelectedGenre == null) return;
                    if (b.Genres.Contains((JournalGenre)SelectedGenre) == false)
                    {
                        ShowList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
        
    }
}

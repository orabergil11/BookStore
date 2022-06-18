// ----------------------------------------------------------------------------------------------------- //
//                                                                                                       //
// @File      ProductMenuViewModel.cs                                                                    //
// @Details   Responsible on the products collection and 'adding to cart' feature in the client side     //
// @Author    Or Abergil                                                                                 //
// @Since     15/03/2022                                                                                 //
//                                                                                                       //
// ----------------------------------------------------------------------------------------------------- //

using BookStore.Models.Enums;
using BookStore.Models;
using BookStore.Server;

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;

using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows;
using System.Linq;
using System;

namespace BookStore.ViewModel
{
    public class ProductMenuViewModel : ViewModelBase
    {
        // fields of the selected product
        private string   authorOrEditor;
        private string   description;
        private decimal  price;
        private int      editionOrIssueNumber;
        private int      requestedProductsToBuy;
        private int      totalQuantityInStock;
        private ProductTypes selectedType;
        private Product      selectedProduct;
        private Enum         selectedGenre;
        private DateTime     publicationDate;

        // views
        private Visibility bookVisibility;
        private Visibility journalVisibility;

        // Notifications fields
        private Visibility addedToCart_Notification;
        private Visibility notAddedToCart_Notification;
        private DispatcherTimer dispatcherTimer;

        // Observable collections
        private ObservableCollection<Product> productDb;
        private ObservableCollection<dynamic> productGenre;
        private ObservableCollection<ProductTypes> productsType;
        private ObservableCollection<Product> showList;

        // flags
        private bool isProductAdded;

        // consts
        public const int NOTIFICATION_TIME_HOUR = 0;
        public const int NOTIFICATION_TIME_MIN  = 0;
        public const int NOTIFICATION_TIME_SEC  = 3;

        #region ObservableCollections
        public ObservableCollection<Product> ProductDb          { get => productDb;     set => Set(ref productDb,    value); }
        public ObservableCollection<Product> ShowList           { get => showList;      set => Set(ref showList,     value); }
        public ObservableCollection<ProductTypes> ProductsType  { get => productsType;  set => Set(ref productsType, value); }
        public ObservableCollection<dynamic> ProductGenre       { get => productGenre;  set => Set(ref productGenre, value); }
        #endregion

        #region FullProperties
        public string Description { get => description; set => Set(ref description, value); }
        public decimal Price { get => price; set => Set(ref price, value); }
        public string AuthorOrEditor { get => authorOrEditor; set => Set(ref authorOrEditor, value); }
        public int EditionOrIssueNumber { get => editionOrIssueNumber; set => Set(ref editionOrIssueNumber, value); }
        public int TotalQuantityInStock { get => totalQuantityInStock; set => Set(ref totalQuantityInStock, value); }
        public int RequestedProductsToBuy { get => requestedProductsToBuy; set => Set(ref requestedProductsToBuy, value); }
        public DateTime PublicationDate { get => publicationDate; set => Set(ref publicationDate, value); }
        public Visibility BookVisibility { get => bookVisibility; set => Set(ref bookVisibility, value); }
        public Visibility JournalVisibility { get => journalVisibility; set => Set(ref journalVisibility, value); }
        public Visibility AddedToCart_Notification { get => addedToCart_Notification; set => Set(ref addedToCart_Notification, value); }
        public Visibility NotAddedToCart_Notification { get => notAddedToCart_Notification; set => Set(ref notAddedToCart_Notification, value); }
        public RelayCommand CartCommand { get; set; }

        public ProductTypes SelectedType
        {
            get { return selectedType; }
            set
            {
                SelectedProduct = null;
                Set(ref selectedType, value);
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
                ShowSelectedProductType();
            }
        }
        public Product SelectedProduct
        {
            get => selectedProduct; set
            {
                Set(ref selectedProduct, value);
                UpdateProductDetailsOnDisplay();
            }
        }
        #endregion

        #region Ctor
        public ProductMenuViewModel()
        {
            InitObservableCollections();
            InitVisibillities();
            this.CartCommand = new RelayCommand(AddProductToCart);
            MessengerInstance.Register<bool>(this, "RefreshProductMenuView", ResetProductMenuDisplay);
            dispatcherTimer = new DispatcherTimer();
        }

        #region methods
        private void ResetProductMenuDisplay(bool obj)
        {
            SelectedType = default;
            SetProductsViewsOff();
            this.productDb = new ObservableCollection<Product>(ProductService.Instance.Products.GetAll());
            FillProductsInShowList();
        }

        private void InitObservableCollections()
        {
            this.ProductsType   = new ObservableCollection<ProductTypes>(Enum.GetValues(typeof(ProductTypes)).Cast<ProductTypes>());
            this.ProductGenre   = new ObservableCollection<dynamic>();
            this.ProductDb      = new ObservableCollection<Product>(ProductService.Instance.Products.GetAll());
            this.ShowList       = ProductDb;
        }

        private void InitVisibillities()
        {
            SetProductsViewsOff();
            SetNoteficationsOff();
        }

        private void SetNoteficationsOff()
        {
            this.AddedToCart_Notification    = Visibility.Collapsed;
            this.NotAddedToCart_Notification = Visibility.Collapsed;
        }

        private void InitTimer()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);

            dispatcherTimer.Interval = new TimeSpan(NOTIFICATION_TIME_HOUR,
                                                    NOTIFICATION_TIME_MIN,
                                                    NOTIFICATION_TIME_SEC);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (isProductAdded)
            {
                this.AddedToCart_Notification = Visibility.Collapsed;
                dispatcherTimer.Stop();
            }
            else
            {
                NotAddedToCart_Notification = Visibility.Collapsed;
            }
        }

        #endregion
        private void SetProductsViewsOff()
        {
            this.BookVisibility    = Visibility.Collapsed;
            this.JournalVisibility = Visibility.Collapsed;
        }

        private void AddProductToCart()
        {
            if (ProductService.Instance.AddProductsToCart(SelectedProduct, RequestedProductsToBuy))
            {
                RequestedProductsToBuy   = 0;
                AddedToCart_Notification = Visibility.Visible;
                InitTimer();
            }
            else
            {
                NotAddedToCart_Notification = Visibility.Visible;
                InitTimer();
            }
        }

        private void UpdateProductDetailsOnDisplay()
        {
            if (SelectedProduct == null) return;

            if (SelectedProduct.GetType() == typeof(Book))
            {
                if (JournalVisibility == Visibility.Visible)
                {
                    JournalVisibility =  Visibility.Collapsed;
                }

                Book book                 = (Book)SelectedProduct;
                this.Description          = book.Description;
                this.Price                = book.BasePrice;
                this.AuthorOrEditor       = book.AuthorName;
                this.TotalQuantityInStock = book.Quantity;
                this.PublicationDate      = book.PublicationDate;
                this.EditionOrIssueNumber = book.Edition;

                BookVisibility = Visibility.Visible;
            }

            else if (SelectedProduct.GetType() == typeof(Journal))
            {
                if (BookVisibility == Visibility.Visible)
                {
                    BookVisibility = Visibility.Collapsed;
                }

                Journal journal         = (Journal)SelectedProduct;
                Description             = journal.Description;
                Price                   = journal.BasePrice;
                AuthorOrEditor          = journal.EditorName;
                TotalQuantityInStock    = journal.Quantity;
                PublicationDate         = journal.PublicationDate;

                JournalVisibility = Visibility.Visible;
            }
        }

        private void FillProductsInComboBox(bool ifGenresNeedToBeAdded)
        {
            if (selectedType == ProductTypes.Book)
            {
                if (ifGenresNeedToBeAdded)
                {
                    foreach (var genre in Enum.GetValues(typeof(BookGenre)))
                    {
                        this.ProductGenre.Add(genre);
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
                if (ifGenresNeedToBeAdded)
                {
                    foreach (var genre in Enum.GetValues(typeof(JournalGenre)))
                    {
                        this.ProductGenre.Add(genre);
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

        private void ShowSelectedProductType()
        {
            if (SelectedType == ProductTypes.Book)
            {
                // pass over all books and delete the irrelvant genres
                for (int product = 0; product < ShowList.Count; product++)
                {
                    Book productAsBook = (Book)ShowList[product];

                    if (SelectedGenre == null) return;

                    if (productAsBook.BookGenre != ((BookGenre)SelectedGenre))
                    {
                        ShowList.RemoveAt(product);
                        product--;
                    }
                }
            }

            else if (SelectedType == ProductTypes.Journal)
            {
                // pass over all journals and delete the irrelvant genres
                for (int product = 0; product < ShowList.Count; product++)
                {
                    Journal productAsJournal = (Journal)ShowList[product];

                    if (SelectedGenre == null) return;

                    if (productAsJournal.JournalGenre != ((JournalGenre)SelectedGenre))
                    {
                        ShowList.RemoveAt(product);
                        product--;
                    }
                }
            }
        }

        private void FillProductsInShowList()
        {
            ShowList.Clear();
            foreach (var product in ProductDb)
            {
                ShowList.Add(product);
            }
        }
    }
}
#endregion

using BookStore.Models;
using BookStore.Models.Enums;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace BookStore.ViewModel
{
    public class ProductMenuViewModel : ViewModelBase
    {
        private string description;
        private string authorOrEditor;

        private int editionOrIssueNumber;
        private int quantity;
        private int amountOfProducts;
        private decimal price;
        private ProductTypes selectedType;
        private Visibility bookVisibility;
        private Visibility journalVisibility;
        private Visibility addedToCart;
        private Visibility notAddedToCartNotefication;
        private DateTime publicationDate;
        private Enum selectedGenre;
        private Product selectedProduct;
        private ObservableCollection<Product> productDb;
        private ObservableCollection<dynamic> productGenre;
        private ObservableCollection<ProductTypes> productsType;
        private ObservableCollection<Product> showList;
        private DispatcherTimer dispatcherTimer;
        private bool isSuccessfullyAdded;



        #region ObservableCollections
        public ObservableCollection<Product> ProductDb { get => productDb; set => Set(ref productDb, value); }
        public ObservableCollection<Product> ShowList { get => showList; set => Set(ref showList, value); }
        public ObservableCollection<ProductTypes> ProductsType { get => productsType; set => Set(ref productsType, value); }
        public ObservableCollection<dynamic> ProductGenre { get => productGenre; set => Set(ref productGenre, value); }
        #endregion

        #region FullProperties
        public string Description { get => description; set => Set(ref description, value); }
        public decimal Price { get => price; set => Set(ref price, value); }
        public string AuthorOrEditor { get => authorOrEditor; set => Set(ref authorOrEditor, value); }
        public int EditionOrIssueNumber { get => editionOrIssueNumber; set => Set(ref editionOrIssueNumber, value); }
        public int Quantity { get => quantity; set => Set(ref quantity, value); }
        public int AmountOfProducts { get => amountOfProducts; set => Set(ref amountOfProducts, value); }
        public DateTime PublicationDate { get => publicationDate; set => Set(ref publicationDate, value); }
        public Visibility BookVisibility { get => bookVisibility; set => Set(ref bookVisibility, value); }
        public Visibility JournalVisibility { get => journalVisibility; set => Set(ref journalVisibility, value); }
        public Visibility AddedToCartNotefication { get => addedToCart; set => Set(ref addedToCart, value); }
        public Visibility NotAddedToCartNotefication { get => notAddedToCartNotefication; set => Set(ref notAddedToCartNotefication, value); }
        public RelayCommand CartCommand { get; set; }
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
            AddRelevantProducts();
        }
        private void InitObservableCollections()
        {
            this.ProductsType = new ObservableCollection<ProductTypes>(Enum.GetValues(typeof(ProductTypes)).Cast<ProductTypes>());
            this.ProductGenre = new ObservableCollection<dynamic>();
            this.ProductDb = new ObservableCollection<Product>(ProductService.Instance.Products.GetAll());
            this.ShowList = ProductDb;
        }

        private void InitVisibillities()
        {
            SetProductsViewsOff();
            SetNoteficationsOff();
        }
        private void SetNoteficationsOff()
        {
            this.AddedToCartNotefication = Visibility.Collapsed;
            this.NotAddedToCartNotefication = Visibility.Collapsed;
        }

        private void InitTimer()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (isSuccessfullyAdded)
            {
                this.AddedToCartNotefication = Visibility.Collapsed;
                dispatcherTimer.Stop();
            }
            else
            {
                NotAddedToCartNotefication = Visibility.Collapsed;
            }
        }
        #endregion

        private void SetProductsViewsOff()
        {
            this.BookVisibility = Visibility.Collapsed;
            this.JournalVisibility = Visibility.Collapsed;
        }

        private void AddProductToCart()
        {
            isSuccessfullyAdded = ProductService.Instance.AddProductsToCart(SelectedProduct, AmountOfProducts);
            if (isSuccessfullyAdded)
            {
                AmountOfProducts = 0;
                AddedToCartNotefication = Visibility.Visible;
                InitTimer();
            }
            else
            {
                NotAddedToCartNotefication = Visibility.Visible;
                InitTimer();
            }
        }

        private void UpdateProductDetailsOnDisplay()
        {
            if (SelectedProduct == null) return;
            if (SelectedProduct.GetType() == typeof(Book))
            {
                if (JournalVisibility == Visibility.Visible)
                    JournalVisibility = Visibility.Collapsed;

                Book book = (Book)SelectedProduct;
                this.Description = book.Description;
                this.Price = book.BasePrice;
                this.AuthorOrEditor = book.AuthorName;
                this.Quantity = book.Quantity;
                this.PublicationDate = book.PublicationDate;
                this.EditionOrIssueNumber = book.Edition;

                BookVisibility = Visibility.Visible;
            }

            else if (SelectedProduct.GetType() == typeof(Journal))
            {
                if (BookVisibility == Visibility.Visible)
                    BookVisibility = Visibility.Collapsed;

                Journal journal = (Journal)SelectedProduct;
                Description = journal.Description;
                Price = journal.BasePrice;
                AuthorOrEditor = journal.EditorName;
                Quantity = journal.Quantity;
                PublicationDate = journal.PublicationDate;

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

        private void ShowSelectedProductType()
        {
            if (SelectedType == ProductTypes.Book)
            {
                for (int i = 0; i < ShowList.Count; i++)
                {
                    Book b = (Book)ShowList[i];
                    if (SelectedGenre == null) return;
                    if (b.BookGenre == ((BookGenre)SelectedGenre) == false)
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
                    Journal j = (Journal)ShowList[i];

                    if (SelectedGenre == null) return;
                    if (j.JournalGenre == ((JournalGenre)SelectedGenre) == false)
                    {
                        ShowList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void AddRelevantProducts()
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

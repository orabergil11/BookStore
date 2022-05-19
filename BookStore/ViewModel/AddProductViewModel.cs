using BookStore.Models.Enums;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace BookStore.ViewModel
{
    public class AddProductViewModel : ViewModelBase
    {
        // fields
        private string authorName;
        private string title;
        private ProductTypes selectedType;
        private int eddition;
        private int quantity;
        private decimal price;
        private JournalFrequency selectedJournalFreq;
        private JournalGenre selectedJournalGenre;
        private Visibility addJournalVisibillity;
        private Visibility addBookVisibillity;
        private BookGenre selectedBookGenre;
        private DateTime dateTimeSelection;

        // collections
        public ObservableCollection<ProductTypes> ProductTypesOc { get; set; }
        //public ObservableCollection<BookGenre> ProductGenreOc { get; set; }
        public ObservableCollection<JournalFrequency> JournalFreqOc { get; set; }
        public ObservableCollection<string> ProductGenreOc{ get; set; }


        // button
        public RelayCommand AddProdCommand { get; set; }

        // prop
        public string AuthorName { get => authorName; set => Set(ref authorName, value); }
        public string Title { get => title; set => Set(ref title, value); }
        public int Quantity { get => quantity; set => Set(ref quantity, value); }
        public int Eddition { get => eddition; set => Set(ref eddition, value); }
        public decimal Price { get => price; set => Set(ref price, value); }
        public JournalGenre SelectedJournalGenre { get => selectedJournalGenre; set => Set(ref selectedJournalGenre, value); }
        public JournalFrequency SelectedJournalFreq { get => selectedJournalFreq; set => Set(ref selectedJournalFreq, value); }
        public Visibility AddJournalVisibillity { get => addJournalVisibillity; set => Set(ref addJournalVisibillity, value); }
        public Visibility AddBookVisibillity { get => addBookVisibillity; set => Set(ref addBookVisibillity, value); }
        public DateTime DateTimeSelection { get => dateTimeSelection; set => Set(ref dateTimeSelection, value); }
        public BookGenre SelectedBookGenre { get => selectedBookGenre; set => Set(ref selectedBookGenre, value); }
        public ProductTypes SelectedType
        {
            get { return selectedType; }
            set
            {
                Set(ref selectedType, value);
                ShowRelevantTypeScreen();
                
                //if (SelectedType == ProductTypes.Book)
                //{
                //    ShowRelevantTypeScreen2(AddBookVisibillity.ToString(), new BookGenre());
                //    AddBookVisibillity = Visibility.Visible;
                //}
                //else if (SelectedType == ProductTypes.Journal)
                //{
                //    ShowRelevantTypeScreen2(AddJournalVisibillity.ToString(), new JournalGenre());
                //    AddJournalVisibillity =Visibility.Visible;
                //}
                //else
                //{
                //    throw new InvalidOperationException();
                //}

            }
        }
        //ctor
        public AddProductViewModel() 

        {
            //string objTypeName ="Add" + ProductTypes.Book.ToString() + "View";

            //ObjectHandle handle = Activator.CreateInstance(null, "BookStore.Views.AddBookView");
            //object O = handle.Unwrap();
            //UserControl ctl = O as UserControl;

            //UserControl c = new AddBookView();

            AddBookVisibillity = Visibility.Collapsed;
            AddJournalVisibillity = Visibility.Collapsed;

            ProductTypesOc = new ObservableCollection<ProductTypes>(Enum.GetValues(typeof(ProductTypes)).Cast<ProductTypes>());
            JournalFreqOc = new ObservableCollection<JournalFrequency>(Enum.GetValues(typeof(JournalFrequency)).Cast<JournalFrequency>());

            
            ProductGenreOc = new ObservableCollection<string>();
            AddProdCommand = new RelayCommand(AddProductToDb);
        }
        // func

        private void ShowRelevantTypeScreen()
        {
            if (SelectedType == ProductTypes.Book)
            {
                if (AddJournalVisibillity == Visibility.Visible)
                {
                    AddJournalVisibillity = Visibility.Collapsed;

                    if (ProductGenreOc.Count != 0)
                    {
                        ProductGenreOc.Clear();
                    }
                }

                var enumValues = Enum.GetValues(typeof(BookGenre));
                foreach (var enumValue in enumValues)
                {
                    ProductGenreOc.Add(((Enum)enumValue).ToString());
                }
                AddBookVisibillity = Visibility.Visible;
            }
            else if (SelectedType == ProductTypes.Journal)
            {
                if (AddBookVisibillity == Visibility.Visible)
                {
                    AddBookVisibillity = Visibility.Collapsed;
                    if (ProductGenreOc.Count != 0)
                    {
                        ProductGenreOc.Clear();
                    }
                }

                var enumValues = Enum.GetValues(typeof(JournalGenre));
                foreach (var enumValue in enumValues)
                {
                    ProductGenreOc.Add(((Enum)enumValue).ToString());
                }
                AddJournalVisibillity = Visibility.Visible;
            }

        }
        private void AddProductToDb()
        {
            if (AddBookVisibillity == Visibility.Visible)
            {

                ProductService.Instance.AddBook(AuthorName, Title, Quantity, DateTimeSelection, Price, Eddition, SelectedBookGenre);

            }
            else if (AddJournalVisibillity == Visibility.Visible)
            {
                ProductService.Instance.AddJournal(AuthorName, Title, Eddition, Quantity, DateTimeSelection, Price, SelectedJournalFreq, SelectedJournalGenre);
            }
        }
    }
}
